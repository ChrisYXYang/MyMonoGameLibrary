using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System.Xml.Linq;
using System.Xml;
using MyMonoGameLibrary.Graphics;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

namespace MyMonoGameLibrary.Tilemap;

// this class represents a tile set
public class Tileset
{
    // variables and properties
    private Sprite[,] _tiles;
    private int _rows;
    private int _columns;
    public Texture2D Set { get; private set; }

    // constructor. Will create a 2d array containing all tiles in the tileset
    //
    // param: content - content manager to load asset
    // param: fileName - xml file for tile set information
    public Tileset(ContentManager content, string fileName)
    {
        // read and use information from the xml file
        string filePath = "Content/images/" + fileName + ".xml";

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;

                // load tile set texture
                var settings = root.Element("Settings");
                string texture = settings.Attribute("texture").Value;
                Set = content.Load<Texture2D>(texture);

                // get general tile sprite information
                int size = int.Parse(settings.Attribute("size").Value);

                // load every tile into tiles 2d array
                _rows = Set.Height / size;
                _columns = Set.Width / size;
                _tiles = new Sprite[_rows, _columns];
                for (int i = 0; i < _rows; i++)
                {
                    for (int j = 0; j < _columns; j++)
                    {
                        // create new tile sprite
                        Sprite newTile = new Sprite(Set, new Vector2(size * 0.5f, size), j * size, i * size, size);
                        _tiles[i, j] = newTile;
                    }
                }
            }
        }
    }

    // get a tile form tile number
    //
    // param: tileNumber - tile number
    // return: chosen tile
    public Sprite GetTile(int tileNumber)
    {
        return _tiles[(tileNumber-1) / _columns, (tileNumber-1) % _columns];
    }
}
