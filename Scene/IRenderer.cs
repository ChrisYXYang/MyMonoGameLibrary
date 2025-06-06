using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scene;

// interface for classes responsible for drawing an object
public interface IRenderer
{
    // draw the object
    public void Draw();
}
