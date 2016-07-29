---
title: "Colore 5.0 released!"
date: 2016-07-26
author: sharparam
categories: release
---

[Colore 5.0 has been released][ghrel] to support the latest SDK features
and also integrate better with Unity!

The new release is available from both [GitHub][ghrel] and [NuGet][ng].

The perhaps biggest change in 5.0 is the separation of WPF and WinForms code
into separate projects, in order to make it easier to use Colore in Unity
projects. The new WPF and WinForms packages can be found on the
[GitHub release page][ghrel] as additional binary downloads, as well as
in the form of two new NuGet packages: [Colore.WPF][ngwpf] and
[Colore.WinForms][ngwinforms].

Colore will *also* be available from the [Unity Asset Store][uas] soon.
This will serve to make it even easier to use Colore for your
Unity projects!

Other than the mentioned big changes, the usual stuff is there as well.
Updates have been made to support new SDK features, and new devices that
have been added to the SDK.

Support for generic/system effects is now included in Colore, giving
many new endpoints to the `IGenericDevice` interface. Devices can be
obtained by calling `Chroma.Get` with a valid device GUID (which can
be found in the `Devices` class).

[ghrel]: https://github.com/CoraleStudios/Colore/releases/tag/v5.0.0
[ng]: https://www.nuget.org/packages/Colore
[ngwpf]: https://www.nuget.org/packages/Colore.WPF
[ngwinforms]: https://www.nuget.org/packages/Colore.WinForms
[uas]: https://www.assetstore.unity3d.com/
