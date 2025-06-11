using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scenes;

// interface for classes responsible for drawing an object
public interface IGameRenderer
{
    // draw the object
    public void Draw();
}
