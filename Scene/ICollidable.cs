using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scene;

public interface ICollidable
{
    // get the collider of object
    //
    // return: the collider
    public IRectCollider GetCollider();
}
