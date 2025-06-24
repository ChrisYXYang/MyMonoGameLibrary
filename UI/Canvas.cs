using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// the canvas will contain UI elemnts to be drawn on the screen. This will there can be layering of 
// which elements to draw over the other.
public class Canvas : UIElement
{
    // scale of canvas
    public float Scale { get; set; } = Camera.PixelScale;

    // empty constructor
    public Canvas() { }

    // constructor
    //
    // param: scale - scale of canvas
    public Canvas(float scale)
    {
        Scale = scale;
    }

    public override void Start()
    {
        Queue<BaseUI> startQueue = [];

        // add canvas children to draw queue
        for (int i = 0; i < ChildCount; i++)
        {
            startQueue.Enqueue(GetChild(i));
        }

        // draw element and add its children to draw queue until all drawn
        while (startQueue.Count > 0)
        {
            BaseUI current = startQueue.Dequeue();

            for (int i = 0; i < current.ChildCount; i++)
            {
                startQueue.Enqueue(current.GetChild(i));
            }

            current.Start();
        }
    }

    public override void Update(GameTime gameTime)
    {
        Queue<BaseUI> updateQueue = [];

        // add canvas children to draw queue
        for (int i = 0; i < ChildCount; i++)
        {
            updateQueue.Enqueue(GetChild(i));
        }

        // draw element and add its children to draw queue until all drawn
        while (updateQueue.Count > 0)
        {
            BaseUI current = updateQueue.Dequeue();

            for (int i = 0; i < current.ChildCount; i++)
            {
                updateQueue.Enqueue(current.GetChild(i));
            }

            current.Update(gameTime);
        }
    }
}
