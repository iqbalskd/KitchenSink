Param ($checkoutdir, $nunitversion, $browsersToRun, $testedApp, [String[]] $appsToRun, [String[]] $helpersToRun, $testsPath)

$StarCounterDir = "$checkoutdir\sc"
$StarCounterWorkDirPath = "$StarCounterDir\starcounter-workdir"
$StarCounterRepoPath = "$StarCounterWorkDirPath\personal"
$StarCounterConfigPath = "$StarCounterDir\Configuration"
$StarExePath = "$StarCounterDir\star.exe"
$StarAdminExePath = "$StarCounterDir\staradmin.exe"

Function createXML()
{
	$fileContent = "<?xml version=`"1.0`" encoding=`"UTF-8`"?><service><server-dir>$StarCounterRepoPath</server-dir></service>"
	New-Item -Path $StarCounterConfigPath -Name personal.xml -ItemType "file" -force -Value $fileContent | Out-Null
}

Function createRepo()
{
	Start-Process -FilePath $StarExePath -ArgumentList "`@`@createrepo $StarCounterWorkDirPath" -NoNewWindow -Wait
}

Function runApps($apps, $source)
{
	foreach ($app in $apps)
	{
		$AppWWWPath = "$checkoutdir\$testedApp\$source\$app\wwwroot"
		$AppExePath = "$checkoutdir\$testedApp\$source\$app\bin\Debug\$app.exe"
		$AppArg = "--resourcedir=$AppWWWPath $AppExePath"
		
		$process = Start-Process -FilePath $StarExePath -ArgumentList $AppArg -PassThru -NoNewWindow		
		wait-process -id $process.Id
	}
}

Function runTests()
{
	$NunitConsoleRunnerExePath = "$checkoutdir\$testedApp\packages\NUnit.ConsoleRunner.$nunitversion\tools\nunit3-console.exe"
	$NunitArg = "$testsPath --noheader --teamcity --params Browsers=$browsersToRun"
	
	Start-Process -FilePath $NunitConsoleRunnerExePath -ArgumentList $NunitArg -NoNewWindow -Wait
}

Function killStarcounter()
{
	Start-Process -FilePath $StarAdminExePath -ArgumentList "kill all" -NoNewWindow -Wait
}

Function runAppsAndTests()
{
	try
	{
		createRepo
		createXML
		if($appsToRun)
			runApps -apps $appsToRun -source "src"
		if($helpersToRun)
			runApps -apps $helpersToRun -source "test"
		runTests
		killStarcounter
	}
	Catch
	{
		$ErrorMessage = $_.Exception.Message
		Write-Output $ErrorMessage
		exit(1)
	}
}

Function Main()
{
	if(Test-Path $testsPath)
	{
		runAppsAndTests
	}
	else 
	{ 
		Write-Output "No tests to run"
		exit(0)				
	}
}

Main