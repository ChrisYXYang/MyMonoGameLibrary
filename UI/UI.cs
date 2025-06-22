using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.UI;

// this class represents a UI element in the game. It will also manage the overall UI of the game.
public abstract class UI
{
    // the scale of all UI
    public static float CanvasScale;

    // position of UI element
    public Vector2 position;

    // scale of UI element
    public Vector2 Scale { get; set; }

    // will this UI have custom origin point
    public bool OverrideOrigin { get; set; }

    // custom origin point for this UI
    public Vector2 Origin { get; set; }

    // draw an UI element
    public static void Draw(UI ui)
    {
        //if (ui is )
        //Core.SpriteBatch.Draw()
    }
}
