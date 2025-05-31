using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Input;

// manages keyboard and mouse input.
public class InputManager
{
    // variables and properties
    public KeyboardInfo Keyboard {  get; private set; } = new KeyboardInfo();
    public MouseInfo Mouse { get; private set; } = new MouseInfo();

    // updates input states
    public void Update()
    {
        Keyboard.Update();
        Mouse.Update();
    }
}
