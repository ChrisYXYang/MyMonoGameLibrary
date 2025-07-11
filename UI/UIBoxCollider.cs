using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// box collider for UI elements
public class UIBoxCollider : ColliderComponent, IAABBCollider
{
    // variables and properties
    public float Width { get; set; } = 0;
    public float Height { get; set; } = 0;
    public float Left => Center.X - (Width * 0.5f);
    public float Right => Center.X + (Width * 0.5f);
    public float Top => Center.Y - (Height * 0.5f);
    public float Bottom => Center.Y + (Height * 0.5f);

    public UIBoxCollider() {}

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: solid - solid or not
    public UIBoxCollider(float width, float height)
    {
        // set the properties
        Width = width;
        Height = height;
        Offset = Vector2.Zero;
    }

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: xOffset - x offset of collider
    // param: yOffset - y offset of collider
    // param: solid - solid or not
    public UIBoxCollider(float width, float height, float xOffset, float yOffset)
    {
        // set the properties
        Width = width;
        Height = height;
        Offset = new Vector2(xOffset, yOffset);
    }
}
