using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;

namespace MyMonoGameLibrary;

// Responsible for the "size" of the view of the game and the rendering of the sprites.
public class Camera
{
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
        return (coordinate + (new Vector2(Core.Graphics.PreferredBackBufferWidth, Core.Graphics.PreferredBackBufferHeight) * 0.5f)) / UnitPixels;
    }

    // converts gme unit coordinates to pixel coordinates
    //
    // param: coordinate - game unit coordintae
    // return: pixel coordinate
    public static Vector2 UnitToPixel(Vector2 coordinate)
    {
        return ((coordinate) * UnitPixels) + (new Vector2(Core.Graphics.PreferredBackBufferWidth, Core.Graphics.PreferredBackBufferHeight) * 0.5f);
    }
}
