---
uid: getting-started
---

# Getting Started

## 1. How to install Colore
[![NuGet version][ngverbadge]][ng]
[![MyGet version][mgverbadge]][mg]

The easiest way to include Colore into your project is to right click on your C# Project in the Visual Studio Solution Explorer and Choose "Manage NuGet Packages..."

From there you can search online for Packages.
The NuGet Package manager sadly looks way different depending on the Version of Visual Studio but it should be easy to find Colore.
Just search for "Colore" and install it.
After that you should see a Reference being added to "Colore".

### Pre-release versions
If you want to test the absolute latest features in Colore, which may not be fully ready for production use yet, you can install pre-release Colore packages from [our MyGet feed][mg]. You can either install a version manually with the commands listed on the page, or add the feed to your NuGet settings: `https://www.myget.org/F/coralestudios/api/v3/index.json`.

Make sure to select the MyGet feed when you are browsing packages in the package manager (or select "All" to see packages from all feeds at once), and to check the "Include pre-releases" checkbox. The MyGet feed will also include stable versions of Colore when they are released, so you can use it as the sole source for Colore.

## 2. How to make every Device the same Color
Now in your Form you can just add a button for example and with a double-click it will take you to the form's source code and already created an event handler for that button when it is clicked.

At the top of that file you should now include a `using Colore;` otherwise you need to prefix everything with `Colore`. I myself like to also add the following to the using statements to make sure I have the correct "[Color](xref:Colore.Data.Color)". Otherwise it might interfere with `System.Drawing`:

``` C#
using ColoreColor = Colore.Data.Color;
```

