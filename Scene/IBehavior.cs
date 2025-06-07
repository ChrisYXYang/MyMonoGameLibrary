using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scene;

// this interface represents scripts for object behavior
public interface IBehavior
{
    // update the script
    public void Update();
    
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
