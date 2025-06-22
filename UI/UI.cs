using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGameLibrary.UI;

// this class represents a UI element in the game. It will also manage the overall UI of the game.
public abstract class UI
{
    // the scale of all UI
    public static float CanvasScale;

    // position of UI element
    public Vector2 position;

    // scale of UI element
    public Vector2 Scale { get; set; }
    
    // color of UI
    public Color Color { get; set; }

    // rotation of UI
    public float Rotation { get; set; }

    // flip horizontally
    public bool FlipX { get; set; }

    // flip vertically
    public bool FlipY { get; set; }
    
    // draw an UI element
    public static void Draw(UI ui)
    {
        // set sprite effects
        int spriteEffect = 0;
        if (ui.FlipX)
            spriteEffect += 1;
        if (ui.FlipY)
            spriteEffect += 2;

        if (ui is TextUI text)
        {
            Core.SpriteBatch.DrawString
                (
                    text.Font,
                    text.Text,
                    text.position,
                    text.Color,
                    text.Rotation,
                    text.Origin,
                    text.Scale,
                    (SpriteEffects)spriteEffect,
                    0f
                );
        }
        else if (ui is SpriteUI sprite)
        {
            Core.SpriteBatch.Draw
                (
                    sprite.Sprite.SpriteSheet,
                    sprite.position,
                    sprite.Sprite.SourceRectangle,
                    sprite.Color,
                    sprite.Rotation,
                    sprite.Origin,
                    sprite.Scale,
                    (SpriteEffects)spriteEffect,
                    0f
                );
        }
    }
}
