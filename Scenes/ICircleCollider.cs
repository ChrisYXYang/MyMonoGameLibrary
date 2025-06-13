using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scenes;

// circle collider interface
public interface ICircleCollider : ICollider
{
    // radius of collider
    public float Radius { get; }
}
