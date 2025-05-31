using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;


namespace MyMonoGameLibrary.Input;

// manages mouse input
public class MouseInfo
{
    // variables and properties
    public MouseState PreviousState { get; private set; }
    public MouseState CurrentState { get; private set; }
    public Point Position => CurrentState.Position;
    public int X => CurrentState.X;
    public int Y => CurrentState.Y;
    public Point PositionDelta => CurrentState.Position - PreviousState.Position;
    public int XDelta => CurrentState.X - PreviousState.X;
    public int YDelta => CurrentState.Y - PreviousState.Y;
    public bool WasMoved => PositionDelta != Point.Zero;
    public int ScrollWheel => CurrentState.ScrollWheelValue;
    public int ScrollWheelDelta => CurrentState.ScrollWheelValue- PreviousState.ScrollWheelValue;
    
    // constructor
    public MouseInfo()
    {
        PreviousState = new MouseState();
        CurrentState = Mouse.GetState();
    }

    // updates mouse input information
    public void Update()
    {
        PreviousState = CurrentState;
        CurrentState = Mouse.GetState();
    }

    // checks if mouse button is down
    //
    // param: button - mouse button
    // return: is button down
    public bool IsButtonDown(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return CurrentState.LeftButton == ButtonState.Pressed;
            case MouseButton.Middle:
                return CurrentState.MiddleButton == ButtonState.Pressed;
            case MouseButton.Right:
                return CurrentState.RightButton == ButtonState.Pressed;
            default:
                return false;
        }
    }

    // checks if mouse button is up
    //
    // param: button - mouse button
    // return: is mouse button up
    public bool IsButtonUp(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return CurrentState.LeftButton == ButtonState.Released;
            case MouseButton.Middle:
                return CurrentState.MiddleButton == ButtonState.Released;
            case MouseButton.Right:
                return CurrentState.RightButton == ButtonState.Released;
            default:
                return false;
        }
    }

    // checks if mouse button was just pressed
    //
    // param: button - mouse button
    // return: was button just pressed
    public bool WasButtonJustPressed(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return CurrentState.LeftButton == ButtonState.Pressed && PreviousState.LeftButton == ButtonState.Released;
            case MouseButton.Middle:
                return CurrentState.MiddleButton == ButtonState.Pressed && PreviousState.MiddleButton == ButtonState.Released;
            case MouseButton.Right:
                return CurrentState.RightButton == ButtonState.Pressed && PreviousState.RightButton == ButtonState.Released;
            default:
                return false;
        }
    }

    // checks if mouse button was just released
    //
    // param: button - mouse button
    // return: was mouse button just released
    public bool WasButtonJustReleased(MouseButton button)
    {
        switch (button)
        {
            case MouseButton.Left:
                return CurrentState.LeftButton == ButtonState.Released && PreviousState.LeftButton == ButtonState.Pressed;
            case MouseButton.Middle:
                return CurrentState.MiddleButton == ButtonState.Released && PreviousState.MiddleButton == ButtonState.Pressed;
            case MouseButton.Right:
                return CurrentState.RightButton == ButtonState.Released && PreviousState.RightButton == ButtonState.Pressed;
            default:
                return false;
        }
    }
}
