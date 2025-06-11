using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MyMonoGameLibrary.Tools;

namespace MyMonoGameLibrary.Scenes;

// component for rectangle collider for gameobject
public class BoxCollider : Component, IRectCollider
{
    // variables and properties
    private Transform _transform;
    private HashSet<string> _collisions = new HashSet<string>();
    
    public int XOffset { get; private set; }
    private float _realXOffset;
    public int YOffset { get; private set; }
    private float _realYOffset;
    public int Width { get; private set; }
    private float _realWidth;
    public int Height { get; private set; }
    private float _realHeight;
    public float Left => _transform.position.X + _realXOffset;
    public float Right => Left + _realWidth;
    public float Top => _transform.position.Y + _realYOffset;
    public float Bottom => Top + _realHeight;

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    public BoxCollider(int width, int height)
    {
        // set the properties
        Width = width;
        Height = height;
        XOffset = -(Width / 2);
        YOffset = -Height;
        
        CalcRealValues();
    }

    // constructor
    //
    // param: width - width of collider
    // param: height - height of collider
    // param: xOffset - x offset of collider
    // param: yOffset - y offset of collider
    public BoxCollider(int width, int height, int xOffset, int yOffset)
    {
        // set the properties
        Width = width;
        Height = height;
        XOffset = xOffset;
        YOffset = yOffset;

        CalcRealValues();
    }

    // calculate teh actual values of the box collider properties
    private void CalcRealValues()
    {
        _realXOffset = (float)XOffset / Camera.SpritePixelsPerUnit;
        _realYOffset = (float)YOffset / Camera.SpritePixelsPerUnit;
        _realWidth = (float)Width / Camera.SpritePixelsPerUnit;
        _realHeight = (float)Height / Camera.SpritePixelsPerUnit;
    }
    
    // initialize
    //
    // param: parent - parent game object
    public override void Initialize(GameObject parent)
    {
        base.Initialize(parent);
        _transform = GetComponent<Transform>();
    }

    // get name of parent
    //
    // return: name of parent
    public string GetName()
    {
        return Parent.Name;
    }

    // what to do when collision happens
    //
    // param: other - other collider
    public void Colliding(IRectCollider other)
    {
        if (!_collisions.Contains(other.GetName()))
        {
            Parent.OnCollisionEnter(other);
            _collisions.Add(other.GetName());
        }
        else
        {
            Parent.OnCollisionStay(other);
        }
    }

    // what to do when not colliding
    //
    // param: other - other collider
    public void NotColliding(IRectCollider other)
    {
        if (_collisions.Contains(other.GetName()))
        {
            _collisions.Remove(other.GetName());
            Parent.OnCollisionExit(other);
        }
    }
}
