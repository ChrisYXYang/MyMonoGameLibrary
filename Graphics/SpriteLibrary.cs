using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using MyMonoGameLibrary.Tilemap;

namespace MyMonoGameLibrary.Graphics;

// This class stores the spritesheets and tilesetsof the game and allows access to them.
public class SpriteLibrary
{
    // variables and properties
    private Dictionary<string, SpriteSheet> _spriteSheets = new Dictionary<string, SpriteSheet>();
    private Dictionary<string, Tileset> _tileSets = new Dictionary<string, Tileset>();

    // add sprite sheet to library
    //
    // param: content - content manager
    // param: spriteSheetName - name of sprite sheet
    public void AddSpriteSheet(ContentManager content, string spriteSheetName)
    {
        _spriteSheets.Add(spriteSheetName, new SpriteSheet(content, spriteSheetName));
    }
    
    // get sprite sheet
    //
    // param: spriteSheetName - name of sprite sheet
    // return: sprite sheet requested
    public SpriteSheet GetSpriteSheet(string spriteSheetName)
    {
        return _spriteSheets[spriteSheetName];
    }

    // get sprite
    //
    // param: spriteSheet - sprite sheet to get from
    // param: sprite - sprite to get
    // return: requested sprite
    public Sprite GetSprite(string spriteSheet, string sprite)
    {
        return _spriteSheets[spriteSheet].GetSprite(sprite);
    }

    // get animation
    //
    // param: spriteSheet - sprite sheet to get from
    // param: animation - animation to get
    // return: requested animation
    public Animation GetAnimation(string spriteSheet, string animation)
    {
        return _spriteSheets[spriteSheet].GetAnimation(animation);
    }

    // add tile set to library
    //
    // param: content - content manager
    // param: tileSetName - name of tile set
    public void AddTileset(ContentManager content, string tileSetName)
    {
        _tileSets.Add(tileSetName, new Tileset(content, tileSetName));
    }

    // get tile set
    //
    // param: tileSetName - name of tile set
    // param: tile set requested
    public Tileset GetTileset(string tileSetName)
    {
        return _tileSets[tileSetName];
    }
}
