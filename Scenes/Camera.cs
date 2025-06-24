using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Tools;
using MyMonoGameLibrary.Tilemap;
using System.Collections.Generic;
using System.Xml.Linq;

namespace MyMonoGameLibrary.Scenes;

// camera for the game world. Handles drawing game world objects
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
            (new Vector2(Core.GraphicsDevice.PresentationParameters.BackBufferWidth, 
            Core.GraphicsDevice.PresentationParameters.BackBufferHeight) * 0.5f);
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
                UnitToPixel(position),
                sourceRect,
                color,
                rotation,
                origin,
                PixelScale * scale,
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
                UnitToPixel(position),
                sourceRect,
                color,
                rotation,
                origin,
                PixelScale * scale,
                spriteEffects,
                layerDepth
            );
    }

    // draw text into game world
    //
    // param: font - the font
    // param: text - the text
    // param: position - position of text
    // param: color - color
    // parm: rotation - rotation
    // param: origin - origin point
    // param: scale - scale
    // param: effects - sprite effects
    // param: layerDepth - layerDepth
    public static void Draw(SpriteFont font, string text, Vector2 position, Color color, float rotation,
        Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
    {
        Core.SpriteBatch.DrawString
            (
                font,
                text,
                UnitToPixel(position),
                color,
                rotation,
                origin,
                scale,
                effects,
                layerDepth
            );
    }

    // draw text into game world
    //
    // param: font - the font
    // param: text - the text
    // param: position - position of text
    // param: color - color
    // parm: rotation - rotation
    // param: origin - origin point
    // param: scale - scale
    // param: effects - sprite effects
    // param: layerDepth - layerDepth
    public static void Draw(SpriteFont font, string text, Vector2 position, Color color, float rotation,
        Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
    {
        Core.SpriteBatch.DrawString
            (
                font,
                text,
                UnitToPixel(position),
                color,
                rotation,
                origin,
                scale,
                effects,
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
        Draw
            (
                sprite.SpriteSheet,
                position,
                sprite.SourceRectangle,
                color,
                rotation,
                sprite.OriginPoint,
                scale,
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
    public static void Draw(Sprite sprite, Vector2 position, Color color, float rotation, float scale,
        SpriteEffects spriteEffects, float layerDepth)
    {
        Draw
            (
                sprite.SpriteSheet,
                position,
                sprite.SourceRectangle,
                color,
                rotation,
                sprite.OriginPoint,
                scale,
                spriteEffects,
                layerDepth
            );
    }

    // draw text renderer into game world
    //
    // param: renderer - the text renderer
    public static void Draw(TextRenderer renderer)
    {
        if (!renderer.IsVisible)
            return;

        // set sprite effects
        int spriteEffect = 0;
        if (renderer.FlipX)
            spriteEffect += 1;
        if (renderer.FlipY)
            spriteEffect += 2;

        Draw(renderer.Font, renderer.Text, renderer.ParentTransform.position, renderer.Color, 
            renderer.ParentTransform.Rotation, renderer.Origin, renderer.ParentTransform.Scale,
            (SpriteEffects)spriteEffect, renderer.LayerDepth);
    }

    // draw sprite renderers in game world
    //
    // param: renderer - the sprite renderer
    public static void Draw(SpriteRenderer renderer)
    {
        if (!renderer.IsVisible)
            return;

        // set sprite effects
        int spriteEffect = 0;
        if (renderer.FlipX)
            spriteEffect += 1;
        if (renderer.FlipY)
            spriteEffect += 2;

        Draw(renderer.Sprite, renderer.ParentTransform.position, renderer.Color, renderer.ParentTransform.Rotation,
            renderer.ParentTransform.Scale, (SpriteEffects)spriteEffect, renderer.LayerDepth);
    }

    // draw renderer component into game world
    public static void Draw(RendererComponent renderer)
    {
        if (renderer is SpriteRenderer sr)
            Draw(sr);
        else if (renderer is TextRenderer tr)
            Draw(tr);
    }

    // draw a tilemap in game world
    //
    // param: tilemap - the tilemap to draw
    public static void Draw(TileMap tilemap)
    {
        List<string> layerNames = tilemap.Layers;

        foreach (string layerName in layerNames)
        {
            for (int i = 0; i < tilemap.Rows; i++)
            {
                for (int j = 0; j < tilemap.Columns; j++)
                {
                    Tile tile = tilemap.GetTile(layerName, i, j);

                    if (tile == null)
                        continue;

                    Draw(tile.Sprite, tile.Position, Color.White, 0f, Vector2.One, SpriteEffects.None, tile.LayerDepth);

                }
            }
        }
    }
}
