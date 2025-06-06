using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scene;

// this interface represents scripts for object behavior
internal interface IBehavior
{
    // what to do when collision happens
    //
    // param: other - other collider
    public void OnCollision(IRectCollider other);
}
