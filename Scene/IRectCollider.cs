using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scene;

// interface for rectangle collider
public interface IRectCollider
{
    // properties
    public float Left { get; }
    public float Top { get; }
    public float Right { get; }
    public float Bottom { get; }

    // get name of parent
    //
    // return: name of parentt
    public string GetName();

    // what to do when colliding
    //
    // param: other - other collider
    public void Colliding(IRectCollider other);

    // what to do when not colliding
    public void NotColliding(IRectCollider other);

}
