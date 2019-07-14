---
uid: getting-started
---

# Getting Started

[![NuGet version][ngverbadge]][ng]
[![MyGet version][mgverbadge]][mg]

## 1. Installing Chroma SDK
Razer Chroma SDK is a C++-tool which allows us to access Chroma devices. It is the backend (or native/unmanaged code) behind Colore. It is expected to be automatically installed with Synapse once you plug in a Chroma device, but in some cases that might not work (see [#263](https://github.com/chroma-sdk/Colore/issues/263#)). As a last resort, you can also find an SDK installer in their [Setting Up](https://developer.razer.com/works-with-chroma/setting-up/) guide.

## 2. How to install Colore 
The easiest way include Colore into your project is to right click on your C# Project in the Visual Studio Solution Explorer and Choose "Manage NuGet Packages..."

From there you can search online for Packages. Just search for "Colore" and install it. After that you should see a reference being added to "Colore".

### Pre-release versions
If you want to test the absolute latest features in Colore, which may not be fully ready for production use yet, you can install pre-release Colore packages from [our MyGet feed][mg]. You can either install a version manually with the commands listed on the page, or add the feed to your NuGet settings: `https://www.myget.org/F/chroma-sdk/api/v3/index.json`.

Make sure to select the MyGet feed when you are browsing packages in the package manager (or select "All" to see packages from all feeds at once), and to check the "Include pre-releases" checkbox. The MyGet feed will also include stable versions of Colore when they are released, so you can use it as the sole source for Colore.

## 3. How to make every device the same color
You can add a button in your WPF-Form using the toolbox (Ctrl+Alt+X). With a double-click it will take you to the form's source code and to an already created event handler for that button.

At the top of that file you should now add `using Colore;` to access the elements of the `Colore` namespace without having to prefix so much. In addition, I myself like to add the following alias at the top to make sure I have the correct ["Color"](xref:Colore.Data.Color). Otherwise it might interfere with `System.Drawing`:

``` C#
using ColoreColor = Colore.Data.Color;
```
Modify the Event Method as follows:
``` C#
private async void button1_Click(object sender, RoutedEventArgs e)
{
    // Sets all Chroma devices to red, the 'chroma' variable should be saved
    // somewhere that is globally accessible to the application.
    var chroma = await ColoreProvider.CreateNativeAsync();
    await chroma.SetAllAsync(ColoreColor.Red);
    
    // Without the usings it will look like this:
    // var chroma = await Colore.ColoreProvider.CreateNativeAsync();
    // await chroma.SetAllAsync(Colore.Data.Color.Red);
}
```

If you then run your application and click the button you will see that all your chroma devices turned red.
What `await` does is waiting for the initializiation of the Chroma SDK to finish before proceding. Methods using `await` not in the last line must be marked ss `async` (asynchronous) and thus allow the usage of `await` on the method itself. How this works under the hood, is that [Task](https://docs.microsoft.com/dotnet/csharp/programming-guide/concepts/async/) objects are returned instead of return values.

#### A note on [`ColoreProvider`]((xref:Colore.ColoreProvider)

Since version 6.0.0, Colore is designed to be asynchronous and less strict in how it is used.
This means that the [`Chroma`](xref:Colore.IChroma) instance is now obtained differently.
You can asynchronously instantiate either the native or REST API SDK in the following ways:

```c#
// Create the regular native SDK backend, like in version 5.x
ColoreProvider.CreateNativeAsync();

// When creating a Chroma instance using the REST API backend, you need to supply the SDK with information
// about your app.
var appInfo = new AppInfo("My app", "An awesome Chroma app!", "John Doe", "me@example.com", Category.Application);
ColoreProvider.CreateRestAsync(appInfo);
```

There are also overloads taking a custom REST API endpoint which can be useful for testing, and an overload taking
a bool parameter to control whether to use the SSL version of Razer's REST API.

Each time you create a new Chroma instance in this way using [`ColoreProvider`](xref:Colore.ColoreProvider), the previously created instance will
be uninitialized and discarded. For this reason, you should create your Chroma instance *once* at application startup
and then use this instance for the remainder of your application's lifetime.

For background applications that dynamically enable and disable Chroma features however, you can call the
[`UninitializeAsync`](xref:Colore.IChroma#Colore_IChroma_UninitializeAsync) and [`InitializeAsync`](xref:Colore.IChroma#Colore_IChroma_InitializeAsync_Colore_Data_AppInfo_) methods to control the lifetime. (Note that uninitializing the SDK
manually this way doesn't always work properly with the SDK, and can sometimes leave it in a weird state.)

## 4. How to access specific device types
Chroma SDK does not allow you to access one single device (as far as we know) but you can access specific device types like Keyboard, Mouse, Keypad etc.

Here is an example for the Keyboard:

Again, accessing namespaces becomes much easier with `using` directives:

``` C#
using Colore.Effects.Keyboard;
```

In some cases it might be helpful to add an alias as we did with ColoreColor.

Then change your method like this:
``` C#
private async void button1_Click(object sender, EventArgs e)
{
    var chroma = await ColoreProvider.CreateNativeAsync();
    await chroma.Keyboard.SetKeyAsync(Key.A, ColoreColor.Red);
}
```

(From now on, it will be assumed that the variable `chroma` contains an instance of [`IChroma`](xref:Colore.IChroma) created from [`ColoreProvider`](xref:Colore.ColoreProvider).)

Now when you run it you'll see that the "A" Key is red.

To set a color on a Mouse for example:
[`chroma.Mouse[Colore.Effects.Mouse.Led.Strip1] = ColoreColor.Red;`](xref:Colore.IMouse#Colore_IMouse_Item_Colore_Effects_Mouse_GridLed_)

The following devices are available as properties on the [`IChroma`](xref:Colore.IChroma) instance:

 * [`Mousepad`](xref:Colore.IMousepad)
 * [`Keypad`](xref:Colore.IKeypad)
 * [`Headset`](xref:Colore.IHeadset)
 * [`Keyboard`](xref:Colore.IKeyboard)
 * [`Mouse`](xref:Colore.IMouse)

Since Mousepad doesn't contain a SetKey method, you can take a look at section 6.

## 5. How to define custom Colors
You can instantiate [`ColoreColor`](xref:Colore.Data.Color#Colore_Data_Color__ctor_System_Double_System_Double_System_Double_) with RGB values ranging from  0 to 1. You can also use intensities from 0 to 255 (the usual range) but make sure to cast your value to `byte` to call the correct constructor. This can cause some trouble if you are not careful:

This will work as it's using the byte constructor: [`new ColoreColor(255, 125, 125)`](xref:Colore.Data.Color#Colore_Data_Color__ctor_System_Byte_System_Byte_System_Byte_).

This will *not* work: [`new ColoreColor(255.0, 125.0, 125.0)`](xref:Colore.Data.Color#Colore_Data_Color__ctor_System_Double_System_Double_System_Double_).

Example:

``` C#
int i = 255;
var color = new ColoreColor((byte)i, (byte)i, (byte)i);
```
Result: Color with R=255, G=255, B=255

While this does *not* work as expected (the integers are coerced into floating point numbers):

``` C#
int i = 255;
var color = new ColoreColor(i, i, i);
```
Result: Color with R=1, G=1, B=1


## 6. How do I know which color is currently set on a Key / What if I don't want to set a Key but a specific row/column?
Internally Colore is storing colors in a grid.
This grid is then send to the device.
Instead of using SetKey you can directly edit that grid which will update the device by doing the following:
[`chroma.Keyboard[Key.A] = ColoreColor.Red;`](xref:Colore.IKeyboard#Colore_IKeyboard_Item_Colore_Effects_Keyboard_Key_)
This is the equivalent to SetKey above.

Moreover, you can *read* the value using the grid. For example:
``` C#
private void button1_Click(object sender, EventArgs e)
{
    // Check if A currently is Red
    if (chroma.Keyboard[Key.A] == ColoreColor.Red)
    {
        // If it is, then set it to Blue
        chroma.Keyboard[Key.A] = ColoreColor.Blue;
    }
    else
    {
        // Otherwise set it to Red
        chroma.Keyboard[Key.A] = ColoreColor.Red;
    }
}
```

In some cases you can even access a virtual grid instead of Key. Then for instance you can set the Key in the second row (top to bottom) and the fifth column (left to right) to red:
[`Chroma.Instance.Keyboard[1, 4] = ColoreColor.Red;`](xref:Colore.IKeyboard#Colore_IKeyboard_Item_System_Int32_System_Int32_)
Starting with zero, the first int represents the row and the second one is for the column. There are special constants that allow you to loop through all keys:

``` C#
private async void button1_Click(object sender, EventArgs e)
{
    var chroma = await ColoreProvider.CreateNativeAsync();

    // Create a Random Generator
    Random random = new Random();

    // Loop through all Rows
    for (uint r = 0; r < KeyboardConstants.MaxRows; r++)
    {
        //Loop through all Columns
        for (uint c = 0; c < KeyboardConstants.MaxColumns; c++)
        {
            // Set the current row and column to the random color
            chroma.Keyboard[r, c] = new ColoreColor(random.Next(256), random.Next(256), random.Next(256));
        }
    }
}
```

The same thing can be achieved with [`SetPositionAsync`](xref:Colore.IKeyboard#Colore_IKeyboard_SetPositionAsync_System_Int32_System_Int32_Colore_Data_Color_System_Boolean_):
`chroma.Keyboard.SetPositionAsync(1, 4, ColoreColor.Red);` This method is not available for all devices.

## 7. What about performance?
Chroma SDK and Colore are fast. If you do a lot of updates at the same time to your targeted devices you can set up a custom grid and send the commands afterwards. The following example works for every device. As we have pointed out in chapter 6, Colore internally stores a grid, but everytime you change a key via SetKey or the index (Keyboard[Key.A]) it sends an update to your device, making a lot of requests to the SDK. A better way in our opinion is to set up the colors in a grid and then update it.

Example:
``` C#
private async void button1_Click(object sender, EventArgs e)
{
    var chroma = await ColoreProvider.CreateNativeAsync();

    // Create the custom grid
    var customGrid = MousepadCustom.Create();
    // Set LED 0 (top right) and LED 14 (top left) to red
    customGrid[0] = ColoreColor.Red;
    customGrid[14] = ColoreColor.Red;
    // Apply the Grid to the Mousepad
    await chroma.Mousepad.SetCustomAsync(customGrid);

    // Create a custom for the Keyboard
    var keyboardGrid = KeyboardCustom.Create();
    // Set the whole Grid to Green
    keyboardGrid.Set(ColoreColor.Green);
    // Set the A Key to Red
    keyboardGrid[Key.A] = ColoreColor.Red;
    // Set the Key in the second row and the fifth column to Red
    keyboardGrid[1,4] = ColoreColor.Red;
    // Apply the grid to the Keyboard
    await chroma.Keyboard.SetCustomAsync(keyboardGrid);
}
```

So the random colors example from chapter 6 will look like the following resulting in just one request per button to the SDK instead of many.

``` C#
// Instantiate a Random Generator
Random random = new Random();
// Create the custom grid
var keyboardGrid = KeyboardCustom.Create();

// Loop through all rows
for (var r = 0; r < Constants.MaxRows; r++)
{
    //Loop through all columns
    for (var c = 0; c < Constants.MaxColumns; c++)
    {
        // Set the current element to the random color
        keyboardGrid[r, c] = new ColoreColor(random.Next(256), random.Next(256), random.Next(256));
    }
}

await chroma.Keyboard.SetCustomAsync(keyboardGrid);
```

## Credits

*Big thanks to [WolfspiritM](https://github.com/WolfspiritM) who initially created this guide!*

[ng]: https://www.nuget.org/packages/Colore
[ngverbadge]: https://img.shields.io/nuget/v/Colore.svg
[mg]: https://www.myget.org/feed/chroma-sdk/package/nuget/Colore
[mgverbadge]: https://img.shields.io/myget/chroma-sdk/vpre/Colore.svg?label=myget
