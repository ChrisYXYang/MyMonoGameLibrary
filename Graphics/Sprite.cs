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
    public Vector2 OriginPoint { get; set; }

    // constructor for Sprite
    //
    // param: spriteSheet - sprite's spritesheet
    // param: originPoint - origin point
    // param: scale - scale of sprite
    // param: x - x position of source rectangle
    // param: y - y position of source rectangle
    // param: size - size of source rectangle
    public Sprite(Texture2D spriteSheet, Vector2 originPoint, int x, int y, int size)
    {
        SpriteSheet = spriteSheet;
        SourceRectangle = new Rectangle(x, y, size, size);
        OriginPoint = originPoint;
    }
}
