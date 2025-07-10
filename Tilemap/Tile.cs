using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.Tilemap;

// this class represents a tile
public class Tile
{
    // variables and properties
    public Sprite Sprite { get; private set; }
    public Vector2 Position { get; private set; }
    public Vector2 GridPosition { get; private set; }
    public int Size { get; private set; }
    public float LayerDepth { get; private set; }
    public TileCollider Collider { get; private set; }
    public string Name { get; private set; }

    // constructor
    //
    // param: sprite - sprite of tile
    // param: position - position of tile
    // param: size - num pixels in tile
    // param: layerDepth - layer
    // param: collider - whether or not collider
    // param: solid - whether or not solid collider
    // param: layer - layer name
    public Tile(string name, Sprite sprite, Vector2 position, Vector2 gridPos, int size, float layerDepth, bool collider, bool solid, string layer)
    {
        Name = name;
        Sprite = sprite;
        Position = position;
        GridPosition = gridPos;
        Size = size;
        LayerDepth = layerDepth;

        if (collider)
            Collider = new TileCollider(this, solid, layer);
    }

    // return collider
    //
    // return: the collider
    public TileCollider GetCollider()
    {
        return Collider;
    }
}
