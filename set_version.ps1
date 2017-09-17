[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True, Position=1)]
    [int]$buildCounter,

    [Parameter(Mandatory=$True, Position=2)]
    [string]$branch
)

$prefix = 'refs/heads/'
$tagPrefix = 'refs/tags/'
$isTag = $False

$friendlyVersion = '0.0.0'
$buildNumber = "${friendlyVersion}.${buildCounter}"

if ($branch.StartsWith($prefix))
{
    $branch = $branch.substring($prefix.length)
}
elseif ($branch.StartsWith($tagPrefix))
{
    $branch = $branch.substring($tagPrefix.length)
    $isTag = $True
}

Write-Host "Build counter: $buildCounter"
Write-Host "Branch: $branch"

if ($branch -eq 'master')
{
    $friendlyVersion = '0.0.1'
    $buildNumber = "${friendlyVersion}.${buildCounter}"
}
elseif ($branch -eq 'develop')
{
    $friendlyVersion = '0.0.0'
    $buildNumber = "${friendlyVersion}.${buildCounter}"
}
elseif ($branch -match "^v\d+\.\d+\.\d+$")
{
    $isTag = $True
    $friendlyVersion = $branch.substring(1)
    $buildNumber = "${friendlyVersion}.${buildCounter}"
}

$infoVersion = $friendlyVersion

if (!$isTag)
{
    $infoVersion = "${friendlyVersion}-${branch}"
}

Write-Host "##teamcity[buildNumber '$buildNumber']"
Write-Host "##teamcity[setParameter name='Version' value='$friendlyVersion']"
Write-Host "##teamcity[setParameter name='InfoVersion' value='$infoVersion']"

function Update-File
{
    Param([String]$file)

    Write-Host "Updating version in file $file"

    (Get-Content $file) `
        -replace '^\[assembly: AssemblyVersion.+$', "[assembly: AssemblyVersion(`"$friendlyVersion`")]" `
        -replace '^\[assembly: AssemblyFileVersion.+$', "[assembly: AssemblyFileVersion(`"$buildNumber`")]" `
        -replace '^\[assembly: AssemblyInformationalVersion.+$', "[assembly: AssemblyInformationalVersion(`"$infoVersion`")]" |
    Out-File $file
}

function Update-NuSpec
{
    Param([String]$file)

    Write-Host "Updating Colore depend version in file $file"

    (Get-Content $file) `
        -replace 'COLORE_VERSION', "$friendlyVersion" |
    Out-File $file
}

Update-File src/Corale.Colore/Properties/AssemblyInfo.cs
Update-File src/Corale.Colore.Wpf/Properties/AssemblyInfo.cs
Update-File src/Corale.Colore.WinForms/Properties/AssemblyInfo.cs

Update-NuSpec src/Corale.Colore.Wpf/Corale.Colore.Wpf.nuspec
Update-NuSpec src/Corale.Colore.WinForms/Corale.Colore.WinForms.nuspec
