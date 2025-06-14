using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Input;

namespace MyMonoGameLibrary.Scenes;

// this class contains static methods that deal with detecting collisions
public static class Collisions
{
    // check interserct between two collliders
    //
    // param: a - first collider
    // parma: b - second collider
    public static bool Intersect(ICollider a, ICollider b)
    {
        // both colliders are rects
        {
            if (a is IAABBCollider rectA && b is IAABBCollider rectB)
                return AABBIntersect(rectA, rectB);
        }

        // a is rect, b is circle
        {
            if (a is IAABBCollider rectA)
                return MixIntersect((ICircleCollider)b, rectA);
        }

        // both are circles
        {
            if (b is ICircleCollider circB)
                return CircleIntersect((ICircleCollider)a, circB);
        }

        // a is circle, b is rect
        return MixIntersect((ICircleCollider)a, (IAABBCollider)b);
    }

    // helper method to check if two AABB (rectangle) colliders intersect
    //
    // param: a - first AABB collider
    // param: b - second AABB collider
    // return: whether they intersect
    private static bool AABBIntersect(IAABBCollider a, IAABBCollider b)
    {
        return a.Left < b.Right &&
        b.Left < a.Right &&
        a.Top < b.Bottom &&
        b.Top < a.Bottom;
    }

    // helper method to check if two circle colliders intersect
    //
    // param: a - first circle collider
    // param: b - second circle collider
    // return: whether they intersect
    private static bool CircleIntersect(ICircleCollider a, ICircleCollider b)
    {
        return ((a.Radius + b.Radius) * (a.Radius + b.Radius)) > Vector2.DistanceSquared(a.Center, b.Center);
    }

    // helper method to check if one circle collider and one aabb collider intersect
    //
    // param: a - circle collider
    // param: b - second AABB collider
    // return: whether they intersect
    private static bool MixIntersect(ICircleCollider circle, IAABBCollider rect)
    {
        float yDist = MathF.Abs(circle.Center.Y - rect.Center.Y);
        float xDist = MathF.Abs(circle.Center.X - rect.Center.X);

        if (yDist >= (rect.Height * 0.5f) + circle.Radius)
            return false;

        if (xDist >= (rect.Width * 0.5f) + circle.Radius)
            return false;

        if (xDist <= (rect.Width * 0.5f))
            return true;

        if (yDist <= (rect.Height * 0.5f))
            return true;

        return (circle.Radius * circle.Radius) > Vector2.DistanceSquared(new Vector2(xDist, yDist), 
            new Vector2(rect.Width * 0.5f, rect.Height * 0.5f));
    }

    // check if point is in collider
    //
    // param: collider - collider to check
    // param: point - point to check
    public static bool PointInCollider(ICollider collider, Vector2 point)
    {
        if (collider is IAABBCollider rect)
        {
            return PointInAABB(rect, point);
        }

        return PointInCircle((ICircleCollider)collider, point);
    }

    // check fi point is in circle collider
    //
    // param: collider - collider to check
    // param: point - point to check
    private static bool PointInCircle(ICircleCollider collider, Vector2 point)
    {
        return collider.Radius * collider.Radius > Vector2.DistanceSquared(collider.Center, point);
    }

    // check fi point is in box collider
    //
    // param: collider - collider to check
    // param: point - point to check
    private static bool PointInAABB(IAABBCollider collider, Vector2 point)
    {
        float distX = MathF.Abs(point.X - collider.Center.X);
        float distY = MathF.Abs(point.Y - collider.Center.Y);

        return distX < (collider.Width * 0.5f) && distY < (collider.Height * 0.5f);
    }
}
