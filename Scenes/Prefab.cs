using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyMonoGameLibrary.Scenes;

// this class represents a prefab to be instantiated
public class Prefab
{
    public string Name { get; set; }
    public readonly Component[] components;
    public readonly Prefab[] children;

    // constructor
    //
    // param: name - name
    // param: components - components
    public Prefab(string name, Component[] components)
    {
        Name = name;
        this.components = components;
        this.children = [];
    }

    // constructor
    //
    // param: name - name
    // param: components - components
    // param: children - children prefabs
    public Prefab(string name, Component[] components, Prefab[] children)
    {
        Name = name;
        this.components = components;
        this.children = children;
    }
}
