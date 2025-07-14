using System;
using System.Collections.Generic;
using MyMonoGameLibrary.Scenes;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Tilemap;

// square collider for a tile
public class TileCollider : IAABBCollider
{
    // variables and properties
    public string Layer { get; set; }
    public Tile Parent { get; private set; }
    public float Left => Center.X - 0.5f;
    public float Right => Center.X + 0.5f;

    public float Top
    {
        get
        {
            if (!Platform)
                return Center.Y - 0.5f;
            else
                return Center.Y - 0.125f;
        }
    }
    public float Bottom
    {
        get
        {
            if (!Platform)
                return Center.Y + 0.5f;
            else
                return Center.Y + 0.125f;
        }
    }

    public float Width => Right - Left;
    public float Height => Bottom - Top;
    public Vector2 Center
    {
        get
        {
            if (!Platform)
                return Parent.Position;
            else
                return new Vector2(Parent.Position.X, Parent.Position.Y - 0.375f);
        }
    }
    public bool Solid { get; set; }
    public bool Platform { get; set; }

    // constructor 
    // 
    // param: tile - the tile that holds the collider
    // param: solid - solid or not
    // param: layer - the layer name
    public TileCollider(Tile parent, bool solid, bool platform, string layer)
    {
        Parent = parent;
        Solid = solid;
        Layer = layer;
        Platform = platform;
    }

    public void Colliding(ICollider other)
    {
        
    }

    public void NotColliding(ICollider other)
    {
        
    }

    // get name of parent
    //
    // return: name of parent
    public string GetName()
    {
        return Parent.Name;
    }
}
