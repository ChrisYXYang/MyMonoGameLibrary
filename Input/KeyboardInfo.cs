using System;
using Microsoft.Xna.Framework.Input;

namespace MyMonoGameLibrary.Input;

// manages keyboard input
public class KeyboardInfo
{
    // variables and properties
    public KeyboardState PreviousState { get; private set; }
    public KeyboardState CurrentState { get; private set; }

    // constructor
    public KeyboardInfo()
    {
        PreviousState = new KeyboardState();
        CurrentState = Keyboard.GetState();
    }
    
    // updates keyboard states
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Keyboard.GetState();
    }

    // checks if key is down
    //
    // param: key - key to check
    // return: is key down
    public bool IsKeyDown(Keys key)
    {
        return CurrentState.IsKeyDown(key);
    }

    // checks if key is up
    //
    // param: key - key to check
    // return: is key up
    public bool IsKeyUp(Keys key)
    {
        return CurrentState.IsKeyUp(key);
    }

    // checks if key was just pressed
    //
    // param: key - key to check
    // return: was key just pressed
    public bool WasKeyJustPressed(Keys key)
    {
        return CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
    }

    // checks if key was just released
    //
    // param: key - key to check
    // return: was key just release
    public bool WasKeyJustReleased(Keys key)
    {
        return CurrentState.IsKeyUp(key) && PreviousState.IsKeyDown(key);
    }
}
