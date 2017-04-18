#tool "xunit.runner.console"
#tool "GitVersion.CommandLine"
#addin "Cake.FileHelpers"
#addin "Newtonsoft.Json"

using Newtonsoft.Json;

var target = Argument("target", "Build");
var configuration = Argument("configuration", "Release");
var dotnetVersion = Argument("dotnetVersion", "net45");
var solution = File("./src/Dapplo.Jira.sln");

Task("Default")
	.IsDependentOn("xUnit");

Task("Clean")
	.Does(() =>
{
	//CleanDirectories(string.Format("./**/obj/{0}", configuration));
	//CleanDirectories(string.Format("./**/bin/{0}", configuration));
});

Task("Versioning")
	.Does(() =>
{
	var version = GitVersion();
	Information(logAction=>logAction("Version: {0}", JsonConvert.SerializeObject(version, Formatting.Indented)));

	var projectJsonFile = "./src/Dapplo.Jira.Tests/project.json";
	var projectJsonFileString = FileReadText(projectJsonFile);
	var projectJson = JsonConvert.DeserializeObject<dynamic>(projectJsonFile);
	Information(logAction=>logAction("Project version: {0}",projectJson["version"].ToString()));
	projectJson["version"]=version.AssemblySemVer.ToString();
	Information(logAction=>logAction("Project version: {0}",projectJson["version"].ToString()));
	projectJsonFileString = JsonConvert.SerializeObject(projectJson, Formatting.Indented);
	FileWriteText(projectJsonFile,projectJsonFileString);
});

Task("Restore-NuGet-Packages")
	.Does(() =>
{
	DotNetCoreRestore("./", new DotNetCoreRestoreSettings
	{
		Verbose = false,
		Verbosity = DotNetCoreRestoreVerbosity.Warning,
		Sources = new [] {
			"https://api.nuget.org/v3/index.json"
		}
	});
});

Task("Build")
	.IsDependentOn("Restore-NuGet-Packages")
	.IsDependentOn("Clean")
	.IsDependentOn("Versioning")
	.Does(() =>
{
	DotNetBuild(solution, settings => settings.SetConfiguration(configuration));
});

Task("xUnit")
	.IsDependentOn("Build")
	.Does(() =>
{
	XUnit2(string.Format("./src/Dapplo.Jira.Tests/bin/{0}/{1}/Dapplo.Jira.Tests.dll", configuration, dotnetVersion));
});

RunTarget(target);