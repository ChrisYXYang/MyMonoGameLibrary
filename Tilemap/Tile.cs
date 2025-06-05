using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;

namespace MyMonoGameLibrary.Tilemap;

// this class represents a tile
public class Tile
{
    // variables and properties
    public Sprite Sprite { get; private set; }
    public Vector2 Position { get; private set; }
    public int Size { get; private set; }
    public float LayerDepth { get; private set; }
    public TileCollider Collider { get; private set; }

    // constructor
    //
    // param: sprite - sprite of tile
    // param: position - position of tile
    // param: size - num pixels in tile
    // param: layerDepth - layer
    // param: collider - whether or not collider
    public Tile(Sprite sprite, Vector2 position, int size, float layerDepth, bool collider)
    {
        Sprite = sprite;
        Position = position;
        Size = size;
        LayerDepth = layerDepth;

        if (collider)
            Collider = new TileCollider(this);
    }
   
    // draw the tile
    public void Draw()
    {
        Sprite.GameDraw
            (
                Position,
                Color.White,
                0f,
                Vector2.One,
                SpriteEffects.None,
                LayerDepth
            );
    }
}
