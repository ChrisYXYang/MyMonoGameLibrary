using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Scenes;

namespace MyMonoGameLibrary.UI;

// this class represents every UI element in the game (including the canvas).
public abstract class UIElement
{
    // parent element
    public UIElement Parent { get; private set; }
    
    // number of children
    public int ChildCount { get => _children.Count; }

    // children of elemente
    private readonly List<BaseUI> _children = [];

    // add a child
    //
    // param: child - child to add
    public void AddChild(BaseUI child)
    {
        _children.Add(child);
        child.Parent?.RemoveChild(child);
        child.Parent = this;
    }

    // remove a child
    //
    // param: child - child to remove
    public void RemoveChild(BaseUI child)
    {
        child.Parent = null;
        _children.Remove(child);
    }

    // get all children
    //
    // return: list of children
    public List<BaseUI> GetChildren()
    {
        BaseUI[] output = new BaseUI[_children.Count];
        _children.CopyTo(output);
        return [.. output];
    }

    // get a child
    //
    // param: index - index of child in list
    // return: chosen child based on index
    public BaseUI GetChild(int index)
    {
        return _children[index];
    }

    public virtual void Start()
    {

    }

    public virtual void Update(GameTime gameTime)
    {

    }
}
