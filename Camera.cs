using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Tools;

namespace MyMonoGameLibrary;

// Responsible for the "view" of the game. This includes where in the game world to render and
// sprite size settings.
public class Camera
{
    // position of the camera in the game world
    public static Vector2 position = Vector2.Zero;    

    // number of pixels per sprite image pixel
    public static int PixelScale { get; private set; } = 10;
    
    // number of sprite image pixels in a game unit.
    public static int SpritePixelsPerUnit { get; private set; } = 8;

    // number of pixels per game unit
    public static int UnitPixels => SpritePixelsPerUnit * PixelScale;

    // converts pixel coordinates to game unit coordinate
    //
    // param: coordinate - pixel coordintae
    // return: game unit coordinate
    public static Vector2 PixelToUnit(Vector2 coordinate)
    {
        return ((coordinate - 
            (new Vector2(DebugMode.GraphicsDevice.PresentationParameters.BackBufferWidth, 
            Core.GraphicsDevice.PresentationParameters.BackBufferHeight) * 0.5f)) / UnitPixels)
            + position;
    }

    // converts gme unit coordinates to pixel coordinates
    //
    // param: coordinate - game unit coordintae
    // return: pixel coordinate
    public static Vector2 UnitToPixel(Vector2 coordinate)
    {
        return ((coordinate - position) * UnitPixels) + 
            (new Vector2(Core.GraphicsDevice.PresentationParameters.BackBufferWidth, 
            Core.GraphicsDevice.PresentationParameters.BackBufferHeight) * 0.5f);
    }
}
