using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Graphics;

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
    public Texture2D SpriteSheet { get; private set; }
    public Rectangle SourceRectangle { get; private set; }
    public OriginType Origin { get; private set; }
    public Vector2 OriginPoint { get; private set; }
    public int Scale { get; private set; }

    // width and height do not take into account transform scale
    public int Width => SourceRectangle.Width * Scale;
    public int Height => SourceRectangle.Height * Scale;
    
    // constructor for preset origin point
    public Sprite(Texture2D spriteSheet, OriginType origin, int scale, int x, int y, int width, 
        int height)
    {
        SpriteSheet = spriteSheet;
        Scale = scale;
        SourceRectangle = new Rectangle(x, y, width, height);

        SetOrigin(origin);
    }

    // constructor for custom origin point
    public Sprite(Texture2D spriteSheet, Vector2 originPoint, int scale, int x, int y, int width,
    int height)
    {
        SpriteSheet = spriteSheet;
        Origin = OriginType.Custom;
        Scale = scale;
        SourceRectangle = new Rectangle(x, y, width, height);
        OriginPoint = originPoint;
    }

    // Summary:
    //      Set origin point type for preset origin type
    public void SetOrigin(OriginType origin)
    {
        Origin = origin;

        if (origin == OriginType.Center)
        {
            OriginPoint = new Vector2(SourceRectangle.Width * 0.5f, SourceRectangle.Height * 0.5f);
        }
        else if (origin == OriginType.TopLeft)
        {
            OriginPoint = Vector2.Zero;
        }
        else if (origin == OriginType.TopRight)
        {
            OriginPoint = new Vector2(SourceRectangle.Width, 0);
        }
        else if (origin == OriginType.BottomLeft)
        {
            OriginPoint = new Vector2(0, SourceRectangle.Height);
        }
        else if (origin == OriginType.BottomRight)
        {
            OriginPoint = new Vector2(SourceRectangle.Width, SourceRectangle.Height);
        }
    }

    //

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
