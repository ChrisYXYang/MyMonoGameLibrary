using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using MyMonoGameLibrary.Graphics;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using System.Diagnostics;
using System.Linq;


namespace MyMonoGameLibrary.Tilemap;

// this class represents a tilemap
public class TileMap
{
    // variables and properties
    public int Rows { get; private set; }
    public int Columns { get; private set; }
    public List<string> Layers => _tilemap.Keys.ToList<string>();

    private Dictionary<string, Tile[,]> _tilemap = new Dictionary<string, Tile[,]>();

    // constructor. Creates a tile map from xml file
    //
    // param: fileName - name of xml file
    public TileMap(string fileName)
    {
        foreach(var entry in _tilemap)
        {
            Debug.WriteLine(entry.Key);
        }
        
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
                Rows = int.Parse(settings.Attribute("height").Value);
                Columns = int.Parse(settings.Attribute("width").Value);
                int tileSize = int.Parse(settings.Attribute("tileSize").Value);
                Tileset tileSet = SpriteLibrary.GetTileset(settings.Attribute("tileset").Value);
                // construct layers
                var layers = root.Elements("layer");

                if (layers != null)
                {
                    int layerDepth = 0;
                    foreach (var layer in layers)
                    {
                        // get layer name
                        string layerName = layer.Attribute("name").Value;
                        bool collide = bool.Parse(layer.Attribute("collide").Value);

                        // create the tile grid from data csv
                        var data = layer.Element("data");
                        string[] grid_lines = data.Value.Trim().Split("\n");
                        Tile[,] tileGrid  = new Tile[Rows, Columns];
                        
                        for (int i = 0; i < grid_lines.Length; i++)
                        {
                            grid_lines[i] = grid_lines[i].Trim();
                            
                            if (i < grid_lines.Length - 1)
                                grid_lines[i] = grid_lines[i].Substring(0, grid_lines[i].Length - 1);

                            string[] line_tiles = grid_lines[i].Split(",");

                            for (int j = 0; j < line_tiles.Length; j++)
                            {
                                int tileNum = int.Parse(line_tiles[j]);
                                if (tileNum != 0)
                                {
                                    tileGrid[i, j] = new Tile
                                                        (
                                                            tileSet.GetTile(int.Parse(line_tiles[j])),
                                                            new Vector2(j - (float)Columns / 2, i - (float)Rows / 2 + 1),
                                                            tileSize,
                                                            0 + layerDepth * 0.1f,
                                                            collide
                                                         );
                                }
                            }
                        }

                        // add the new layer to tile map dictionary
                        _tilemap.Add(layerName, tileGrid);
                        layerDepth++;
                    }
                }
            }
        }
    }

    // draw the tilemap
    public void Draw()
    {
        foreach (Tile[,] layer in _tilemap.Values)
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    if (layer[i, j] != null)
                        layer[i,j].Draw();
                }
            }
        }
    }

    // get a tile
    //
    // param: layerName - layer to get tile from
    // param: row - which row tile is in
    // param: col - which column
    // return: Tile requested, or null if no tile there
    public Tile GetTile(string layerName, int row, int col)
    {
        return _tilemap[layerName][row, col];
    }
}
