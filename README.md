<p align="center"><img src="images/logo.png" alt="Colore"></p>

<p align="center">
  <a href="https://opensource.org/licenses/MIT">
    <img alt="MIT License" src="https://img.shields.io/badge/license-MIT-blue.svg">
  </a>
  <a href="https://github.com/chroma-sdk/Colore/releases">
    <img alt="Latest GitHub release" src="https://img.shields.io/github/release/chroma-sdk/Colore.svg?logo=github">
  </a>
  <a href="https://www.nuget.org/packages/Colore">
    <img alt="NuGet package" src="https://img.shields.io/nuget/v/Colore.svg">
  </a>
</p>

<p align="center">A powerful and elegant C# library for Razer Chroma's SDK.</p>

| | GitHub | AppVeyor | Travis |
|-|-|-|-|
| [**`master`**][master] | [![Workflow status][gha-master-badge]][gha-master] | [![Build status][appveyor-master-badge]][appveyor-master-status] | [![TravisCI Status][travis-master-badge]][travis-master-status] |
| [**`develop`**][develop] | [![Workflow status][gha-develop-badge]][gha-develop] | [![Build status][appveyor-develop-badge]][appveyor-develop-status] | [![TravisCI Status][travis-develop-badge]][travis-develop-status] |

| [`master`][master] | [![Test status][test-master-badge]][test-master-status] | [![Codecov coverage][codecov-master-badge]][codecov-master] | [![CodeFactor][codefactor-master-badge]][codefactor-master] |
|-|-|-|-|
| [**`develop`**][develop] | [![Test status][test-develop-badge]][test-develop-status] | [![Codecov coverage][codecov-develop-badge]][codecov-develop] | [![CodeFactor][codefactor-develop-badge]][codefactor-develop] |

Getting started
---------------

