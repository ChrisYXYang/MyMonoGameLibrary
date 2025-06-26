using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// base collider class for UI elements
public abstract class UICollider : ICollider
{
    public string Layer { get; set; }

    public Vector2 Offset { get; set; }

    // center of collider
    public Vector2 Center => Parent.position + Offset;

    protected BaseUI Parent { get; private set; }

    public UICollider(BaseUI parent)
    {
        Parent = parent;
    }

    // get name of parent
    //
    // return: name of parent
    public virtual string GetName()
    {
        return null;
    }

    // what to do when collision happens
    //
    // param: other - other collider
    public virtual void Colliding(ICollider other) { }

    // what to do when not colliding
    //
    // param: other - other collider
    public virtual void NotColliding(ICollider other) { }
}
