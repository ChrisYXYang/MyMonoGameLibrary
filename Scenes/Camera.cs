using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Tools;

namespace MyMonoGameLibrary.Scenes;

// camera for the game world.
public static class Camera
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
        return (coordinate - 
            new Vector2(Core.GraphicsDevice.PresentationParameters.BackBufferWidth, 
            Core.GraphicsDevice.PresentationParameters.BackBufferHeight) * 0.5f) / UnitPixels
            + position;
    }

    // converts gme unit coordinates to pixel coordinates
    //
    // param: coordinate - game unit coordintae
    // return: pixel coordinate
    public static Vector2 UnitToPixel(Vector2 coordinate)
    {
        return (coordinate - position) * UnitPixels + 
            new Vector2(Core.GraphicsDevice.PresentationParameters.BackBufferWidth, 
            Core.GraphicsDevice.PresentationParameters.BackBufferHeight) * 0.5f;
    }

    // draw texture in game world.
    //
    // param: texture - texture
    // param: position - position
    // param: sourceRect - source rectangle
    // param: color - color
    // parm: rotation - rotation
    // param: origin - origin point
    // param: scale - scale
    // param: spriteEffects - sprite effects
    // param: layerDepth - layerDepth
    public static void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color, 
        float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects, float layerDepth)
    {
        Core.SpriteBatch.Draw
            (
                texture,
                Camera.UnitToPixel(position),
                sourceRect,
                color,
                rotation,
                origin,
                Camera.PixelScale * scale,
                spriteEffects,
                layerDepth
            );
    }

    // draw texture in game world. 
    //
    // param: texture - texture
    // param: position - position
    // param: sourceRect - source rectangle
    // param: color - color
    // parm: rotation - rotation
    // param: origin - origin point
    // param: scale - scale
    // param: spriteEffects - sprite effects
    // param: layerDepth - layerDepth
    public static void Draw(Texture2D texture, Vector2 position, Rectangle? sourceRect, Color color,
        float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth)
    {
        Core.SpriteBatch.Draw
            (
                texture,
                Camera.UnitToPixel(position),
                sourceRect,
                color,
                rotation,
                origin,
                Camera.PixelScale * scale,
                spriteEffects,
                layerDepth
            );
    }

    // draw sprite in game world
    //
    // param: position - position
    // param: color - color
    // parm: rotation - rotation
    // param: scale - scale
    // param: spriteEffects - sprite effects
    // param: layerDepth - layerDepth
    public static void Draw(Sprite sprite, Vector2 position, Color color, float rotation, Vector2 scale,
        SpriteEffects spriteEffects, float layerDepth)
    {
        sprite.GameDraw(position, color, rotation, scale, spriteEffects, layerDepth);
    }

    // draw sprite in game world
    //
    // param: position - position
    // param: color - color
    // parm: rotation - rotation
    // param: scale - scale
    // param: spriteEffects - sprite effects
    // param: layerDepth - layerDepth
    public static void Draw(Sprite sprite, Vector2 position, Color color, float rotation, float scale,
        SpriteEffects spriteEffects, float layerDepth)
    {
        sprite.GameDraw(position, color, rotation, scale, spriteEffects, layerDepth);
    }

    // draw object in game world
    //
    // param: renderer - the renderer
    public static void Draw(IGameRenderer renderer)
    {
        renderer.Draw();
    }
}
