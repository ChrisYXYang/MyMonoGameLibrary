using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace MyMonoGameLibrary.Scene;

// this interface represents scripts for object behavior
public interface IGameBehavior : IBehavior
{    
    // what to do when just collided
    //
    // param: other - other collider
    public void OnCollisionEnter(IRectCollider other);

    // what to do when collision exit
    //
    // param: other - other collider
    public void OnCollisionExit(IRectCollider other);

    // what to do when ongoing collision
    //
    // param: other - other collider
    public void OnCollisionStay(IRectCollider other);
}
