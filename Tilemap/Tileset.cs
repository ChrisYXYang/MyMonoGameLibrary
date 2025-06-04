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
    // param: fileName - xml file for tile set information
    public Tileset(string fileName)
    {
        // read and use information from the xml file
        string filePath = Path.Combine("Content", fileName);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;

                // load tile set texture
                var settings = root.Element("Settings");
                string texture = settings.Attribute("texture").Value;
                Set = Core.Content.Load<Texture2D>(texture);

                // get general tile sprite information
                int size = int.Parse(settings.Attribute("size").Value);

                // load every tile into tiles 2d array
                _rows = Set.Width / size;
                _columns = Set.Height / size;
                _tiles = new Sprite[_rows, _columns];
                for (int i = 0; i < _rows; i++)
                {
                    for (int j = 0; j < _columns; j++)
                    {
                        // create new tile sprite
                        Sprite newTile = new Sprite(Set, new Vector2(0, size), i * size, j * size, size);
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
        return _tiles[tileNumber / _columns, tileNumber % _columns];
    }
}
