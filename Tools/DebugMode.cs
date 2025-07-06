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
    }

    // print the scene UI heriarchy
    public static void PrintUI()
    {
        Canvas canvas = SceneTools.GetCanvas();
        List<BaseUI> elements = canvas.GetChildren();

        // iterate through every elements. If element's parent is canvas, then print the
        // elements and its heirarchy
        foreach (BaseUI element in elements)
        {
            if (element.Parent == canvas)
            {
                Stack<(string, BaseUI)> printStack = [];
                printStack.Push(("- ", element));

                while (printStack.Count > 0)
                {
                    (string, BaseUI) currentPrint = printStack.Pop();
                    Debug.WriteLine(currentPrint.Item1 + currentPrint.Item2.Name);

                    for (int i = currentPrint.Item2.ChildCount - 1; i >= 0; i--)
                    {
                        printStack.Push(("  " + currentPrint.Item1, currentPrint.Item2.GetChild(i)));
                    }
                }
            }
        }
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
                transform.position,
                Color.White,
                0f,
                0.05f,
                SpriteEffects.None,
                1f
            );
    }

    // draw collider for game object
    //
    // param: gameObject - game object to draw
    public static void DrawGameObjectCollider(GameObject gameObject)
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

    // draw collider for ui element
    //
    // param: ui - ui to draw
    public static void DrawUICollider(BaseUI ui)
    {
        UICollider collider = ui.Collider;

        if (collider != null)
        {
            if (collider is UIBoxCollider box)
                DrawUIBoxCollider(box);
            else if (collider is UICircleCollider circle)
                DrawUICircleCollider(circle);
        }
    }

    // draw colliders for canvas
    //
    // param: canvas - canvas to draw colliders for
    public static void DrawCanvasColliders(Canvas canvas)
    {
        Queue<BaseUI> drawQueue = [];

        // add canvas children to draw queue
        for (int i = 0; i < canvas.ChildCount; i++)
        {
            drawQueue.Enqueue(canvas.GetChild(i));
        }

        // draw element and add its children to draw queue until all drawn
        while (drawQueue.Count > 0)
        {
            BaseUI current = drawQueue.Dequeue();

            for (int i = 0; i < current.ChildCount; i++)
            {
                drawQueue.Enqueue(current.GetChild(i));
            }

            DrawUICollider(current);
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

        Camera.Draw
            (
                _originPoint,
                collider.Center,
                Color.White,
                0f,
                0.05f,
                SpriteEffects.None,
                1f
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
                Color.White,
                0f,
                collider.Radius,
                SpriteEffects.None,
                0.9f
            );

        Camera.Draw
        (
            _originPoint,
            collider.Center,
            Color.White,
            0f,
            0.05f,
            SpriteEffects.None,
            1f
        );
    }

    // helper method to draw ui box collider
    //
    // param: collider - box collider to draw
    private static void DrawUIBoxCollider(UIBoxCollider collider)
    {
        Core.SpriteBatch.Draw
            (
                _boxCollider.SpriteSheet,
                new Vector2(collider.Left, collider.Top),
                _boxCollider.SourceRectangle,
                Color.White,
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
    private static void DrawUICircleCollider(UICircleCollider collider)
    {
        Core.SpriteBatch.Draw
            (
                _circleCollider.SpriteSheet,
                collider.Center,
                _circleCollider.SourceRectangle,
                Color.White,
                0f,
                _circleCollider.OriginPoint,
                collider.Diameter / 16f,
                SpriteEffects.None,
                0.9f
            );
    }
}
