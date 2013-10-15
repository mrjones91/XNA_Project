using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Project03
{
    public class PongBall : Entity
    {
        public Vector2 Speed;
        float maxX, maxY;

        public override void LoadContent(ContentManager content, InputManager inputmanager)
        {
            moveAnimation = new SpriteSheetAnimation();
            //moveAnimation.Frames = new Vector2(0, 0);
            base.LoadContent(content, inputmanager);
            fileManager = new FileManager();
            //load image
            fileManager.LoadContent("Load/Ball.txt", attributes, contents);
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
                        //case for individual Frames per object
                        case "Frames":
                            string[] frms = contents[i][j].Split(',');
                            moveAnimation.Frames = new Vector2( int.Parse(frms[0]), int.Parse(frms[1]) );
                            break;
                    }

                }
            }
            moveAnimation.LoadContent(content, image, "", null, position);

            
            maxX = ScreenManager.Instance.Dimensions.X - image.Width;
            maxY = ScreenManager.Instance.Dimensions.Y - image.Height;

            moveSpeed = 150f;
            Speed = new Vector2(moveSpeed, moveSpeed);
            Position = Vector2.Zero;

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

            position += Speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            //Wall Bounce
            if (position.X > maxX || position.X < 0)
                Speed.X *= -1;
            if (position.Y > maxY || position.Y < 0)
                Speed.Y *= -1;
            
            moveAnimation.IsActive = true;

            moveAnimation.Position = position;
            moveAnimation.Update(gameTime);
           
        }
    }
}
