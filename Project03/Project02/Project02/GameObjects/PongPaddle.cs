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
    public class PongPaddle : Entity
    {
        

        public override void LoadContent(ContentManager content, InputManager inputmanager)
        {
            moveSpeed = 300f;
            moveAnimation = new SpriteSheetAnimation();

            base.LoadContent(content, inputmanager);
            fileManager = new FileManager();
            //make paddle
            fileManager.LoadContent("Load/Paddle.txt", attributes, contents);
            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Image":
                            image = this.content.Load<Texture2D>(contents[i][j]);
                            break;
                        case "Position":
                            string[] frames = contents[i][j].Split(' ');
                            position = new Vector2(int.Parse(frames[0]), int.Parse(frames[1]));
                            break;
                    }

                }
            }
            moveAnimation.LoadContent(content, image, "", null, position);
           
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            moveAnimation.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime, InputManager inputmanager, Collision col, Layers layer)
        {
            base.Update(gameTime, inputmanager, col, layer);
            moveAnimation.IsActive = true;
            if (inputmanager.KeyDown(Keys.Left, Keys.A))
            {
                //moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
                position.X -= moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (inputmanager.KeyDown(Keys.Right, Keys.D))
            {
                //moveAnimation.CurrentFrame = new Vector2(moveAnimation.CurrentFrame.X, 1);
                position.X += moveSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                moveAnimation.IsActive = false;
            }
            moveAnimation.Position = position;
            moveAnimation.Update(gameTime);
        }
    }
}
