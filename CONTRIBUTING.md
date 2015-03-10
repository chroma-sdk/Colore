# Contributing

We deeply appreciate anyone wanting to contribute to Colore!

In order to make the code neat, readable, and organized properly, there are some guidelines and rules you must follow.

Always remember, if you are unsure about something: check the existing source to get an idea of how things should look!

If you have more specific questions about the guidelines or other things about the project, feel free to contact anyone
listed under `PRIMARY AUTHORS` in the [AUTHORS file][authors] or [create an issue][] in the [main repo][].

## Cloning the repo

First off, fork the repository and clone it to your local development system:

```
git clone git@github.com:<your-username>/Colore.git
```

## General design guidelines

 * Design your code to be event-driven when possible, it makes it easier for applications to implement the library.
 * Follow common C# practices for cleaner looking code (properties instead of `Get<Name>` methods, for example).

## StyleCop

Colore has a config file included for use with [StyleCop][], follow anything StyleCop tells you **if you are using the config file supplied with Colore**.

If StyleCop and this guide contradict each other, **always** follow what StyleCop tells you (assuming you are using the supplied config).

### StyleCop exception cases
During the early development of Colore, StyleCop may not be properly configured with all rules yet.
If you feel unsure about whether a certain StyleCop rule should *really* be followed, feel free to [create an issue][] with your question.

You may also submit a pull request with your changes and we will notify you if something looks wrong.

## Mandatory file header

The following file header **must be present in every code file**, if it is missing your code will not be accepted until the header has been put in place.

```
// ---------------------------------------------------------------------------------------
// <copyright file="{NAME OF FILE WITH EXTENSION}" company="Corale">
//     Copyright © 2015 by Adam Hellberg and Brandon Scott.
//
//     Permission is hereby granted, free of charge, to any person obtaining a copy of
//     this software and associated documentation files (the "Software"), to deal in
//     the Software without restriction, including without limitation the rights to
//     use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
//     of the Software, and to permit persons to whom the Software is furnished to do
//     so, subject to the following conditions:
//
//     The above copyright notice and this permission notice shall be included in all
//     copies or substantial portions of the Software.
//
//     THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//     IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//     FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//     AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY,
//     WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
//     CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//
//     Disclaimer: Corale and/or Colore is in no way affiliated with Razer and/or any
//     of its employees and/or licensors. Corale, Adam Hellberg, and/or Brandon Scott
//     do not take responsibility for any harm caused, direct or indirect, to any
//     Razer peripherals via the use of Colore.
//
//     "Razer" is a trademark of Razer USA Ltd.
// </copyright>
// ---------------------------------------------------------------------------------------
```

## Indenting

Use 4 **spaces** for indenting (**not tabs!**). No more, no less. Every decent IDE (and even text editor) lets you change this.

Code with incorrect indentation will not be accepted until corrected.

## Using declarations

*StyleCop will notify you about incorrect using statements.*

Put using statements as deep as possible (usually it will be just after the namespace declaration) and in alphabetical order.

`System.*` namespaces are always put at the top in their own alphabetical order.

### Example showing correctly sorted using statements

```c#
using System;

using Colore.Core;

using SomeAuthor.SomeLib;
```

(Space between using groups is optional, but can often make it easier to read.)

## Naming

StyleCop will tell you how to name things most of the time. The only thing worth mentioning is that private fields are named by prefixing them with an underscore. The only exception is for `const` and `static readonly` in which case they are named using PascalCase.

```c#
// Naming example for a private variable
private SomeType _myVar;

// Naming example for a private static readonly
private static readonly SomeType MyVar;
```

If you are using ReSharper, it will manage this for you, as we are using its default naming scheme.

### Exceptions to naming convention

You should always try to name everything in a C#-idiomatic way, but we may make exceptions for exceptional cases, like when importing native code fragments.

## Exception throwing and handling

Try not to catch the base `Exception` class, always catch specific exceptions that you are certain your code can handle.

When throwing exceptions, throw a type that is relevant to the error that occurred, don't throw a base `Exception` if an argument is `null`, throw the `ArgumentNullException`.

