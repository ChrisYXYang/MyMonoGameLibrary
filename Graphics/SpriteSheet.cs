using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.IO;
using System.Reflection.Metadata.Ecma335;
using System.Xml;
using System.Xml.Linq;

namespace MyMonoGameLibrary.Graphics;

// This class represents a sprite sheet. It will contain all the Sprites made from the sprite sheet.
public class SpriteSheet
{
    // variables and properties
    private Dictionary<string, Sprite> _sprites = new Dictionary<string, Sprite>();
    private Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();
    public Texture2D Sheet { get; private set; }

    // constructor for SpriteSheet. Will create a dictionary containing user-defined
    // Sprites in the spritesheet. Will also create a dictionary containing user-defined Animations
    // in the spritesheet.
    //
    // param: content - content manager to load the asset
    // param: fileName - xml file for sprite sheet information
    public SpriteSheet(ContentManager content, string fileName)
    {
        // read and use information from the xml file
        string filePath = "Content/images/" + fileName + ".xml";

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;

                // load sprite sheet texture
                var settings = root.Element("Settings");
                string texture = settings.Attribute("texture").Value;
                Sheet = content.Load<Texture2D>(texture);

                // get general sprite information
                int size = int.Parse(settings.Attribute("size").Value);
                bool centered = bool.Parse(settings.Attribute("centered")?.Value ?? "true");

                // retrieve all <Sprite> elements and create new Sprite for each element
                var sprites = root.Element("Sprites").Elements("Sprite");

                int x = 0;
                int y = 0;
                if (sprites != null)
                {
                    foreach (var sprite in sprites)
                    {
                        // get name and origin point
                        string name = sprite.Attribute("name").Value;
                        float originX = centered ? size * 0.5f : 0;
                        float originY = centered ? size * 0.5f : 0;

                        string tempX = sprite.Attribute("originX")?.Value;
                        if (tempX != null)
                            originX = float.Parse(tempX);

                        string tempY = sprite.Attribute("originY")?.Value;
                        if (tempY != null)
                            originY = float.Parse(tempY);

                        // create new sprite
                        Sprite newSprite = new Sprite(Sheet, new Vector2(originX, originY), x, y, size);
                        _sprites.Add(name, newSprite);


                        // move source rectangle to next sprite
                        x += size;

                        if (x == Sheet.Width)
                        {
                            x = 0;
                            y += size;
                        }
                    }
                }

                // retrive all <Animation> elements and create new Animation for each element
                var animations = root.Element("Animations")?.Elements("Animation");

                if (animations != null)
                {
                    foreach (var animation in animations)
                    {
                        // get name and delay
                        string name = animation.Attribute("name").Value;
                        double delayTime = double.Parse(animation.Attribute("delay")?.Value ?? "100");
                        TimeSpan delay = TimeSpan.FromMilliseconds(delayTime);

                        // get all frames from <Frame> elements
                        List<Sprite> allFrames = new List<Sprite>();
                        var frames = animation.Elements("Frame");

                        if (frames != null)
                        {
                            foreach(var frame in frames)
                            {
                                string frameName = frame.Attribute("name").Value;
                                allFrames.Add(GetSprite(frameName));
                            }
                        }

                        // create new animation
                        Animation newAnimation = new Animation(allFrames, delay);
                        _animations.Add(name, newAnimation);
                    }
                }
            }
        }
    }

    // get a sprite
    //
    // param: spriteName - name of sprite
    // return: chosen Sprite
    public Sprite GetSprite(string spriteName)
    {
        return _sprites[spriteName];
    }

    // get an animation
    //
    // param: animationName - name of animation
    // return: chosen Animation
    public Animation GetAnimation(string animationName)
    {
        return _animations[animationName];
    }
}
