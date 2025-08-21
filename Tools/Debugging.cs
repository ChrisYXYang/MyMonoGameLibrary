using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Graphics;
using MyMonoGameLibrary.Tools;
using MyMonoGameLibrary.Tilemap;
using System.Collections.Generic;
using MyMonoGameLibrary.Scenes;
using System.Reflection.Metadata.Ecma335;
using MyMonoGameLibrary.UI;

namespace MyMonoGameLibrary.Tools;

// Extension of the Core class that gives debugging tools such as visualizing origin and colliders
public class Debugging
{
    // variables and properties
    private static SpriteSheet _sprites;
    private static Sprite _originPoint;
    private static Sprite _boxCollider;
    private static Sprite _circleCollider;
    private static Color _colliderColor = Color.White * 0.8f;

    // load content
    public static void LoadContent()
    {
        _sprites = new SpriteSheet(Core.Content, "debug");
        _originPoint = _sprites.GetSprite("origin_point");
        _boxCollider = _sprites.GetSprite("box_collider");
        _circleCollider = _sprites.GetSprite("circle_collider");
    }

    // print the scene game object heirarchy
    public static void PrintScene()
    {
        List<GameObject> gameObjects = SceneTools.GetGameObjects();

        // iterate through every game object. If game object has no parent, then print the
        // game object and its heirarchy
        foreach(GameObject gameObject in gameObjects)
        {
            if (gameObject.Parent == null)
            {
                Stack<(string, GameObject)> printStack = [];
                printStack.Push(("- ", gameObject));

                while (printStack.Count > 0)
                {
                    (string, GameObject) currentPrint = printStack.Pop();
                    Debug.WriteLine(currentPrint.Item1 + currentPrint.Item2.Name);

                    for (int i = currentPrint.Item2.ChildCount - 1; i >= 0; i--)
                    {
                        printStack.Push(("  " + currentPrint.Item1, currentPrint.Item2.GetChild(i)));
                    }
                }
            }
        }
        Debug.WriteLine("");
    }

    // draw origin point for a gameobject
    //
    // param: gameObject - game object to draw origin point for
    public static void DrawOrigin(GameObject gameObject)
    {
        Transform transform = gameObject.Transform;

        if (transform == null)
            return;

        Camera.Draw
            (
                _originPoint,
                transform.TruePosition,
                Color.White,
                0f,
                0.05f,
                SpriteEffects.None,
                1f
            );
    }

    // draw origin point for ui
    //
    // param: gameObject - ui to draw origin point for
    public static void DrawUIOrigin(GameObject gameObject)
    {
        Transform transform = gameObject.Transform;

        if (transform == null)
            return;

        Core.SpriteBatch.Draw
            (
                _originPoint.SpriteSheet,
                transform.TruePosition,
                _originPoint.SourceRectangle,
                Color.White,
                0f,
                _originPoint.OriginPoint,
                Vector2.One,
                SpriteEffects.None,
                1f
            );
    }

    // draw collider for game object
    //
    // param: gameObject - game object to draw
    public static void DrawCollider(GameObject gameObject)
    {
        ICollider collider = gameObject.Collider;

        if (collider != null)
        {
            if (collider is BoxCollider box)
                DrawBoxCollider(box);
            else if (collider is CircleCollider circle)
                DrawCircleCollider(circle);
        }
    }

    // draw collider for ui
    //
    // param: gameObject - game object to draw
    public static void DrawUICollider(GameObject gameObject)
    {
        ICollider collider = gameObject.Collider;

        if (collider != null)
        {
            if (collider is BoxCollider box)
                DrawUIBoxCollider(box);
            else if (collider is CircleCollider circle)
                DrawUICircleCollider(circle);
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
                _colliderColor,
                0f,
                new Vector2(collider.Right - collider.Left, collider.Bottom - collider.Top) * 0.5f,
                SpriteEffects.None,
                0.9f
            );
    }

    // helper method to draw circle collider 
    //
    // param: collider - circle collider to draw
    private static void DrawCircleCollider(ICircleCollider collider)
    {
        Camera.Draw
            (
                _circleCollider,
                collider.Center,
                _colliderColor,
                0f,
                collider.Radius,
                SpriteEffects.None,
                0.9f
            );
    }

    // helper method to draw ui box collider
    //
    // param: collider - box collider to draw
    private static void DrawUIBoxCollider(BoxCollider collider)
    {
        Core.SpriteBatch.Draw
            (
                _boxCollider.SpriteSheet,
                new Vector2(collider.Left, collider.Top),
                _boxCollider.SourceRectangle,
                _colliderColor,
                0f,
                _boxCollider.OriginPoint,
                 new Vector2(collider.Right - collider.Left, collider.Bottom - collider.Top) / 16f,
                SpriteEffects.None,
                0.9f
            );
    }

    // helper method to draw ui circle collider 
    //
    // param: collider - circle collider to draw
    private static void DrawUICircleCollider(CircleCollider collider)
    {
        Core.SpriteBatch.Draw
            (
                _circleCollider.SpriteSheet,
                collider.Center,
                _circleCollider.SourceRectangle,
                _colliderColor,
                0f,
                _circleCollider.OriginPoint,
                collider.Diameter / 16f,
                SpriteEffects.None,
                0.9f
            );
    }
}
