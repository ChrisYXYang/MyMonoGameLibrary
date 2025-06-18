using System;
using System.Collections.Generic;
using MyMonoGameLibrary.Scenes;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Tilemap;

// square collider for a tile
public class TileCollider : IAABBCollider
{
    // variables and properties
    public Tile Parent { get; private set; }
    public float Left => Center.X - 0.5f;
    public float Right => Center.X + 0.5f;
    public float Top => Center.Y - 0.5f;
    public float Bottom => Center.Y + 0.5f;
    public float Width => 1;
    public float Height => 1;
    public Vector2 Center => Parent.Position; 
    public bool Solid { get; set; }

    // constructor 
    // 
    // param: tile - the tile that holds the collider
    public TileCollider(Tile parent, bool solid)
    {
        Parent = parent;
        Solid = solid;
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
