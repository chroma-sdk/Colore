[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True)]
    [string]$Platform,

    [Parameter(Mandatory=$True)]
    [string]$Configuration
)

If ($Platform -ne "Any CPU")
{
    Exit
}

$dll = "Corale.Colore.Tests\bin\$Configuration\Corale.Colore.Tests.dll"
$nunit = "packages\NUnit.Console.3.0.1\tools\nunit3-console.exe"
$filter = "+[Corale.Colore*]* -[*Tests]* -[*]*Constants -[*]Corale.Colore.Native* -[*]*NativeMethods -[*]*NativeWrapper -[*]Corale.Colore.Annotations*"
$targetArgs = "$dll"

$Env:NUNIT_EXEC = $nunit
$Env:OPENCOVER_FILTER = $filter
$Env:TARGET_ARGS = $targetArgs

.\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe --% -register:user -filter:"%OPENCOVER_FILTER%" -target:"%NUNIT_EXEC%" -targetargs:"%TARGET_ARGS%" -output:coverage.xml
.\packages\coveralls.net.0.6.0\tools\csmacnz.Coveralls.exe --% --opencover -i coverage.xml --repoTokenVariable COVERALLS_REPO_TOKEN --jobId %CI_JOB_ID% --serviceName TeamCity
