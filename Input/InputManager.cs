using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Input;

// manages keyboard and mouse input.
public static class InputManager
{
    // variables and properties
    public static KeyboardInfo Keyboard { get; private set; } = new KeyboardInfo();
    public static MouseInfo Mouse { get; private set; } = new MouseInfo();

    // updates input states
    public static void Update()
    {
        Keyboard.Update();
        Mouse.Update();
    }
}
