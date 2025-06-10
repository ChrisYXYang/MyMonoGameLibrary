using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scenes;

// interface for objects that can use colliders
public interface ICollidable
{
    // get the collider of object
    //
    // return: the collider
    public IRectCollider GetCollider();
}