Now you change the Event Method like the following:
``` C#
private async void button1_Click(object sender, EventArgs e)
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

If you then run your application and click the button you will see that all your chroma devices turned Red.

### A note on [`ColoreProvider`](xref:Colore.ColoreProvider)

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

## 3. How to access Device Types
Chroma SDK does not allow you to access one single device (as far as we know) but you can access Device Types. For example Keyboards, Mouse, Keypads...

To do that with Colore here is an example for the Keyboard:

Again this can be done without any using but we consider it easier and more readable. So add the following at the top of the file:

``` C#
using Colore.Effects.Keyboard;
```

In some cases it might be helpful to also map it to another name as we did with the @"Colore.Data.Color?text=Color", but for now it's fine.

Then change your method like this:
``` C#
private async void button1_Click(object sender, EventArgs e)
{
    var chroma = await ColoreProvider.CreateNativeAsync();
    await chroma.Keyboard.SetKeyAsync(Key.A, ColoreColor.Red);
}
```

(From now on, it will be assumed that the variable `chroma` contains an instance of [`IChroma`](xref:Colore.IChroma) created from [`ColoreProvider`](xref:Colore.ColoreProvider).)

Now when you run it you will see that the "A" Key will glow Red.
So it's pretty easy to use Colore :)

To set a color on a Mouse for example:
[`chroma.Mouse[Colore.Effects.Mouse.Led.Strip1] = ColoreColor.Red;`](xref:Colore.IMouse#Colore_IMouse_Item_Colore_Effects_Mouse_GridLed_)

The following Devices are available as properties on the [`IChroma`](xref:Colore.IChroma) instance:

 * [`Mousepad`](xref:Colore.IMousepad)
 * [`Keypad`](xref:Colore.IKeypad)
 * [`Headset`](xref:Colore.IHeadset)
 * [`Keyboard`](xref:Colore.IKeyboard)
 * [`Mouse`](xref:Colore.IMouse)

As a mousepad doesn't contain a SetKey method take a look at section 5.

## 4. How to define custom Colors
You can of course define any color you like when you use [`new ColoreColor(1.0, 1.0, 1.0)`](xref:Colore.Data.Color#Colore_Data_Color__ctor_System_Double_System_Double_System_Double_). The numbers are in the order red, green, and blue where `1.0` is full color and `0.0` is nothing. Which makes this white (red, green and blue glowing at full bright). You can also use color values which range from 0 - 255 (the usual color range) but make sure to use or cast your value to `byte` to call the correct constructor. This can cause some trouble if you aren't careful:

This will work as expected as it's using the byte constructor: [`new ColoreColor(255, 125, 125)`](xref:Colore.Data.Color#Colore_Data_Color__ctor_System_Byte_System_Byte_System_Byte_)
This will **NOT** work as expected: [`new ColoreColor(255.0, 125.0, 125.0)`](xref:Colore.Data.Color#Colore_Data_Color__ctor_System_Double_System_Double_System_Double_)

This does work as expected:

``` C#
int i = 255;
var color = new ColoreColor((byte)i, (byte)i, (byte)i);
```
Result: Color with R=255, G=255, B=255.

While this does **NOT** work as expected (the integers are coerced into floating point numbers):

``` C#
int i = 255;
var color = new ColoreColor(i, i, i);
```
Result: Color with R=1, G=1, B=1.


## 5. How do I know which color is currently set on a Key / What if I don't want to set a Key but a specific row/column?
Internally Colore is storing colors in a grid.
This Grid is then send to the Device.
Instead of using `SetKey` you can directly edit that grid which will also cause an update to the device by doing the following:
[`chroma.Keyboard[Key.A] = ColoreColor.Red;`](xref:Colore.IKeyboard#Colore_IKeyboard_Item_Colore_Effects_Keyboard_Key_)
This is the equivalent to SetKey above.

But you can also read the value from this Grid with this method. For example:
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

In some cases you can even access a virtual Grid instead of for example [`Key.A`](xref:Colore.Effects.Keyboard.Key#Colore_Effects_Keyboard_Key_A) you can set the Key in the second row from the top and the fifth column from the left to Red:
[`Chroma.Instance.Keyboard[1, 4] = ColoreColor.Red;`](xref:Colore.IKeyboard#Colore_IKeyboard_Item_System_Int32_System_Int32_)
The first int is row and the second is column, starting with `0`! There even are Constants that allow you to loop through all keys:

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


The same thing can be archived with [`SetPositionAsync`](xref:Colore.IKeyboard#Colore_IKeyboard_SetPositionAsync_System_Int32_System_Int32_Colore_Data_Color_System_Boolean_):
`chroma.Keyboard.SetPositionAsync(1, 4, ColoreColor.Red);`
It depends on the device if it's available or not.

## 6. What about performance?
Now it gets a bit more advanced and some people might think it's not actually needed as the SDK is very fast, but you can manage the grids myself. The following example works for every device and if you do an application which does many updates at once we recommend it as you can at first set everything before sending it to the keyboard, mouse, mousepad&hellip; As said in point 5 Colore does internally also store a grid, but everytime you change a Key via `SetKey` or the index (`Keyboard[Key.A]`) it sends an update to the Keyboard. That means if you set multiple Keys at once (maybe even update the whole keyboard) it does many many many requests to the SDK. A more performant way is to set everything in the grid and then update it. That's what the following code does.

Now the code in your method:
``` C#
private async void button1_Click(object sender, EventArgs e)
{
    var chroma = await ColoreProvider.CreateNativeAsync();
    
    // Create the custom Grid
    var customGrid = MousepadCustom.Create();
    // Set LED 0 (top right) and LED 14 (top left) to red
    customGrid[0] = ColoreColor.Red;
    customGrid[14] = ColoreColor.Red;
    // Apply the Grid to the Keyboard
    await chroma.Mousepad.SetCustomAsync(customGrid);
}
```

``` C#
private async void button1_Click(object sender, EventArgs e)
{
    var chroma = await ColoreProvider.CreateNativeAsync();

    // Create the custom Grid
    var customGrid = MousepadCustom.Create();
    // Set LED 0 (top right) and LED 14 (top left) to red
    customGrid[0] = ColoreColor.Red;
    customGrid[14] = ColoreColor.Red;
    // Apply the Grid to the Keyboard
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

So the random Colors from before will look like the following making instead of many requests to the SDK just one per button click:

``` C#
// Create a Random Generator
Random random = new Random();
// Create the custom Grid
var keyboardGrid = KeyboardCustom.Create();

// Loop through all Rows
for (var r = 0; r < Constants.MaxRows; r++)
{
    //Loop through all Columns
    for (var c = 0; c < Constants.MaxColumns; c++)
    {
        // Set the current row and column to the random color
        keyboardGrid[r, c] = new ColoreColor(random.Next(256), random.Next(256), random.Next(256));
    }
}

await chroma.Keyboard.SetCustomAsync(keyboardGrid);
```

## Credits

*Big thanks to [WolfspiritM](https://github.com/WolfspiritM) who initially created this guide!*

[ng]: https://www.nuget.org/packages/Colore
[ngverbadge]: https://img.shields.io/nuget/v/Colore.svg
[mg]: https://www.myget.org/feed/coralestudios/package/nuget/Colore
[mgverbadge]: https://img.shields.io/myget/coralestudios/vpre/Colore.svg?label=myget
