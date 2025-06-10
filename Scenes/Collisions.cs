using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scenes;

// this class contains static methods that deal with detecting collisions
public static class Collisions
{
    // helper method to check if two AABB (rectangle) colliders intersect
    //
    // param: a - first AABB collider
    // param: b - second AABB collider
    // return: whether they intersect
    public static bool AABBIntersect(IRectCollider a, IRectCollider b)
    {
        return a.Left < b.Right &&
        b.Left < a.Right &&
        a.Top < b.Bottom &&
        b.Top < a.Bottom;
    }
}
