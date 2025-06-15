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
    public float Left => Parent.Position.X - 0.5f;
    public float Right => Parent.Position.X + 0.5f;
    public float Top => Parent.Position.Y - 0.5f;
    public float Bottom => Parent.Position.Y + 0.5f;
    public float Width => 1;
    public float Height => 1;
    public Vector2 Center => Parent.Position; 

    // constructor 
    // 
    // param: tile - the tile that holds the collider
    public TileCollider(Tile parent)
    {
        Parent = parent;
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
