using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Linq;

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
    // param: fileName - xml file for sprite information
    // param: spriteSize - the size of each sprite in pixels
    // param: spriteScale - the scale of each sprite
    // param: centered - whether or not sprites in the spritesheet have origin point in the center
    public SpriteSheet(Texture2D spriteSheet, string fileName, int spriteScale, int spriteSize, 
        bool centered)
    {
        Sheet = spriteSheet;

        // create Sprites from xml file data about sprites in sprite sheet
        string filePath = Path.Combine("Content", fileName);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;

                // retrieve all <Sprite> elements and create new Sprite for each element
                // based on its information
                var sprites = root.Element("Sprites")?.Elements("Sprite");

                int x = 0;
                int y = 0;
                if (sprites != null)
                {
                    foreach (var sprite in sprites)
                    {
                        // get name and origin point (if exists)
                        string name = sprite.Attribute("name")?.Value;
                        float originX = float.Parse(sprite.Attribute("originX")?.Value ?? "0");
                        float originY = float.Parse(sprite.Attribute("originY")?.Value ?? "0");

                        Vector2 originPoint = Vector2.Zero;
                        if (originX != 0 || originY != 0)
                        {
                            originPoint = new Vector2(originX, originY);

                        }
                        else if (centered)
                        {
                            originPoint = new Vector2(spriteSize, spriteSize) * 0.5f;
                        }


                        // create new sprite
                        Sprite newSprite = new Sprite(Sheet, originPoint, spriteScale, x, y, spriteSize);
                        _sprites.Add(name, newSprite);


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
