$buildCounter = "%build.counter%"
$branch = "%teamcity.build.branch%"

$prefix = 'refs/heads/'
$tagPrefix = 'refs/tags/'
$isTag = False

$friendlyVersion = '0.0.0'

if ($branch.StartsWith($prefix))
{
    $branch = $branch.substring($prefix.length)
}
elseif ($branch.StartsWith($tagPrefix))
{
    $branch = $branch.substring($tagPrefix.length)
    $isTag = True
}

Write-Host "Branch: $branch"

if ($branch -eq 'master')
{
    $friendlyVersion = '1.0.0'
    $buildNumber = "${friendlyVersion}.${buildCounter}"
}
elseif ($branch -eq 'develop')
{
    $friendlyVersion = '0.0.0'
    $buildNumber = "${friendlyVersion}.${buildCounter}"
}
elseif ($branch -match "^v\d+\.\d+\.\d+$")
{
    $friendlyVersion = $branch.substring(1)
    $buildNumber = "${friendlyVersion}.${buildCounter}"
}

Write-Host "##teamcity[buildNumber '$buildNumber']"
Write-Host "##teamcity[setParameter name='Version' value='$friendlyVersion']"

if (!$isTag)
{
    Write-Host "##teamcity[setParameter name='InfoVersion' value='$friendlyVersion-$branch]"
}
