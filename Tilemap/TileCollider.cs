using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using MyMonoGameLibrary;

namespace MyMonoGameLibrary.Tilemap;

// square collider for a tile
public class TileCollider : RectCollider
{
    // variables and properties
    public Tile Tile { get; private set; }
    public float Left => Tile.Position.X;
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

    // see if intersects between another rect collider
    //
    // param: other - other collider
    // return: intersect or not
    public bool Intersects(RectCollider other)
    {
        return (this.Left < other.Right &&
                other.Left < this.Right &&
                this.Top < other.Bottom &&
                other.Top < this.Bottom);
    }
}
