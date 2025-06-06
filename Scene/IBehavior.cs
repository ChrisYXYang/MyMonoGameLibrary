using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scene;

// this interface represents scripts for object behavior
internal interface IBehavior
{
    // what to do when just collided
    //
    // param: other - other collider
    public void OnCollisionEnter(IRectCollider other);

    // what to do when collision exit
    public void OnCollisionExit(IRectCollider other);

    // what to do when ongoing collision
    public void OnCollisionStay(IRectCollider other);
}
