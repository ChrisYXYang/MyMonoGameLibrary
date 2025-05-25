using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MyMonoGameLibrary.Graphics;

public class SpriteSheet
{
    // variables and properties
    private Dictionary<string, Sprite> _sprites;
    
    public Texture2D Sheet { get; private set; }

    // constructor for SpriteSheet. Will create a dictionary containing user-defined
    // sprites in the spritesheet.
    //
    // param: spriteSize is the size of each sprite in pixels
    // param: spriteNames is names of all sprites in order
    // param: originType - preset position for origin point for sprites in the sprite sheet
    public SpriteSheet(Texture2D spriteSheet, int spriteSize, string[] spriteNames, 
        OriginType originType)
    {
        // check if too many sprites requested
        int numSprites = spriteNames.Length;
        int possibleSprites = spriteSheet.Height * spriteSheet.Width / numSprites;

        if (possibleSprites < numSprites)
        {
            throw new Exception("Too many sprites requested for spritesheet");
        }

        Sheet = spriteSheet;
        _sprites = new Dictionary<string, Sprite>();

        // create a sprite in SpriteSheet for every sprite listed in spriteNames
        int x = 0;
        int y = 0;

        for (int i = 0; i < numSprites; i++)
        {
            // create sprite and add to dict
            Sprite newSprite = new Sprite(Sheet, originType, 16, x, y, spriteSize);
            _sprites.Add(spriteNames[i], newSprite);
            
            // move source rectangle to next sprite
            x += spriteSize;

            if (x == Sheet.Width)
            {
                x = 0;
                y += spriteSize;
            }

        }
    }
}
