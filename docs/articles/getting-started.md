# Getting Started
## 1. How to install Colore
The easiest way to include Colore into your project is to rightclick on your C# Project in the Visual Studio Solution Explorer and Choose "Manage NuGet Packages..."

From there you can search online for Packages.
The NuGet Package manager sadly looks way different depending on the Version of Visual Studio but it should be easy to find Colore.
Just search for "Colore" and install it.
After that you should see a Reference being added to "Corale.Colore".

## 2. How to make every Device the same Color
Now in your Form you can just add a button for example and with a doubleclick it will take you to the Forms Sourcecode and already created an Event for that button when it is clicked.

At the top of that file you should now include a ``using Corale.Colore.Core;`` otherwise you need to prefix everything with Corale.Colore.Core. I myself like to also add the following to the using statements to make sure I have the correct "Color". Otherwise it might interfere with System.Drawing:
``` C#
using ColoreColor = Corale.Colore.Core.Color;
```

Now you change the Event Method like the following:
``` C#
private void button1_Click(object sender, EventArgs e)
{
    // Sets all Chroma devices to red
    Chroma.Instance.SetAll(ColoreColor.Red);
    //without the usings it will look like that:
    // Corale.Colore.Core.Chroma.Instance.SetAll(Corale.Colore.Core.Color.Red);
}
```

If you then run your application and click the button you will see that all your chroma devices turned Red.

## 3. How to access Device Types
Chroma SDK does not allow you to access one single device (as far as I know) but you can access Device Types. For example Keyboards, Mouse, Keypads...

To do that with Colore here is an example for the Keyboard:

Again this can be done without any using but I think it's easier. So I'd add the following at the top of the file:

``` C#
using Corale.Colore.Razer.Keyboard;
```

In some cases it might be helpful to also map it to another name as we did with the ColoreColor, but for now it's fine.

Then change your method like that:
``` C#
private void button1_Click(object sender, EventArgs e)
{
    Chroma.Instance.Keyboard.SetKey(Key.A, ColoreColor.Red);
}
```

Now when you run it you will see that the "A" Key will glow Red.
So it's pretty easy to use Colore :)

To set a color on a Mouse for example:
``Chroma.Instance.Mouse.SetLed(Corale.Colore.Razer.Mouse.Led.Strip1, ColoreColor.Red);``

The following Devices are available:
            Chroma.Instance.Mousepad
            Chroma.Instance.Keypad
            Chroma.Instance.Headset
            Chroma.Instance.Keyboard
            Chroma.Instance.Mouse

As a mousepad doesn't contain a SetKey method take a look at point 5.

## 4. How to define custom Colors
You can of course define any color you like when you use "new ColoreColor(1.0, 1.0, 1.0)". The numbers are in the order Red, Green and Blue while 1.0 is full color and 0.0 is nothing. Which makes this White (Red Green and Blue glowing at full bright). You can also use color values which range from 0 - 255 (the usual color range) but make sure to use or cast your value to "byte" to call the correct constructor. This can cause some trouble if you aren't careful:

While this will work as expected as it's using the byte constructor: ``new ColoreColor(255,125,125)``
This will **NOT** work as expected: ``new ColoreColor(255.0,125.0,125.0)``

This does work as expected:

``` C#
int i = 255;
var color = new ColoreColor((byte)i, (byte)i, (byte)i);
```
Result: Color with R=255, G=255, B=255

While this does **NOT** work as expected:

``` C#
int i = 255;
var color = new ColoreColor(i, i, i);
```
Result: Color with R=1, G=1, B=1


## 5. How do I know which color is currently set on a Key / What if I don't want to set a Key but a specific row/column?
Internally Colore is storing colors in a Grid.
This Grid is then send to the Device.
Instead of using SetKey you can directly edit that Grid which will also cause an update to the device by doing the following:
``Chroma.Instance.Keyboard[Key.A] = ColoreColor.Red;``
This is the equivalent to SetKey above.

