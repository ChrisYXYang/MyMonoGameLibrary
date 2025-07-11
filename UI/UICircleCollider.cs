using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// circle collider for UI elements
public class UICircleCollider : ColliderComponent, ICircleCollider
{
    // radius of collider
    public float Radius { get; set; }
    public float Diameter { get; set; }

    // constructor
    //
    // param: diameter - diameter of collider
    public UICircleCollider(float diameter)
    {
        Diameter = diameter;
        Radius = Diameter / 2;
        Offset = Vector2.Zero;
    }

    // constructor
    //
    // param: diameter - diameter of collider
    // param: xOffset - x offset
    // param: yOffset - y offset
    public UICircleCollider(float diameter, float xOffset, float yOffset)
    {
        Diameter = diameter;
        Radius = Diameter / 2;
        Offset = new Vector2(xOffset, yOffset);
    }
}
