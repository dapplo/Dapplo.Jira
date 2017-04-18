#tool "xunit.runner.console"
#tool "OpenCover"
#tool "GitVersion.CommandLine"
#tool "docfx.console"
#tool "coveralls.net"
#addin "MagicChunks"
#addin "Cake.FileHelpers"
#addin "Cake.DocFx"
#addin "Cake.Coveralls"

var target = Argument("target", "Build");
var configuration = Argument("configuration", "release");
var nugetApiKey = Argument("nugetApiKey", EnvironmentVariable("NuGetApiKey"));
var coverallsRepoToken = Argument("nugetApiKey", EnvironmentVariable("COVERALLS_REPO_TOKEN"));
var solutionFilePath = GetFiles("./**/*.sln").First();

// Used to store the version, which is needed during the build and the packaging
GitVersion version;

Task("Default")
    .IsDependentOn("Publish");

// Publish the Artifact of the Package Task to the Nexus Pro
Task("Publish")
	.IsDependentOn("PublishPackages")
	.IsDependentOn("PublishCoverage")
    .WithCriteria(() => !BuildSystem.IsLocalBuild);

// Publish the coveralls report to Coveralls.NET
Task("PublishCoverage")
    .IsDependentOn("Coverage")
    .WithCriteria(() => !BuildSystem.IsLocalBuild)
    .WithCriteria(() => !string.IsNullOrEmpty(coverallsRepoToken))
    .Does(()=>
{
	CoverallsIo("./artifacts/coverage.xml", new CoverallsIoSettings()
    {
        RepoToken = coverallsRepoToken
    });
});

// Publish the Artifacts of the Package Task to NuGet
Task("PublishPackages")
    .IsDependentOn("Package")
    .WithCriteria(() => !BuildSystem.IsLocalBuild)
    .WithCriteria(() => !string.IsNullOrEmpty(nugetApiKey))
    .Does(()=>
{
    var settings = new NuGetPushSettings {
        ApiKey = nugetApiKey
    };

    var packages = GetFiles("./artifacts/*.nupkg");
    NuGetPush(packages, settings);
});

// Package the results of the build, if the tests worked, into a NuGet Package
Task("Package")
	.IsDependentOn("Build")
	.IsDependentOn("Documentation")
    .Does(()=>
{
    var settings = new DotNetCorePackSettings  
    {
        OutputDirectory = "./artifacts/",
        Verbose = true,
        Configuration = configuration
    };

    var projectFilePaths = GetFiles("./**/*.xproj").Where(p => !p.FullPath.Contains("Test") && !p.FullPath.Contains("packages") &&!p.FullPath.Contains("tools"));
    foreach(var projectFilePath in projectFilePaths)
    {
	
        Information("Packaging: " + projectFilePath.FullPath);
        var nuspecFilename = string.Format("{0}/{1}.nuspec", projectFilePath.GetDirectory(),projectFilePath.GetFilenameWithoutExtension());
        TransformConfig(nuspecFilename, 
            new TransformationCollection {
                { "package/metadata/version", version.NuGetVersion }
            });

		DotNetCorePack(projectFilePath.GetDirectory().FullPath, settings);
    }
});

// Build the DocFX documentation site
Task("Documentation")
    .Does(() =>
{
	DocFx("./doc/docfx.json");
});

// Run the XUnit tests via OpenCover, so be get an coverage.xml report
Task("Coverage")
    .IsDependentOn("Build")
    .Does(() =>
{
    CreateDirectory("artifacts");

    var openCoverSettings = new OpenCoverSettings() {
        // Forces error in build when tests fail
        ReturnTargetCodeOffset = 0
    };

    var projectFiles = GetFiles("./**/*.xproj");
    foreach(var projectFile in projectFiles)
    {
        var projectName = projectFile.GetDirectory().GetDirectoryName();
        if (projectName.Contains("Test")) {
           openCoverSettings.WithFilter("-["+projectName+"]*");
        }
        else {
           openCoverSettings.WithFilter("+["+projectName+"]*");
        }
    }

    // Make XUnit 2 run via the OpenCover process
    OpenCover(
        // The test tool Lamdba
        tool => {
            tool.XUnit2("./**/*.Tests.dll",
                new XUnit2Settings {
                    ShadowCopy = false,
					XmlReport = true,
					OutputDirectory = "./artifacts",
					WorkingDirectory = "./src"
                });
            },
        // The output path
        new FilePath("./artifacts/coverage.xml"),
        // Settings
       openCoverSettings
    );
});

// This starts the actual MSBuild
Task("Build")
    .IsDependentOn("RestoreNuGetPackages")
    .IsDependentOn("Clean")
    .IsDependentOn("Versioning")
    .Does(() =>
{
	DotNetBuild(solutionFilePath, settings => settings.SetConfiguration(configuration));
    // Make sure the .dlls in the obj path are not found elsewhere
    CleanDirectories("./**/obj");
});

// Load the needed NuGet packages to make the build work
Task("RestoreNuGetPackages")
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

// Version is written to the AssemblyInfo files when !BuildSystem.IsLocalBuild
Task("Versioning")
    .Does(() =>
{
    version = GitVersion();

	var projectFilePaths = GetFiles("./**/project.json").Where(p => !p.FullPath.Contains("Test") && !p.FullPath.Contains("packages") &&!p.FullPath.Contains("tools"));
    foreach(var projectFilePath in projectFilePaths)
    {
        Information("Changing version in : " + projectFilePath.FullPath + " to " + version.AssemblySemVer);
		 TransformConfig(projectFilePath.FullPath, 
            new TransformationCollection {
                { "version", version.AssemblySemVer }
            });
	}
		
    Information("Version of this build: " + version.AssemblySemVer);
    Information("Nuget Version of this build: " + version.NuGetVersion);

});


// Clean all unneeded files, so we build on a clean file system
Task("Clean")
    .Does(() =>
{
    CleanDirectories("./**/obj");
    CleanDirectories("./**/bin");
    CleanDirectories("./artifacts");	
});

RunTarget(target);