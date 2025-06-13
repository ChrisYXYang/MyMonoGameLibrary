using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Tools;
using MyMonoGameLibrary.Tilemap;
using System.Collections.Generic;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.Tools;

// Extension of the Core class that gives debugging tools such as seeing origin point
public class DebugMode : Core
{
    // variables and properties
    private static SpriteSheet _sprites;
    private static Sprite _originPoint;
    private static Sprite _boxCollider;
    private static Sprite _circleCollider;

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
        _sprites = new SpriteSheet(Core.Content, "debug");
        _originPoint = _sprites.GetSprite("origin_point");
        _boxCollider = _sprites.GetSprite("box_collider");
        _circleCollider = _sprites.GetSprite("circle_collider");


        base.LoadContent();
    }

    // draw origin point for a gameobject
    //
    // param: gameObject - game object to draw origin point for
    public static void DrawOrigin(GameObject gameObject)
    {
        Transform transform = gameObject.GetComponent<Transform>();
        Camera.Draw
            (
                _originPoint,
                transform.position,
                Color.White,
                0f,
                1/Camera.PixelScale,
                SpriteEffects.None,
                1f
            );
    }

    // draw collider for game object
    //
    // param: gameObject - game object to draw
    public static void DrawGameObjectCollider(GameObject gameObject)
    {
        BoxCollider boxCollider = gameObject.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            DrawBoxCollider(boxCollider);
        }

        CircleCollider circleCollider = gameObject.GetComponent<CircleCollider>();
        if (circleCollider != null)
        {
            DrawCircleCollider(circleCollider);
        }
    }

    // draw tile colliders for a tilemap
    //
    // param: tilemap - tile map to draw collider
    public static void DrawTilemapCollider(TileMap tilemap)
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

                    DrawBoxCollider(tile.Collider);
                }
            }
        }
    }

    // helper method to draw box collider
    //
    // param: collider - box collider to draw
    private static void DrawBoxCollider(IAABBCollider collider)
    {
        Camera.Draw
            (
                _boxCollider, 
                new Vector2(collider.Left, collider.Top),
                Color.White,
                0f,
                new Vector2(collider.Right - collider.Left, collider.Bottom - collider.Top) * 0.5f,
                SpriteEffects.None,
                0.9f
            );
    }

    // helper method to draw circle collider 
    //
    // param: collider - circle collider to draw
    private static void DrawCircleCollider(CircleCollider collider)
    {
        Camera.Draw
            (
                _circleCollider,
                collider.Center,
                Color.White,
                0f,
                collider.Radius,
                SpriteEffects.None,
                0.9f
            );
    }
}
