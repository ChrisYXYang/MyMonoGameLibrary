using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary;

// interface for rectangle collider
public interface RectCollider
{
    // properties
    public float Left { get; }
    public float Top { get; }
    public float Right { get; }
    public float Bottom { get; }

    // see if intersects between another rect collider
    //
    // param: other - other collider
    // return: intersect or not
    public bool Intersects(RectCollider other);
}
