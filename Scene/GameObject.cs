using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Mime;

namespace MyMonoGameLibrary.Scene;

// This class represents the objects in the game. Each object will have multiple components.
public class GameObject : ICollidable, IRenderable, IAnimatable
{
    // variables and properties
    private Dictionary<string, Component> _components = new Dictionary<string, Component>();
    private Dictionary<string, IBehavior> _behaviors = new Dictionary<string, IBehavior>();
    
    // constructs the game object using information from xml file
    //
    // param: fileName - xml file that stores game object information
    public GameObject(string fileName)
    {
        // read and use information from the xml file
        string filePath = Path.Combine("Content", fileName) + ".xml";

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
                    string typeName = "MyMonoGameLibrary.Scene." + componentName;

                    bool behavior = false;
                    if (componentName.Contains("Behavior"))
                    {
                        behavior = true;
                        typeName = componentName;
                    }

                    Component newComponent =
                                (Component)Activator.CreateInstance
                                                (
                                                    Type.GetType(typeName),
                                                    [attributes]
                                                );
                    _components.Add(componentName, newComponent);

                    if (behavior)
                        _behaviors.Add(componentName, (IBehavior)newComponent);
                }
            }
        }

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

    // on collision with other collider
    //
    // param: other - other collider
    public void OnCollision(IRectCollider other)
    {

    }

    // get the collider
    //
    // return: box collider
    public IRectCollider GetCollider()
    {
        return GetComponent<BoxCollider>();
    }

    // get the renderer
    //
    // return: sprite renderer
    public IRenderer GetRenderer()
    {
        return GetComponent<SpriteRenderer>();
    }

    // get the animator
    //
    // return: animator
    public IAnimator GetAnimator()
    {
        return GetComponent<Animator>();
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

