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

    // get parent of collider
    //
    // return: parent object
    public ICollidable GetParent();
}
