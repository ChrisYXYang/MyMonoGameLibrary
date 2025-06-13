using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

// interface for colliders
public interface ICollider
{
    // center of collider
    public Vector2 Center { get; }

    // get name of parent
    //
    // return: name of parent
    public string GetName();

    // what to do when colliding
    //
    // param: other - other collider
    public void Colliding(ICollider other);

    // what to do when not colliding
    //
    // param: other - other collider
    public void NotColliding(ICollider other);
}
