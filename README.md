Colore
======

[![GitHub release version](https://img.shields.io/github/release/CoraleStudios/Colore.svg)](https://github.com/CoraleStudios/Colore/releases)
[![NuGet version](https://img.shields.io/nuget/v/Colore.svg)](https://www.nuget.org/packages/Colore)
[![NuGet downloads](https://img.shields.io/nuget/dt/Colore.svg)](https://www.nuget.org/packages/Colore)

**Develop:**
[![Build status](https://ci.appveyor.com/api/projects/status/86g99rkyhc57sa74?svg=true)](https://ci.appveyor.com/project/Corale/colore)
[![Coverage Status](https://coveralls.io/repos/CoraleStudios/Colore/badge.svg?branch=develop)](https://coveralls.io/r/CoraleStudios/Colore?branch=develop)

**Master:**
[![Master build status](https://ci.appveyor.com/api/projects/status/86g99rkyhc57sa74/branch/master?svg=true)](https://ci.appveyor.com/project/Corale/colore/branch/master)
[![Master coverage Status](https://coveralls.io/repos/CoraleStudios/Colore/badge.svg?branch=develop)](https://coveralls.io/r/CoraleStudios/Colore?branch=master)

A powerful and elegant C# library for Razer Chroma's SDK

Contributing
------------

[![Gitter](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/CoraleStudios/Colore?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge)

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

It's important to note that this project doesn't build on the "AnyCPU" platform (which is the default
for C# projects). It builds against x86 or x64 to stay compliant with Razer's code which targets the x86 or x64
platforms. When building with MSBuild, you'd run something like:

```
msbuild Colore.sln /p:Configuration=Release;Platform=x86
```

(Replace `x86` with `x64` if compiling for Win64.)

Make sure that your projects using Colore are also compiled against a matching platform.

Razer's SDK installer **will only install the library relevant for your platform**.

This means that your apps will need to be compiled twice, once for x86 platforms, and once for x64.
They must both be provided when you distribute your application, depending on what platform the user has.

*We are looking into ways to make this requirement optional or not needed at all, and instead manage loading dynamically in some fancy way.*

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
