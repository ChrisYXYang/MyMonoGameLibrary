using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Tools;
using MyMonoGameLibrary.Tilemap;
using System.Collections.Generic;
using MyMonoGameLibrary.Scene;

namespace MyMonoGameLibrary.Tools;

// Extension of the Core class that gives debugging tools such as seeing origin point
public class DebugMode : Core
{
    // variables and properties
    private static SpriteSheet _sprites;
    private static Sprite _originPoint;
    private static Sprite _boxCollider;

    // constructor
    //
    // param: title - title of window
    // param: width - width of screen
    // param: height - height of screen
    // param: fullScreen - full screen or not
    public DebugMode(string title, int width, int height, bool fullScreen) : base(title, width, height, fullScreen)
    {

    }

    // load content
    protected override void LoadContent()
    {
        _sprites = new SpriteSheet("debug");
        _originPoint = _sprites.GetSprite("origin_point");
        _boxCollider = _sprites.GetSprite("box_collider");
    }

    // draw origin point for a gameobject
    //
    // param: gameObject - game object to draw origin point for
    protected static void DrawOrigin(GameObject gameObject)
    {
        Transform transform = gameObject.GetComponent<Transform>();
        _originPoint.Draw
            (
                Camera.UnitToPixel(transform.position),
                Color.White,
                0f,
                Vector2.One,
                SpriteEffects.None,
                1f
            );
    }

    // draw box collider for gameObject
    //
    // param: gameObject - game object to draw collider
    protected static void DrawBoxCollider(GameObject gameObject)
    {
        BoxCollider collider = gameObject.GetComponent<BoxCollider>();

        if (collider == null)
            return;

        _boxCollider.GameDraw
            (
                 new Vector2(collider.Left, collider.Top),
                 Color.White,
                 0f,
                 new Vector2((float)collider.Width / Camera.SpritePixelsPerUnit, (float)collider.Height / Camera.SpritePixelsPerUnit),
                 SpriteEffects.None,
                 0.9f
            );
    }

    // draw tile colliders for a tilemap
    //
    // param: tilemap - tile map to draw collider
    protected static void DrawTilemapCollider(TileMap tilemap)
    {
        List<string> layerNames = tilemap.Layers;

        foreach (string layerName in layerNames)
        {
            for (int i = 0; i < tilemap.Rows; i++)
            {
                for (int j = 0; j < tilemap.Columns; j++)
                {
                    Tile tile = tilemap.GetTile(layerName, i, j);

                    if (tile == null)
                        continue;

                    if (tile.Collider == null)
                        continue;

                    _boxCollider.GameDraw
                        (
                             new Vector2(tile.Collider.Left, tile.Collider.Top),
                             Color.White,
                             0f,
                             Vector2.One,
                             SpriteEffects.None,
                             0.9f
                        );
                }
            }
        }
    }
}
