function rmrf($path)
{
    Remove-Item $path -Recurse -Force -ErrorAction Ignore
}

if ($env:APPVEYOR_REPO_TAG -ne 'true' -and $env:APPVEYOR_REPO_BRANCH -ne 'develop')
{
    Write-Host 'Not running from tag or develop branch, aborting,'
    return
}

Write-Host 'AppVeyor docs deploy script running'

$release = $false
$target = 'devdocs'
$covtarget = 'devcoverage'

if ($env:APPVEYOR_REPO_TAG -eq 'true')
{
    Write-Host 'Building from tag, setting variables to release values'
    $release = $true
    $target = 'docs'
    $covtarget = 'coverage'
}

Write-Host 'Cleaning any existing gh-pages directory'
rmrf gh-pages

Write-Host 'Cloning gh-pages branch'
git clone -q -b gh-pages --depth 1 -- git@github.com:CoraleStudios/Colore.git gh-pages

if (!(Test-Path -Path gh-pages))
{
    Write-Host 'Failed to clone gh-pages branch, aborting'
    return 1
}

Write-Host "Removing existing documentation at gh-pages/${target}"
rmrf .\gh-pages\${target}
Write-Host "Removing existing coverage at gh-pages/${covtarget}"
rmrf .\gh-pages\${covtarget}

Write-Host 'Copying new documentation'
cp .\docs\_site .\gh-pages\${target} -Recurse
Write-Host 'Copying new coverage'
cp .\artifacts\coverage-report .\gh-pages\${covtarget} -Recurse

$gitdata="$(git log -n 1 --format='commit %h - %s')"
$timestamp = "$(Get-Date -UFormat '%Y-%m-%d %T%Z')"
Write-Host "Git data: ${gitdata}"

Push-Location gh-pages

Write-Host "Adding files"
cmd /c "git add ${target} 2>&1"
cmd /c "git add ${covtarget} 2>&1"

Write-Host "Committing"
git commit -q -m "[AUTOMATED] Documentation update

Updates to project documentatio and coverage data.

Timestamp: ${timestamp}
From ${gitdata}
Target: ${target}
Coverage target: ${covtarget}"

Write-Host "Pushing"
git push -q origin gh-pages

Pop-Location

Write-Host 'Removing gh-pages folder after completion of update'
rmrf gh-pages

Write-Host 'AppVeyor docs deploy script finished'
