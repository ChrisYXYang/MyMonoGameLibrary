using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Linq;

namespace MyMonoGameLibrary;

// This class represents the objects in the game. Each object will have multiple components.
public class GameObject
{
    // variables and properties
    private Dictionary<string, Component> _components = new Dictionary<string, Component>();
    private SpriteManager _spriteManager;
    
    // constructs the game object using information from xml file
    //
    // param: fileName - xml file that stores game object information
    public GameObject(string fileName)
    {
        // read and use information from the xml file
        string filePath = Path.Combine("Content", fileName);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;

                // create component for each element in <Components>
                var components = root.Element("Components").Elements();

                foreach (var component in components)
                {
                    // store all attributes in component
                    Dictionary<string, string> attributes = new Dictionary<string, string>();

                    foreach (var attribute in component.Attributes())
                    {
                        attributes.Add(attribute.Name.ToString(), attribute.Value);
                    }

                    // create the new component
                    string componentName = component.Name.ToString();
                    Component newComponent =
                        (Component)Activator.CreateInstance(Type.GetType(componentName), [attributes]);
                    _components.Add(componentName.Split(".").Last(), newComponent);
                }
            }
        }

        // set sprite manager
        _spriteManager = GetComponent<SpriteManager>();

        // initialize all components
        foreach (var entry in _components)
        {
            entry.Value.Initialize(this);
        }
    }

    // get chosen component from game object.
    //
    // return: chosen component, null if no component
    public T GetComponent<T>() where T : Component
    {
        string chosenComponent = typeof(T).Name;

        if (_components.ContainsKey(chosenComponent))
        {
            return (T)_components[chosenComponent];
        }

        return null;
    }

    // draw this game object
    public void Draw()
    {
        _spriteManager.Draw();
    }

    // 4 testing
    public void PrintComponents()
    {
        foreach (var entry in _components)
        {
            Debug.WriteLine(entry.Key + "\n");
        }
    }
}

