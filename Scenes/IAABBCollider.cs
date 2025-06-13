using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scenes;

// interface for rectangle collider
public interface IAABBCollider : ICollider
{
    // properties
    public float Left { get; }
    public float Top { get; }
    public float Right { get; }
    public float Bottom { get; }
    public float Width { get; }
    public float Height { get; }
}
