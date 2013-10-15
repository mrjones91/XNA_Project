using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project03
{
    public class Player: Entity
    {
       
        public override void LoadContent(ContentManager content, InputManager inputmanager)
        {
            base.LoadContent(content, inputmanager);
            fileManager = new FileManager();
            moveAnimation = new SpriteSheetAnimation();
            string[] tempFrames = null;
            moveSpeed = 100f;

            fileManager.LoadContent("Load/Player.txt", attributes, contents);

            for (int i = 0; i < attributes.Count; i ++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Health":
                            health = int.Parse(contents[i][j]);
                            break;
                        case "Frames":
                            tempFrames = contents[i][j].Split(' ');
                            //moveAnimation.Frames = new Vector2(int.Parse(tempFrames[0]), int.Parse(tempFrames[1]));
                            break;
                        case "Image":
                            image = this.content.Load<Texture2D>(contents[i][j]);
                            break;
                        case "Position":
                            string[] posit = contents[i][j].Split(' ');
                            position = new Vector2(int.Parse(posit[0]), int.Parse(posit[1]));
                            break;

                    }

                }
            }
            moveAnimation.LoadContent(content, image, "", null, position);
            moveAnimation.Frames = new Vector2(int.Parse(tempFrames[0]), int.Parse(tempFrames[1]));
            
      
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            moveAnimation.UnloadContent();
        }

        public override void Update(GameTime gameTime, InputManager inputmanager, Collision col, Layers layer)
        {
            if (position == new Vector2(160, 160) || position == new Vector2(960, 160) ||
                position == new Vector2(1120, 1024) || position == new Vector2(160, 1120))
                health--;
            //Animation
            moveAnimation.IsActive = true;

            if (inputmanager.KeyDown(Keys.Down, Keys.S))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 0);
                position.Y += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (inputmanager.KeyDown(Keys.Left, Keys.A))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (inputmanager.KeyDown(Keys.Right, Keys.D))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 2);
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (inputmanager.KeyDown(Keys.Up, Keys.W))
            {
                moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 3);
                position.Y -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                //CurrentFrame set to default in SpriteSheetAnimation.cs
                //moveAnimation.CurrentFrame = new Vector2(1, moveAnimation.CurrentFrame.Y);
                moveAnimation.IsActive = false;
            }

            //Collision
            for (int i = 0; i < col.CollisionMap.Count; i++)
            {
                for (int j = 0; j < col.CollisionMap[i].Count; j++)
                {
                    if (col.CollisionMap[i][j] == "x")
                    {
                        if (position.X + moveAnimation.FrameWidth < j * layer.TileDimensions.X ||
                            position.X > j * layer.TileDimensions.X + layer.TileDimensions.X ||
                            position.Y + moveAnimation.FrameHeight < i * layer.TileDimensions.Y ||
                            position.Y > i * layer.TileDimensions.Y + layer.TileDimensions.Y)
                        {

                        }
                        else
                        {
                            position = moveAnimation.Position;
                        }
                    }
                }
            }
            moveAnimation.Position = position;
            moveAnimation.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            moveAnimation.Draw(spriteBatch);
        }
    }
}
