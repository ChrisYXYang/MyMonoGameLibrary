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
    public Tile Tile { get; private set; }
    public float Left => Tile.Position.X - 0.5f;
    public float Right => Left + _width;
    public float Top => Tile.Position.Y + _yOffset;
    public float Bottom => Top + _height;

    private float _yOffset;
    private float _width;
    private float _height;

    // constructor 
    // 
    // param: tile - the tile that holds the collider
    public TileCollider(Tile tile)
    {
        Tile = tile;

        _yOffset = -(float)Tile.Size / Camera.SpritePixelsPerUnit;
        _width = (float)Tile.Size / Camera.SpritePixelsPerUnit;
        _height = (float)Tile.Size / Camera.SpritePixelsPerUnit;
    }
}
