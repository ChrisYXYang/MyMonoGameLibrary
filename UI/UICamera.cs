using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGameLibrary.UI;

// this class is responsible for drawing UI canvas/elements
public static class UICamera
{
    // draw an UI element
    //
    // param: ui - the ui element
    // param: canvasScale - the scale of the canvas
    public static void Draw(UIElement ui, float canvasScale)
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
                    canvasScale * sprite.Scale,
                    (SpriteEffects)spriteEffect,
                    0f
                );
        }
    }
}
