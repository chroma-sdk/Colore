[CmdletBinding()]
Param(
    [Parameter(Mandatory=$True)]
    [string]$Platform,

    [Parameter(Mandatory=$True)]
    [string]$Configuration
)

$binStub = $Configuration

$zip = "colore.zip"

If ($Platform -eq "Any CPU")
{
    $zip = "colore_anycpu.zip"
}
Else
{
    $binStub = "$Platform\$binStub"
    If ($Platform -eq "x86")
    {
        $zip = "colore_x86.zip"
    }
    Else
    {
        $zip = "colore_x64.zip"
    }
}

Write-Host "Zipping artifacts for $Platform - $Configuration"

7z.exe a $zip .\src\Corale.Colore\bin\$binStub\*.dll
7z.exe a $zip .\src\Corale.Colore\bin\$binStub\*.xml
7z.exe a $zip .\src\Corale.Colore\bin\$binStub\*.pdb

# Create NuGet package if Release and Any CPU
if ($Configuration -eq "Release" -And $Platform -eq "Any CPU")
{
    Write-Host "Creating NuGet package"
    nuget.exe --% pack src\Corale.Colore\Corale.Colore.csproj -Prop Configuration=Release -Prop Platform=AnyCPU -Version "%APPVEYOR_BUILD_VERSION%"
    $name = "Colore.$Env:APPVEYOR_BUILD_VERSION"
    $file = "$name.nupkg"
    Push-AppveyorArtifact $file -DeploymentName "nuget_package"
}
