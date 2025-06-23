using System;
using System.Collections.Generic;
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
}
