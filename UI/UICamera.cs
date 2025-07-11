using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// this class is responsible for drawing UI canvas/elements
public static class UICamera
{
    // scale to draw
    public static float Scale { get; set; } = Camera.PixelScale;
    
    // draw an UI element
    //
    // param: ui - the ui element
    public static void Draw(GameObject ui)
    {
        // set sprite effects
        int spriteEffect = 0;
        if (ui.Renderer.FlipX)
            spriteEffect += 1;
        if (ui.Renderer.FlipY)
            spriteEffect += 2;

        if (ui.Renderer is UIText text)
        {
            Core.SpriteBatch.DrawString
                (
                    text.Font,
                    text.Text,
                    ui.Transform.TruePosition,
                    text.Color,
                    MathHelper.ToRadians(ui.Transform.TrueRotation),
                    text.Origin,
                    ui.Transform.TrueScale,
                    (SpriteEffects)spriteEffect,
                    0f
                );
        }
        else if (ui.Renderer is UISprite sprite)
        {
            Core.SpriteBatch.Draw
                (
                    sprite.Sprite.SpriteSheet,
                    ui.Transform.TruePosition,
                    sprite.Sprite.SourceRectangle,
                    sprite.Color,
                    MathHelper.ToRadians(ui.Transform.TrueRotation),
                    sprite.Origin,
                    Scale * ui.Transform.TrueScale,
                    (SpriteEffects)spriteEffect,
                    0f
                );
        }
    }
}
