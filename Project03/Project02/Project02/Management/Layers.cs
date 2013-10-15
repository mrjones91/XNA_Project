using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project03
{
    public class Layers
    {
        List<List<List<Vector2>>> tileMap;
        List<List<Vector2>> layer;
        List<Vector2> tile;

        List<List<string>> attributes, contents;

        ContentManager content;
        FileManager fileManager;

        Texture2D tileSet;
        Vector2 tileDimensions;

        int layerNumber;

        public int LayerNumber
        {
            set { layerNumber = value; }
        }

        public Vector2 TileDimensions
        {
            get { return tileDimensions; }
        }

        public void LoadContent(ContentManager content, string mapID)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
            fileManager = new FileManager();

            tile = new List<Vector2>();
            layer = new List<List<Vector2>>();
            tileMap = new List<List<List<Vector2>>>();
            attributes = new List<List<string>>();
            contents = new List<List<string>>();

            fileManager.LoadContent("Load/Maps/" + mapID + ".txt", attributes, contents, "Layers");

            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "TileSet":
                            tileSet = this.content.Load<Texture2D>(contents[i][j]);
                            break;
                        case "TileDimensions":
                            string[] boop = contents[i][j].Split(',');
                            tileDimensions = new Vector2(int.Parse(boop[0]), int.Parse(boop[1]));
                            break;
                        case "StartLayer":
                            for(int k = 0; k < contents[i].Count; k++)
                            {
                                boop = contents[i][k].Split(',');
                                tile.Add(new Vector2(int.Parse(boop[0]), int.Parse(boop[1])));
                            }
                            if (tile.Count > 0)
                                layer.Add(tile);
                            tile = new List<Vector2>();
                            break;
                        case "EndLayer":
                            if (layer.Count > 0)
                                tileMap.Add(layer);
                            layer = new List<List<Vector2>>();
                            break;
                    }//Switch
                }//for j
            }
        }// LoadContent
        public void UnloadContent()
        {
            this.content.Unload();
            tileMap.Clear();
            layer.Clear();
            tile.Clear();
            attributes.Clear();
            contents.Clear();
            fileManager = null;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            for (int k = 0; k < tileMap.Count; k++)
            {
                for (int i = 0; i < tileMap[k].Count; i++)
                {
                    for (int j = 0; j < tileMap[k][i].Count; j++)
                    {
                        spriteBatch.Draw(tileSet, new Vector2(j * tileDimensions.X, i * tileDimensions.Y), new Rectangle((int)tileMap[layerNumber][i][j].X * (int)tileDimensions.X,
                            (int)tileMap[layerNumber][i][j].Y * (int)tileDimensions.Y, (int)tileDimensions.X, (int)tileDimensions.Y), Color.White);
                    }
                }
            }
        }
    }
}