But you can also read the value from this Grid with this method. For example:
``` C#
private void button1_Click(object sender, EventArgs e)
{
    // Check if A currently is Red
    if (Chroma.Instance.Keyboard[Key.A] == ColoreColor.Red)
    {
        // If it is, then set it to Blue
        Chroma.Instance.Keyboard[Key.A] = ColoreColor.Blue;
    }
    else
    {
        // Otherwise set it to Red
        Chroma.Instance.Keyboard[Key.A] = ColoreColor.Red;
    }
}
```

In some cases you can even access a virtual Grid instead of for example Key.A you can set the Key in the second row from the top and the fifth column from the left to Red:
``Chroma.Instance.Keyboard[1, 4] = ColoreColor.Red;``
The first int is row and the second is column. Starting with 0! There even are Constants that allow you to loop through all Keys:

``` C#
private void button1_Click(object sender, EventArgs e)
{
    // Create a Random Generator
    Random random = new Random();

    // Loop through all Rows
    for (uint r = 0; r < Constants.MaxRows; r++)
    {
        //Loop through all Columns
        for (uint c = 0; c < Constants.MaxColumns; c++)
        {
            // Set the current row and column to the random color
            Chroma.Instance.Keyboard[r, c] = new ColoreColor(random.Next(256), random.Next(256), random.Next(256));
        }
    }
}
```


The same thing can be archived with "SetPosition":
``Chroma.Instance.Keyboard.SetPosition(1, 4, ColoreColor.Red);``
It depends on the device if it's available or not.

## 6. What about performance?
Now it gets a bit more advanced and some people might think it's not actually needed as the SDK is very fast. But I personally love to manage my grids myself. The following example works for every device and if you do an application which does many updates at once I'd recommend it as you can at first set everything before sending it to the Keyboard, Mouse, Mousepad... . As said in point 5 Colore does internally also store a Grid, but everytime you change a Key via SetKey or the index (Keyboard[Key.A]) it sends an update to the Keyboard. That means if you set multiple Keys at once (maybe even update the whole Keyboard) it does many many many requests to the SDK. Better way in my opinion is to set everything in the grid and then update it. That's what the following code does. As all Customs for the different types are called "Custom" it's easier to map them via a using at the top:
``` C#
using MousepadCustom = Corale.Colore.Razer.Mousepad.Effects.Custom;
```

Now the code in your method:
``` C#
private void button1_Click(object sender, EventArgs e)
{
    // Create the custom Grid
    var customGrid = MousepadCustom.Create();
    // Set LED 0 (top right) and LED 14 (top left) to red
    customGrid[0] = ColoreColor.Red;
    customGrid[14] = ColoreColor.Red;
    // Apply the Grid to the Keyboard
    Chroma.Instance.Mousepad.SetCustom(customGrid);
}
```

The using helps if you now want to also use a Keyboard Custom for example:
``` C#
using KeyboardCustom = Corale.Colore.Razer.Keyboard.Effects.Custom;
```

``` C#
private void button1_Click(object sender, EventArgs e)
{
    // Create the custom Grid
    var customGrid = MousepadCustom.Create();
    // Set LED 0 (top right) and LED 14 (top left) to red
    customGrid[0] = ColoreColor.Red;
    customGrid[14] = ColoreColor.Red;
    // Apply the Grid to the Keyboard
    Chroma.Instance.Mousepad.SetCustom(customGrid);

    // Create a custom for the Keyboard
    var keyboardGrid = KeyboardCustom.Create();
    // Set the whole Grid to Green
    keyboardGrid.Set(ColoreColor.Green);
    // Set the A Key to Red
    keyboardGrid[Key.A] = ColoreColor.Red;
    // Set the Key in the second row and the fifth column to Red
    keyboardGrid[1,4] = ColoreColor.Red;
    // Apply the grid to the Keyboard
    Chroma.Instance.Keyboard.SetCustom(keyboardGrid);
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

Chroma.Instance.Keyboard.SetCustom(keyboardGrid);
```
