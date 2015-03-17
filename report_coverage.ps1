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
$nunit = "packages\NUnit.Runners.2.6.4\tools\nunit-console.exe"
$filter = "+[Corale.Colore*]* -[*Tests]* -[*]Corale.Colore.Native* -[*]*NativeMethods -[*]*NativeWrapper -[*]Corale.Colore.Annotations*"
$targetArgs = "/noshadow /domain:single $dll"

$Env:NUNIT_EXEC = $nunit
$Env:OPENCOVER_FILTER = $filter
$Env:TARGET_ARGS = $targetArgs

.\packages\OpenCover.4.5.3723\OpenCover.Console.exe --% -register:user -filter:"%OPENCOVER_FILTER%" -target:"%NUNIT_EXEC%" -targetargs:"%TARGET_ARGS%" -output:coverage.xml
.\packages\coveralls.io.1.2.2\tools\coveralls.net.exe --opencover coverage.xml
