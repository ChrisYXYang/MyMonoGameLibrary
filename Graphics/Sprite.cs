using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Graphics;

// Preset positions for the origin point of a sprite.
public enum OriginType
{
    Center,
    TopLeft,
    TopRight,
    BottomLeft,
    BottomRight,
    Custom
}

// summary:
//      This class will represent a single sprite from a spritesheet. It will determine source 
//      rectangle, origin point, and scale of sprite.
public class Sprite
{
    // variables and properties
    public Texture2D SpriteSheet { get; set; }
    public Rectangle SourceRectangle { get; set; }
    public OriginType Origin { get; private set; }
    public Vector2 OriginPoint { get; private set; }
    public int Scale { get; set; }

    // Size doesn't take into account transform scale
    public int Size => SourceRectangle.Width * Scale;
    
    // constructor with origin point being one of the preset origin points
    public Sprite(Texture2D spriteSheet, OriginType origin, int scale, int x, int y, int size)
    {
        SpriteSheet = spriteSheet;
        Scale = scale;
        SourceRectangle = new Rectangle(x, y, size, size);
        SetOrigin(origin);
    }

    // Summary:
    //      Set origin point type for preset origin type
    public void SetOrigin(OriginType origin)
    {
        Origin = origin;

        if (Origin == OriginType.Center)
        {
            OriginPoint = new Vector2(SourceRectangle.Width * 0.5f, SourceRectangle.Height * 0.5f);
        }
        else if (Origin == OriginType.TopLeft)
        {
            OriginPoint = Vector2.Zero;
        }
        else if (Origin == OriginType.TopRight)
        {
            OriginPoint = new Vector2(SourceRectangle.Width, 0);
        }
        else if (Origin == OriginType.BottomLeft)
        {
            OriginPoint = new Vector2(0, SourceRectangle.Height);
        }
        else if (Origin == OriginType.BottomRight)
        {
            OriginPoint = new Vector2(SourceRectangle.Width, SourceRectangle.Height);
        }
    }

    // Summary:
    //      Set origin point to custom position
    public void SetOrigin(Vector2 originPoint)
    {
        Origin = OriginType.Custom;
        OriginPoint = originPoint;
    }

    // Summary:
    //      Draw this sprite with custom transform, color, layer depth, and sprite effects
    public void Draw(SpriteBatch spriteBatch, Vector2 position, float rotation, float scale,
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
