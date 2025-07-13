using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Tilemap;

namespace MyMonoGameLibrary;

// This class stores assets needed for the game such as spritesheets, fonts, and tilesets.
public class Library
{
    // variables and properties
    private Dictionary<string, SpriteSheet> _spriteSheets = [];
    private Dictionary<string, Tileset> _tileSets = [];
    private Dictionary<string, SpriteFont> _fonts = [];
    private Dictionary<string, SoundEffect> _soundEffects = [];
    private Dictionary<string, Song> _songs = [];
    private ContentManager _content;
    public Library(ContentManager content)
    {
        _content = content;
    }

    // add sprite sheet to library
    //
    // param: spriteSheetName - name of sprite sheet
    public void AddSpriteSheet(string spriteSheetName)
    {
        _spriteSheets.Add(spriteSheetName, new SpriteSheet(_content, spriteSheetName));
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
    // param: tileSetName - name of tile set
    public void AddTileset(string tileSetName)
    {
        _tileSets.Add(tileSetName, new Tileset(_content, tileSetName));
    }

    // get tile set
    //
    // param: tileSetName - name of tile set
    // return: tile set requested
    public Tileset GetTileset(string tileSetName)
    {
        return _tileSets[tileSetName];
    }


    // add font to library
    //
    // param: fontName - name of font
    public void AddFont(string fontName)
    {
        _fonts.Add(fontName, _content.Load<SpriteFont>("fonts/" + fontName));
    }

    // get font
    //
    // param: fontName - name of font
    // return: font requested
    public SpriteFont GetFont(string fontName)
    {
        return _fonts[fontName];
    }

    // add sound effect to library
    //
    // param: soundEffectName - sound effect to add
    public void AddSoundEffect(string soundEffectName)
    {
        _soundEffects.Add(soundEffectName, _content.Load<SoundEffect>("audio/" + soundEffectName));
    }

    // get sound effect
    //
    // param: soundEffectName - name of font
    // return: sound effect requested
    public SoundEffect GetSoundEffect(string soundEffectName)
    {
        return _soundEffects[soundEffectName];
    }

    // add song to library
    //
    // param: songName - song to add
    public void AddSong(string songName)
    {
        _songs.Add(songName, _content.Load<Song>("audio/" + songName));
    }

    // get song
    //
    // param: songName - name of font
    // return: song requested
    public Song GetSong(string songName)
    {
        return _songs[songName];
    }
}
