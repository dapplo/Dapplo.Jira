# Set the build version in the project.json files

$version = $env:APPVEYOR_BUILD_VERSION
if ( Test-Path Env:\APPVEYOR_BUILD_VERSION ) {
	$version = $env:APPVEYOR_BUILD_VERSION
} else {
	$version = "0.9.9.9"
}

Get-ChildItem . -recurse project.json | 
		foreach {
			$projectJson = $_.FullName
			echo "Reading JSON from $projectJson"
			$jsonContent = Get-Content $projectJson -raw | ConvertFrom-Json
			$oldVersion = $jsonContent.version
			echo "Modifying the version in $projectJson from $oldVersion to $version"
			$jsonContent.version = $version
			$jsonContent | ConvertTo-Json  -Depth 5 | set-content $_.FullName
		}