If creating custom exception types, append `Exception` to the name and inherit from the relevant base exception class, as per usual C# guidelines. Try to keep exception names short but descriptive.

## Usage of the `var` keyword

With fancy IDEs and other tools, the need to explicitly specify the type of a variable is not as important.

If you look through the source, you'll find that the `var` keyword is used extensively.

However, whether or not you use the `var` keyword doesn't matter too much. If you find a piece of code more readable with the full type name, then write the full type name.

Use your own judgement to decide whether using the `var` keyword makes your code more readable.

That said, there are situations where using the `var` keyword is strongly encouraged, when the type name is mentioned in the statement (typically on assignment operations):

```c#
// Consider this line of code:
SpecialSuperType[] myArray = new SpecialSupertype[10];
// here it would be preferred to use the var keyword, as the type is already mentioned in the assignment
var myArray = new SpecialSuperType[10];
```

## Calling native Chroma SDK functions

**Never call native SDK functions directly**. If implementing new SDK functions, add a wrapper function for them in [NativeWrapper.cs][nativewrapper] first, then call them using that wrapper. Look at the existing code in the file for an idea of how they are written. The bare requirement for a wrapper function is that it must encapsulate and isolate the API call fully and handle the returned result value, throwing a `NativeCallException` if it failed.

## Documenting your code

Public-facing components of your code **must** have documentation. We will not merge pull requests that are missing documentation on public members.

Use standard C# XML documentation comments for documenting your code, like so:

```c#
/// <summary>
/// Opens a webpage.
/// </summary>
/// <param name="url">The URL to open.</param>
/// <returns><c>true</c> if the webpage opened, <c>false</c> otherwise.</returns>
public bool OpenWebpage(string url)
{
    // Code...
}
```

If you are using Visual Studio, it will automatically insert the proper tags for you when you type three slashes where applicable (`///`).

Please use proper grammar and punctuation in your documentation comments for the convenience of the reader. We understand that not everyone is a native English speaker or have perfect language skills, and so we are pretty relaxed with the proper-ness of documentation comments.

Simply: Badly written documentation comments are still better than no documentation comments at all. The important thing is that you can write something understandable. We will tell you in the pull request if there's something you can improve before we merge it.

If you're referencing/importing/wrapping functions from external sources like the WinAPI or Razer's APIs you can simply copy documentation from the original source (MSDN for example). This usually means you get properly written documentation that is easy (ok, most of the time anyway) to understand.

## Submitting a pull request

Good, we didn't scare you off with all these requirements, and you are on your way to submitting your first pull request! (Hopefully).

When making a pull request, if is often convenient to make a branch just for that request (so that you can continue working on your main development branch while your pull request is being reviewed). These branches are typically named `patch-<counter>` but can be anything you desire, like a descriptive name for what the PR is about. If you decide to name branches based on the change, make sure you keep them quite short.

Another very positive thing for both you and us, the maintainers, is to avoid pull requests with a lot of commits that don't share anything in common. Imagine if you submit a pull request that implements a car and a bicycle. We decide that we like your car implementation, but not the bicycle one. But we can't choose, as you put them both in the same pull request!

So always make sure that your pull request is handling **one type of feature or change**.

Other things to keep in mind is to include details about what your feature or change is about in the pull request message. The more information the better! And make sure to give your pull request a relevant name, too.

### Pull request target branch

When submitting your pull request, make sure you are targeting the [`develop`][develop] branch of the main repo. If you target the wrong branch we will close your pull request. Don't feel bad though, just re-create it with the correct target branch! (We have to close it as GitHub does not support changing the target branch on an existing pull request.)

[AUTHORS]: AUTHORS
[create an issue]: https://github.com/CoraleStudios/Colore/issues/new
[main repo]: https://github.com/CoraleStudios/Colore
[StyleCop]: http://stylecop.codeplex.com/
[nativewrapper]: Colore/Core/NativeWrapper.cs
[develop]: https://github.com/CoraleStudios/Colore/tree/develop
