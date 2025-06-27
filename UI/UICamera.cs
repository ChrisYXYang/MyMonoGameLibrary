using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;

namespace MyMonoGameLibrary.UI;

// this class is responsible for drawing UI canvas/elements
public static class UICamera
{
    // draw a canvas
    //
    // param: canvas - canvas to draw
    public static void Draw(Canvas canvas)
    {
        Queue<BaseUI> drawQueue = [];

        // add canvas children to draw queue
        for (int i = 0; i < canvas.ChildCount; i++)
        {
            drawQueue.Enqueue(canvas.GetChild(i));
        }

        // draw element and add its children to draw queue until all drawn
        while (drawQueue.Count > 0)
        {
            BaseUI current = drawQueue.Dequeue();

            for (int i = 0; i < current.ChildCount; i++)
            {
                drawQueue.Enqueue(current.GetChild(i));
            }

            if (current.IsVisible)
                UICamera.Draw(current, canvas.Scale);
        }
    }
    
    // draw an UI element
    //
    // param: ui - the ui element
    // param: canvasScale - the scale of the canvas
    public static void Draw(BaseUI ui, float canvasScale)
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
