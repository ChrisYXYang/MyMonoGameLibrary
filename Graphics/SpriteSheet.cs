using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MyMonoGameLibrary.Graphics;

// This class represents a sprite sheet. It will contain all the Sprites made from the sprite sheet.
public class SpriteSheet
{
    // variables and properties
    private Dictionary<string, Sprite> _sprites;
    public Texture2D Sheet { get; private set; }

    // constructor for SpriteSheet. Will create a dictionary containing user-defined
    // Sprites in the spritesheet.
    //
    // param: spriteSheet - the sprite sheet
    // param: spriteSize - the size of each sprite in pixels
    // param: spriteScale - the scale of each sprite
    // param: spriteNames - names of all sprites in left-right, top-down order
    // param: centered - whether or not sprites in the spritesheet have origin point in the center
    public SpriteSheet(Texture2D spriteSheet, int spriteScale, int spriteSize, string[] spriteNames, 
        bool centered)
    {
        Sheet = spriteSheet;
        _sprites = new Dictionary<string, Sprite>();

        // create a Sprite in SpriteSheet for every sprite listed in spriteNames
        int numSprites = spriteNames.Length;
        int x = 0;
        int y = 0;

        for (int i = 0; i < numSprites; i++)
        {
            // create sprite and add to dict
            Sprite newSprite = new Sprite(Sheet, centered, spriteScale, x, y, spriteSize);
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

    // get a sprite
    //
    // param: spriteName - name of sprite
    // return: chosen Sprite
    public Sprite GetSprite(string spriteName)
    {
        return _sprites[spriteName];
    }
}
