using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MyMonoGameLibrary.Tilemap;

namespace MyMonoGameLibrary.Graphics;

// This class stores the spritesheets and tilesetsof the game and allows access to them.
public static class SpriteLibrary
{
    // variables and properties
    private static Dictionary<string, SpriteSheet> _spriteSheets = new Dictionary<string, SpriteSheet>();
    private static Dictionary<string, Tileset> _tileSets = new Dictionary<string, Tileset>();

    // add sprite sheet to library
    //
    // param: spriteSheetName - name of sprite sheet
    // param: spriteSheet - SpriteSheet to add
    public static void AddSpriteSheet(string spriteSheetName, SpriteSheet spriteSheet)
    {
        _spriteSheets.Add(spriteSheetName, spriteSheet);
    }
    
    // get sprite sheet
    //
    // param: spriteSheetName - name of sprite sheet
    // return: sprite sheet requested
    public static SpriteSheet GetSpriteSheet(string spriteSheetName)
    {
        return _spriteSheets[spriteSheetName];
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

    // add tile set to library
    //
    // param: tileSetName - name of tile set
    // param: tileSet - tile set to add
    public static void AddTileset(string tileSetName, Tileset tileSet)
    {
        _tileSets.Add(tileSetName, tileSet);
    }

    // get tile set
    //
    // param: tileSetName - name of tile set
    // param: tile set requested
    public static Tileset GetTileset(string tileSetName)
    {
        return _tileSets[tileSetName];
    }
}
