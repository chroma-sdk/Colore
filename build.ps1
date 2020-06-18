[CmdletBinding()]
Param(
    [Parameter(Mandatory=$false)]
    [string]$Script = "build.cake",
    [Parameter(Mandatory=$false)]
    [string]$Target = "Default",
    [Parameter(Mandatory=$false)]
    [string]$Verbosity = "Normal",
    [Parameter(Position=0, Mandatory=$false, ValueFromRemainingArguments=$true)]
    [string[]]$ScriptArgs
)

# Restore Cake tool
& dotnet tool restore

# Build Cake arguments
$cakeArguments = @("$Script");
if ($Target) { $cakeArguments += "--target=$Target" }
$cakeArguments += "--verbosity=$Verbosity"
$cakeArguments += $ScriptArgs

& dotnet tool run dotnet-cake --bootstrap --verbosity=$Verbosity
& dotnet tool run dotnet-cake -- $cakeArguments
exit $LASTEXITCODE
