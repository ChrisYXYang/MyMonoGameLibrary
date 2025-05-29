using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Graphics;

// This class stores the sprite sheets of the game and and so allows any class to access sprite
// sheets and their corresponding sprites/animations.
public class SpriteLibrary
{
    // variables and properties
    private static Dictionary<string, SpriteSheet> _spriteSheets = new Dictionary<string, SpriteSheet>();
    
    // add sprite sheet to library
    //
    // param: spriteSheetName - name of sprite sheet
    // param: spriteSheet - SpriteSheet to add
    public static void AddSpriteSheet(string spriteSheetName, SpriteSheet spriteSheet)
    {
        _spriteSheets.Add(spriteSheetName, spriteSheet);
    }
    
    // get sprite
    //
    // param: spriteSheet - sprite sheet to get from
    // param: sprite - sprite to get
    // return: requested sprite
    public static Sprite GetSprite(string spriteSheet, string sprite)
    {
        return _spriteSheets[spriteSheet].GetSprite(sprite);
    }

    // get animation
    //
    // param: spriteSheet - sprite sheet to get from
    // param: animation - animation to get
    // return: requested animation
    public static Animation GetAnimation(string spriteSheet, string animation)
    {
        return _spriteSheets[spriteSheet].GetAnimation(animation);
    }
}
