# Create a coverage report by:
# 1: using OpenCover to run the xunit tests
# 2: ReportGenerator for the HTML report (if COVERALLS_REPO_TOKEN NOT available)
# 3: Coveralls.net to export the generated OpenCover report to coveralls.io (if COVERALLS_REPO_TOKEN available)

$projectName = (gci *.Sln).BaseName
$filter="-filter:+`"[$projectName]*`""
$output="coverage.xml"
$nugetPackages="$env:USERPROFILE\.nuget\packages"
$opencoverPath = ((gci $nugetPackages\opencover\*\tools | sort-object name)[-1]).Fullname
$xunitrunnerPath = ((gci $nugetPackages\xunit.runner.console\*\tools | sort-object name)[-1]).Fullname
$dotnet="net45"
$openCoverArguments = @("-register:user", "$filter", "-target:$xunitrunnerPath\xunit.console.exe","-targetargs:`"$projectName.Tests\bin\release\$dotnet\$projectName.Tests.dll -noshadow -xml xunit.xml`"","-output:`"$output`"")
Start-Process -wait $opencoverPath\OpenCover.Console.exe -NoNewWindow -ArgumentList $openCoverArguments

if (Test-Path Env:COVERALLS_REPO_TOKEN) {
	$coverallsPath = ((gci $nugetPackages\coveralls.io\*\tools | sort-object name)[-1]).Fullname
	$coverallsArguments = @("--opencover $output")
	Start-Process -wait $coverallsPath\coveralls.net.exe -NoNewWindow -ArgumentList $coverallsArguments
}
else {
	$reportgeneratorPath = ((gci $nugetPackages\ReportGenerator\*\tools | sort-object name)[-1]).Fullname
	$reportgeneratorArguments = @("-reports:$output", "-targetdir:CoverageReport")
	Start-Process -wait $reportgeneratorPath\ReportGenerator.exe -NoNewWindow -ArgumentList $reportgeneratorArguments
}