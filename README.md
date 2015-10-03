Colore
======

[![Stories in Ready][wafflebadge]][waffle]
[![MIT License][licensebadge]][license]
[![Latest GitHub release][ghreleasebadge]][ghrelease]
[![NuGet version][ngverbadge]][ng]
[![NuGet downloads][ngdlbadge]][ng]

**Develop:**
[![Build status][devbuildbadge]][devbuild]
[![Coverage Status][devcoverbadge]][devcover]

**Master:**
[![Master build status][masterbuildbadge]][masterbuild]
[![Master coverage Status][mastercoverbadge]][mastercover]

A powerful and elegant C# library for Razer Chroma's SDK

Contributing
------------

[![Gitter][gitterbadge]][gitter]

*For discussing, you can join the Gitter chat using the badge above. If you want to join the Slack chat, contact [Brandon Scott][bs] ([brandon@brandonscott.co.uk](mailto:brandon@brandonscott.co.uk)) or [Adam Hellberg][sharp] ([sharparam@sharparam.com](mailto:sharparam@sharparam.com)).*

Contributors are very welcome! If you got code fixes, please [submit a pull request][newpull] here on GitHub.

If you want to join the development team, please contact [Sharparam][sharp] or [Brandon][bs] on GitHub.

All authors and contributors are listed in the **AUTHORS** file.

Please read the [CONTRIBUTING.md](CONTRIBUTING.md) file before making a pull request.

License
-------

Copyright &copy; 2015 by [Adam Hellberg][sharp] and [Brandon Scott][bs].

This project is licensed under the MIT license, please see the file **LICENSE** for more information.

Razer is a trademark and/or a registered trademark of Razer USA Ltd.  
All other trademarks are property of their respective owners.

This project is in no way endorsed, sponsored or approved by Razer.

Dependencies
------------

Colore depends on the Razer Chroma SDK (RzChromaSDK64.dll or RzChromaSDK.dll).

The Razer Chroma SDK is provided by Razer and [can be obtained from their website][rzdev].

Building
--------

It's important to note that the platform under which this project is built plays a huge role on the usability of the library.

When compiling with the x86 or x64 platform set in build configuration, Colore will **only work on the respective system platform**
(32-bit if compiled using x86, and 64-bit if compiled using x64).

The native methods are imported using `DllImport` when Colore is compiled in x86 or x64, which is why the setting matters for deployment,
as this cannot be changed at runtime.

However, if compiling with the "Any CPU" configuration, Colore will dynamically load functions relevant for the current executing platform,
making it run on both 32- and 64-bit systems without any work having to be done by the dev.

For non-performance critical applications, the "Any CPU" mode should be fine (this is also what the NuGet package is compiled against).

For applications that require peak performance, we recommend shipping separate 32- and 64-bit builds of your application, using the relevant build configurations in Colore.

The below example compiles Colore in Release mode for the x86 (32-bit) platform.

```
msbuild Colore.sln /p:Configuration=Release;Platform=x86
```

(Replace `x86` with `x64` if compiling for Win64, or `"Any CPU"` if compiling cross-platform)

Make sure that your projects using Colore are also compiled against a matching platform.

Razer's SDK installer **will only install the library relevant for your platform**.

This means that your apps will need to be compiled twice, once for x86 platforms, and once for x64, unless you are using "Any CPU".
They must both be provided when you distribute your application, depending on what platform the user has.

Projects
--------

Current projects utilizing this or modified versions of this library:

*None right now!*

(If you want your project listed, just contact [Sharparam][sharp] or [Brandon][bs])

[newpull]: ../../pull/new/develop
[sharp]: https://github.com/Sharparam
[contrib]: ../../wiki/Contributing
[bs]: https://github.com/brandonscott
[rzdev]: http://developer.razerzone.com/chroma

[waffle]: http://waffle.io/coralestudios/colore
[wafflebadge]: https://badge.waffle.io/coralestudios/colore.svg?label=ready&title=Ready
[license]: http://opensource.org/licenses/MIT
[licensebadge]: https://img.shields.io/badge/license-MIT-blue.svg
[ghrelease]: https://github.com/CoraleStudios/Colore/releases
[ghreleasebadge]: https://img.shields.io/github/release/CoraleStudios/Colore.svg
[ng]: https://www.nuget.org/packages/Colore
[ngverbadge]: https://img.shields.io/nuget/v/Colore.svg
[ngdlbadge]: https://img.shields.io/nuget/dt/Colore.svg

[devbuild]: http://tc.sharpblade.net/viewType.html?buildTypeId=colore_mainbuild
[devbuildbadge]: https://img.shields.io/teamcity/http/tc.sharpblade.net/s/colore_mainbuild.svg?style=flat
[devcover]: https://coveralls.io/r/CoraleStudios/Colore?branch=develop
[devcoverbadge]: https://coveralls.io/repos/CoraleStudios/Colore/badge.svg?branch=develop

[masterbuild]: http://tc.sharpblade.net/viewType.html?buildTypeId=colore_releasebuild
[masterbuildbadge]: https://img.shields.io/teamcity/http/tc.sharpblade.net/s/colore_releasebuild.svg?style=flat
[mastercover]: https://coveralls.io/r/CoraleStudios/Colore?branch=master
[mastercoverbadge]: https://coveralls.io/repos/CoraleStudios/Colore/badge.svg?branch=master

[gitter]: https://gitter.im/CoraleStudios/Colore?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge
[gitterbadge]: https://badges.gitter.im/Join%20Chat.svg
