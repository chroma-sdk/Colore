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
git clone -b gh-pages --depth 1 -- git@github.com:CoraleStudios/Colore.git gh-pages

Write-Host "Removing existing documentation at gh-pages/${target}"
rmrf .\gh-pages\${target}
Write-Host "Removing existing coverage at gh-pages/${covtarget}"
rmrf .\gh-pages\${covtarget}

Write-Host 'Copying new documentation'
cp .\docs\_site .\gh-pages\${target} -Recurse
Write-Host 'Copying new coverage'
cp .\artifacts\coverage-report .\gh-pages\${covtarget} -Recurse

$gitdata="$(git log -n 1 --format='commit %h - %s')"
Write-Host "Git data: ${gitdata}"

Push-Location gh-pages

git add ${target}
git commit -m "[AUTOMATED] Documentation update

Updates to project documentatio and coverage data.

Timestamp: $(Get-Date -UFormat '%Y-%m-%d %T%Z')
From ${gitdata}
Target: ${target}
Coverage target: ${covtarget}"

git push origin gh-pages

Pop-Location

Write-Host 'Removing gh-pages folder after completion of update'
rmrf gh-pages

Write-Host 'AppVeyor docs deploy script finished'
