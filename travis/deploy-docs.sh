#!/usr/bin/env bash

set -euf -o pipefail

target=devdocs

if [ $# -eq 1 ] && [ "$1" == 'release' ];
then
    echo 'Deploying docs in RELEASE mode'
    target=docs
fi

echo 'Cleaning any existing gh-pages directory'
rm -rf gh-pages

echo 'Cloning gh-pages branch'
git clone -b gh-pages --depth 1 -- git@github.com:CoraleStudios/Colore.git gh-pages

echo "Removing existing documentation at gh-pages/${target}"
rm -rf gh-pages/${target}

echo 'Copying new documentation'
cp -r docs/_site gh-pages/${target}

gitdata="$(git log -n 1 --format='commit %h - %s')"
echo "Git data: ${gitdata}"

(
cd gh-pages
git add ${target}
git commit -m "[AUTOMATED] Documentation update

Updates to project documentation.

Timestamp: $(date "+%Y-%m-%d %H:%M:%S")
From ${gitdata}
Target: ${target}"

git push origin gh-pages
)

rm -rf gh-pages
