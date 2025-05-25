using System;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MyMonoGameLibrary.Graphics;

public class SpriteSheet
{
    // variables and properties
    private Dictionary<string, Sprite> _sprites;
    
    public Texture2D Texture { get; private set; }


    // constructor
    // param: spriteSize is the size of each sprite in pixels
    // param: spriteNames is names of all sprites in order
    public SpriteSheet(Texture2D texture, int spriteSize, List<string> spriteNames)
    {
        Texture = texture;
        _sprites = new Dictionary<string, Sprite>();

        int numSprites = spriteNames.Count;

        for (int i = 0; i < numSprites; i++)
        {
            Sprite newSprite = new Sprite(Texture, )
        }
    }
}
