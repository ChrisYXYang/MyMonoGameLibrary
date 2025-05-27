using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Xml;

namespace MyMonoGameLibrary.Graphics;

// This class represents a sprite sheet. It will contain all the Sprites made from the sprite sheet.
public class SpriteSheet
{
    // variables and properties
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    public Texture2D Sheet { get; private set; }

    // constructor for SpriteSheet. Will create a dictionary containing user-defined
    // Sprites in the spritesheet.
    //
    // param: spriteSheet - the sprite sheet
    // param: content - content manager
    // param: fileName - xml file for sprite information
    // param: spriteSize - the size of each sprite in pixels
    // param: spriteScale - the scale of each sprite
    // param: centered - whether or not sprites in the spritesheet have origin point in the center
    public SpriteSheet(Texture2D spriteSheet, ContentManager content, string fileName, 
        int spriteScale, int spriteSize, bool centered)
    {
        Sheet = spriteSheet;

        string filePath = Path.Combine(content.RootDirectory, fileName);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {

            }
        }

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
