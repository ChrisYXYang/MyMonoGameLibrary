using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;


namespace MyMonoGameLibrary.Tilemap;

// this class represents a tilemap
public class TileMap
{
    private Dictionary<string, Tile[,]> _tileGrid;
    
    //public TileMap(Tileset tileSet, int[,] grid, int size, int layer)
    //{
    //    int rows = grid.GetLength(0);
    //    int cols = grid.GetLength(1);
    //    _tileGrid = new Tile[rows, cols];

    //    for (int i = 0; i < rows; i++)
    //    {
    //        for (int j = 0; j < cols; j++)
    //        {
    //            _tileGrid[i, j] = new Tile(new Vector2(i - (cols / 2), j - (rows / 2)), size, 0 + (layer * 0.1f), false);
    //        }
    //    }
    //}
    

    public TileMap(string fileName)
    {
        // read and use information from the xml file
        string filePath = Path.Combine("Content", fileName);

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;

                // get general tilemap information
                var settings = root.Element("Settings");
                int rows = int.Parse(settings.Attribute("height").Value);
                int cols = int.Parse(settings.Attribute("width").Value);
                int tileSize = int.Parse(settings.Attribute("tileSize").Value);

                // construct layers
                var layers = root.Elements("layer");

                if (layers != null)
                {
                    foreach (var layer in layers)
                    {
                        // get layer name
                        string layerName = layer.Attribute("name").Value;

                        // create the tile grid from data csv
                        var data = layer.Element("data");
                        Debug.WriteLine(data.Value);

                        string[] grid_lines = data.Value.Split("\n");
                        int[,] tileGrid  = new int[rows, cols];
                        
                        for (int i = 0; i < grid_lines.Length; i++)
                        {
                            Debug.WriteLine(grid_lines[i]);
                            string[] line_tiles = grid_lines[i].Trim().Split(",");

                            for (int j = 0; j < line_tiles.Length; j++)
                            {
                                tileGrid[i, j] = int.Parse(line_tiles[j]);
                            }
                        }

                        Debug.WriteLine(layerName);
                        Debug.WriteLine(tileGrid.ToString());
                    }
                }
            }
        }
    }
}
