using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MyMonoGameLibrary.Scene;

namespace MyMonoGameLibrary.Tilemap;

// square collider for a tile
public class TileCollider : IRectCollider
{
    // variables and properties
    public Tile Parent { get; private set; }
    public float Left => Parent.Position.X - 0.5f;
    public float Right => Left + _width;
    public float Top => Parent.Position.Y + _yOffset;
    public float Bottom => Top + _height;

    private float _yOffset;
    private float _width;
    private float _height;

    // constructor 
    // 
    // param: tile - the tile that holds the collider
    public TileCollider(Tile parent)
    {
        Parent = parent;

        _yOffset = -(float)Parent.Size / Camera.SpritePixelsPerUnit;
        _width = (float)Parent.Size / Camera.SpritePixelsPerUnit;
        _height = (float)Parent.Size / Camera.SpritePixelsPerUnit;
    }

    // what to do when collision happens
    //
    // param: other - other collider
    public void OnCollision(IRectCollider other)
    {

    }
}
