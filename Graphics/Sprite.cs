using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Graphics;

// summary:
//      This class will represent a single sprite from a spritesheet. It will determine source 
//      rectangle, origin point, and scale of sprite.
public class Sprite
{
    // variables and properties
    public Texture2D SpriteSheet { get; private set; }
    public Rectangle SourceRectangle { get; private set; }
    public Vector2 OriginPoint { get; set; };
    public int Scale { get; private set; }

    // Size doesn't take into account transform scale
    public int Size => SourceRectangle.Width * Scale;
    
    // constructor for Sprite
    //
    // param: spriteSheet - sprite's spritesheet
    // param: originPoint - origin point
    // param: scale - scale of sprite
    // param: x - x position of source rectangle
    // param: y - y position of source rectangle
    // param: size - size of source rectangle
    public Sprite(Texture2D spriteSheet, Vector2 originPoint, int scale, int x, int y, int size)
    {
        SpriteSheet = spriteSheet;
        Scale = scale;
        SourceRectangle = new Rectangle(x, y, size, size);
        OriginPoint = originPoint;
    }

    // Draw this sprite
    //
    // param: spriteBatch - SpriteBatch that will draw the sprite
    // param: position - position to draw sprite
    // param: rotation - sprite rotation
    // param: scale - sprite scale (not including Sprite.Scale)
    // param: color - color of sprite
    // param: spriteEffects - sprite effects
    // param: layerDepth - layer depth of sprite
    public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, Vector2 scale,
        Color color, SpriteEffects spriteEffects, float layerDepth)
    {
        spriteBatch.Draw(
            SpriteSheet,
            position,
            SourceRectangle,
            color,
            rotation,
            OriginPoint,
            scale * Scale,
            spriteEffects,
            layerDepth     
        );
    }
}
