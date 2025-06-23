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
    

    // constructor
    //
    // param: children - children UI elements
    public Canvas(List<BaseUI> children) : base(children)
    {
    }

    // constructor
    //
    // param: children - chidlren UI elements
    // param: scale - scale of canvas
    public Canvas(List<BaseUI> children, float scale) : base(children)
    {
        Scale = scale;
    }
}
