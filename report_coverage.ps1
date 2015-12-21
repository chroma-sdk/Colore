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

$dir = "Corale.Colore.Tests\bin\$Configuration"
$dll = "Corale.Colore.Tests.dll"
$nunit = "packages\NUnit.Console.3.0.1\tools\nunit3-console.exe"
$filter = "+[Corale.Colore*]* -[*Tests]* -[*]*Constants -[*]Corale.Colore.Native* -[*]*NativeMethods -[*]*NativeWrapper -[*]Corale.Colore.Annotations*"
$targetArgs = "$dll"

$Env:NUNIT_EXEC = $nunit
$Env:OPENCOVER_FILTER = $filter
$Env:TARGET_DIR = $dir
$Env:TARGET_ARGS = $targetArgs

$git_log = git --% log -1 --format=%H;%an;%ae;%s
$git_info = $git_log -split ';'

$Env:GIT_HASH = $git_info[0]
$Env:GIT_NAME = $git_info[1]
$Env:GIT_EMAIL = $git_info[2]
$Env:GIT_SUBJECT = $git_info[3]
$Env:GIT_BRANCH = git --% name-rev --name-only HEAD

.\packages\OpenCover.4.6.166\tools\OpenCover.Console.exe --% -register "-filter:%OPENCOVER_FILTER%" "-target:%NUNIT_EXEC%" "-targetargs:%TARGET_ARGS%" "-targetdir:%TARGET_DIR%" -output:coverage.xml

# --commitId %GIT_HASH% --commitBranch %GIT_BRANCH% --commitAuthor %GIT_NAME% --commitEmail %GIT_EMAIL% --commitMessage %GIT_SUBJECT%
.\packages\coveralls.net.0.6.0\tools\csmacnz.Coveralls.exe --% --opencover -i coverage.xml --useRelativePaths --repoTokenVariable COVERALLS_REPO_TOKEN --jobId %CI_JOB_ID% --serviceName TeamCity
