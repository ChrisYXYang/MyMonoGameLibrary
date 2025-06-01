using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;

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
            (new Vector2(Core.GraphicsDevice.PresentationParameters.BackBufferWidth, 
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

    // draw a game object
    //
    // param: gameObject - game object to draw
    public static void Draw(GameObject gameObject)
    {
        // get relevant components
        Transform transform = gameObject.GetComponent<Transform>();
        SpriteManager spriteManager = gameObject.GetComponent<SpriteManager>();
        Sprite sprite = spriteManager.Sprite;

        // set sprite effects
        int spriteEffect = 0;
        if (spriteManager.FlipX) { spriteEffect += 1; }
        if (spriteManager.FlipY) { spriteEffect += 2; }

        // draw
        Core.SpriteBatch.Draw
            (
                sprite.SpriteSheet,
                Camera.UnitToPixel(transform.position),
                sprite.SourceRectangle,
                spriteManager.Color,
                transform.Rotation,
                sprite.OriginPoint,
                Camera.PixelScale * transform.Scale,
                (SpriteEffects)spriteEffect,
                spriteManager.LayerDepth
            );
    }
}