If you are a new developer and are looking for a helpful guide on how to get started, head on over to the [documentation](https://chroma-sdk.github.io/Colore/docs/articles/getting-started.html) which describes getting Colore installed and running some example code.

Contributing
------------

[![Discussions][discussions-badge]][discussions]
[![Discord][discordbadge]][discord]

*For discussing, you can [discuss right here in the repo][discussions] or join the Razer Community Discord server using the badges above. If you want to join the Slack chat, contact [Adam Hellberg][sharp] ([sharparam@sharparam.com](mailto:sharparam@sharparam.com)).*

Contributors are very welcome! If you have code fixes, please [submit a pull request][newpull] here on GitHub.

If you want to join the development team, please contact [Sharparam][sharp] on GitHub.

All authors and contributors are listed in the [`AUTHORS`](AUTHORS) file.
Feel free to add yourself to this file under a relevant section in your pull request.

Please read the [`CONTRIBUTING.md`](CONTRIBUTING.md) file before making a pull request.

### Code of Conduct
Please note that this project is released with a [Contributor Code of Conduct][coc].
By participating in this project you agree to abide by its terms.

License
-------

Copyright &copy; 2015-2021 by [Adam Hellberg][sharp] and [Brandon Scott][bs].

This project is licensed under the MIT license, please see the file [`LICENSE`](LICENSE) for more information.

Razer is a trademark and/or a registered trademark of Razer USA Ltd.
All other trademarks are property of their respective owners.

Installing
----------

Using Colore in your project is simple, all you have to do is install it with NuGet!

```powershell
Install-Package Colore
```

Or using the .NET CLI tools:

```powershell
dotnet add package Colore
```

You can also search for it in Visual Studio by right clicking your project and choosing "Manage NuGet Packages..." and install it the GUI way.

### Pre-releases

New release candidate versions will be uploaded to NuGet and marked as pre-releases there (vX.Y.Z-rcDDDD).

You can also find pre-release versions for [Colore][gpr-colore] and any additional extensions in [the GitHub package registry][gpr]. The GitHub package registry will be the place where you'll always find the most bleeding edge packages.

### Extensions

The [WPF][colore-wpf] and [WinForms][colore-winforms] extension packages for Colore are not yet available for the new Colore version, but will be on NuGet soon™, so stay tuned!

Using
-----

Obtain a reference to an `IChroma` instance by calling `Colore.ColoreProvider.CreateNative()`.

This instance initializes the Chroma SDK so it is important you **save this reference** for the lifetime of your application!
If you need to dispose of it and obtain a new one later, be sure to call the uninitialize method first!

Colore supports binding to both the [native Chroma SDK][chroma-native] and the [REST API][chroma-rest].
To use the REST API, create an `IChroma` instance by calling `Colore.ColoreProvider.CreateRestAsync(AppInfo)`.
The REST API requires information about your application, so you'll have to pass an instance of `AppInfo` to `CreateRestAsync` containing details about your application or game.

For a more in-depth guide on how to get started, check out [our wiki][getting-started].

For more information on the native and REST SDKs that Colore uses, check out [Razer's official page for the Chroma SDK][rzdev] and their [page about SDK tools][chroma-sdk-tools].

Dependencies
------------

Colore depends on the Razer Chroma SDK (`RzChromaSDK64.dll` or `RzChromaSDK.dll`).

The Razer Chroma SDK is provided by Razer and installed together with the [Synapse application][synapse].
More information can be read [on their website][rzdev].

Other dependencies are installed via NuGet and listed in each project file.

Building
--------

Colore supports building for multiple target frameworks.
At the moment, these are .NET Standard 1.3 and .NET Framework 4.5.1.
When building the project, DLLs for both frameworks will be generated in the output folders, under the folder names `netstandard1.3` and `net451`.
Use the ones fitting for your application.

The below examples compiles Colore in Release mode.

```powershell
.\build.ps1 -Configuration Release
```

You can also use the "CI" build target to generate the same artifacts made available for each release of Colore.

```powershell
.\build.ps1 -Configuration Release -Target CI
```

You will find the resulting artifact files under the `artifacts` folder in the root of the repository.

Note that the above commands are executed with [PowerShell][ps]. If you are building on a Linux system or macOS,
use the `build.sh` script in place of `build.ps1` (you may have to make it executable first with `chmod +x build.sh`).

Native Documentation
--------------------
As Colore is built upon the native Chroma SDK for C++, it can be worth taking a look at their [documentation][chroma-native].

REST Documentation
------------------

Colore's REST mode is built on Razer's official REST API, which [has its own documentation][chroma-rest].

Razer Chroma Workshop
---------------------

Many of the games and apps featured on the [Razer Chroma Workshop][workshop] have used the Colore library.

The official [Razer Chroma Workshop][workshop] is your one-stop-shop to get the most out of your Chroma devices. Whether it's smart lighting based on in-game events, standalone apps or stunning profiles created by fans around the world, the Chroma Workshop is where you can explore, download and even share your own creations.

Games using Colore
------------------

The following games (powered by Unity) are using Colore:

[![DubWars](http://cdn.akamai.steamstatic.com/steam/apps/290000/capsule_184x69.jpg)](https://store.steampowered.com/app/290000/)
[![Masquerada: Songs and Shadows](http://cdn.akamai.steamstatic.com/steam/apps/459090/capsule_184x69.jpg)](https://store.steampowered.com/app/459090/)
[![Nevermind](http://cdn.akamai.steamstatic.com/steam/apps/342260/capsule_184x69.jpg)](https://store.steampowered.com/app/342260/)
[![Please, Don't Touch Anything 3D](http://cdn.akamai.steamstatic.com/steam/apps/529590/capsule_184x69.jpg)](https://store.steampowered.com/app/529590/)
[![Starcrawlers](http://cdn.akamai.steamstatic.com/steam/apps/318970/capsule_184x69.jpg)](https://store.steampowered.com/app/318970/)
[![The Little Acre](http://cdn.akamai.steamstatic.com/steam/apps/423590/capsule_184x69.jpg)](https://store.steampowered.com/app/423590/)

Projects using Colore
---------------------

[Aurora](https://www.project-aurora.com/) - Unified lighting effects across multiple brands and various games. ([GitHub](https://github.com/antonpup/Aurora))

There may be others we are unaware of, so please let us know if there are any others.

[coc]: CODE_OF_CONDUCT.md
[getting-started]: https://github.com/chroma-sdk/Colore/wiki/Getting-started
[newpull]: https://github.com/chroma-sdk/Colore/pull/new/develop
[sharp]: https://github.com/Sharparam
[contrib]: CONTRIBUTING.md
[bs]: https://github.com/brandonscott
[master]: https://github.com/chroma-sdk/Colore/tree/master
[develop]: https://github.com/chroma-sdk/Colore/tree/develop

[license]: https://opensource.org/licenses/MIT
[licensebadge]: https://img.shields.io/badge/license-MIT-blue.svg
[ghrelease]: https://github.com/chroma-sdk/Colore/releases
[ghreleasebadge]: https://img.shields.io/github/release/chroma-sdk/Colore.svg?logo=github
[ng]: https://www.nuget.org/packages/Colore
[ngverbadge]: https://img.shields.io/nuget/v/Colore.svg
[gpr]: https://github.com/chroma-sdk/Colore/packages
[gpr-colore]: https://github.com/chroma-sdk/Colore/packages/274021

[gha-develop]: https://github.com/chroma-sdk/Colore/actions?query=workflow%3ACI+branch%3Adevelop
[gha-develop-badge]: https://github.com/chroma-sdk/Colore/workflows/CI/badge.svg?branch=develop
[appveyor-develop-status]: https://ci.appveyor.com/project/chroma-sdk/colore/branch/develop
[appveyor-develop-badge]: https://ci.appveyor.com/api/projects/status/on4kfnasv564yhh9/branch/develop?svg=true
[travis-develop-status]: https://travis-ci.org/chroma-sdk/Colore
[travis-develop-badge]: https://travis-ci.org/chroma-sdk/Colore.svg?branch=develop
[test-develop-status]: https://ci.appveyor.com/project/chroma-sdk/colore/branch/develop/tests
[test-develop-badge]: https://img.shields.io/appveyor/tests/chroma-sdk/Colore/develop.svg
[codecov-develop]: https://codecov.io/gh/chroma-sdk/Colore/branch/develop
[codecov-develop-badge]: https://codecov.io/gh/chroma-sdk/Colore/branch/develop/graph/badge.svg
[codefactor-develop]: https://www.codefactor.io/repository/github/chroma-sdk/colore/overview/develop
[codefactor-develop-badge]: https://www.codefactor.io/repository/github/chroma-sdk/colore/badge/develop

[gha-master]: https://github.com/chroma-sdk/Colore/actions?query=workflow%3ACI+branch%3Amaster
[gha-master-badge]: https://github.com/chroma-sdk/Colore/workflows/CI/badge.svg?branch=master
[appveyor-master-status]: https://ci.appveyor.com/project/chroma-sdk/colore/branch/master
[appveyor-master-badge]: https://ci.appveyor.com/api/projects/status/on4kfnasv564yhh9/branch/master?svg=true
[travis-master-status]: https://travis-ci.org/chroma-sdk/Colore
[travis-master-badge]: https://travis-ci.org/chroma-sdk/Colore.svg?branch=master
[test-master-status]: https://ci.appveyor.com/project/chroma-sdk/colore/branch/master/tests
[test-master-badge]: https://img.shields.io/appveyor/tests/chroma-sdk/Colore/master.svg
[codecov-master]: https://codecov.io/gh/chroma-sdk/Colore
[codecov-master-badge]: https://codecov.io/gh/chroma-sdk/Colore/branch/master/graph/badge.svg
[codefactor-master]: https://www.codefactor.io/repository/github/chroma-sdk/colore/overview/master
[codefactor-master-badge]: https://www.codefactor.io/repository/github/chroma-sdk/colore/badge/master

[gitter]: https://gitter.im/chroma-sdk/Colore
[gitterbadge]: https://badges.gitter.im/Join%20Chat.svg
[discord]: https://discord.gg/4ysuMYK
[discordbadge]: https://img.shields.io/discord/342761229544194048.svg?label=discord&logo=discord
[discussions]: https://github.com/chroma-sdk/Colore/discussions
[discussions-badge]: https://img.shields.io/badge/github-discussions-brightgreen?logo=github

[colorelogo]: https://files.sharparam.com/2017/10/31/colore-logo.png
[colore-wpf]: https://github.com/chroma-sdk/Colore.Wpf
[colore-winforms]: https://github.com/chroma-sdk/Colore.WinForms

[rzdev]: https://developer.razer.com/chroma/
[chroma-sdk-tools]: https://developer.razer.com/works-with-chroma/download/
[synapse]: https://www.razer.com/synapse-3
[chroma-native]: https://assets.razerzone.com/dev_portal/C%2B%2B/html/en/index.html
[chroma-rest]: https://assets.razerzone.com/dev_portal/REST/html/index.html

[ps]: https://docs.microsoft.com/en-us/powershell/

[workshop]: https://www.razer.com/chroma-workshop
