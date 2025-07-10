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
using MyMonoGameLibrary.Scenes;


namespace MyMonoGameLibrary.Tilemap;

// this class represents a tilemap
public class TileMap
{
    // variables and properties
    public int Rows { get; private set; }
    public int Columns { get; private set; }
    public List<string> Layers => _tilemap.Keys.ToList<string>();

    private Dictionary<string, Tile[,]> _tilemap = new Dictionary<string, Tile[,]>();
    public string Name {  get; private set; }

    // constructor. Creates a tile map from xml file
    //
    // param: fileName - name of xml file
    public TileMap(string fileName, Tileset tileSet)
    {
        Name = fileName;
        
        foreach(var entry in _tilemap)
        {
            Debug.WriteLine(entry.Key);
        }

        // read and use information from the xml file
        string filePath = "Content/Tilemaps/" + fileName + ".xml"; 

        using (Stream stream = TitleContainer.OpenStream(filePath))
        {
            using (XmlReader reader = XmlReader.Create(stream))
            {
                XDocument doc = XDocument.Load(reader);
                XElement root = doc.Root;

                // get general tilemap information
                Rows = int.Parse(root.Attribute("height").Value);
                Columns = int.Parse(root.Attribute("width").Value);
                int tileSize = int.Parse(root.Attribute("tilewidth").Value);
                // construct layers
                var layers = root.Elements("layer");

                if (layers != null)
                {
                    int layerDepth = 0;
                    foreach (var layer in layers)
                    {
                        // get layer name
                        string layerName = layer.Attribute("name").Value;
                        string layerLayer = layer.Attribute("layer").Value;
                        bool collide = layerName.Contains("col");
                        bool solid = layerName.Contains("sol");

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
                                                            Name + "_" + layerName + " tile r" + i + "col " + j,
                                                            tileSet.GetTile(int.Parse(line_tiles[j])),
                                                            new Vector2(j + 0.5f - (float)Columns / 2, i - (float)Rows / 2 + 0.5f),
                                                            new Vector2(i,j),
                                                            tileSize,
                                                            0 + layerDepth * 0.01f,
                                                            collide,
                                                            solid,
                                                            layerLayer
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
