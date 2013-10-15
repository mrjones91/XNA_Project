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
    public class PongPlayScreen : GameScreen
    {

        Camera camera;
        PongPaddle paddle;
        PongBall ball;
        Rectangle B, P;
        SpriteFont font;

        float points;

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            
            points = 0;

            camera = new Camera();
            paddle = new PongPaddle();
            ball = new PongBall();
            
            base.LoadContent(Content, inputManager);
            paddle.LoadContent(content, inputManager);
            ball.LoadContent(content, inputManager);

            if (font == null)
                font = this.content.Load<SpriteFont>("SpriteFont1");

            B = new Rectangle((int)ball.Position.X, (int)ball.Position.Y, (int)ball.Image.Width, (int)ball.Image.Height);
            P = new Rectangle((int)paddle.Position.X, (int)paddle.Position.Y, (int)paddle.Image.Width, (int)paddle.Image.Height);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            paddle.UnloadContent();
            ball.UnloadContent();
        }

        public override void Draw(SpriteBatch spritesB)
        {
            base.Draw(spritesB);
            paddle.Draw(spritesB);
            ball.Draw(spritesB);
            spritesB.DrawString(font, "Points: " + (int)points, new Vector2(25, 25), Color.White);
        }

        public override void Update(GameTime gameTime)
        {
            points += (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            base.Update(gameTime);
            inputManager.Update();
            paddle.Update(gameTime, inputManager, null, null);
            ball.Update(gameTime, inputManager, null, null);

            

            B = new Rectangle((int)ball.Position.X, (int)ball.Position.Y, (int)ball.Image.Width, (int)ball.Image.Height);
            P = new Rectangle((int)paddle.Position.X, (int)paddle.Position.Y, (int)paddle.Image.Width, (int)paddle.Image.Height);

            //if ( B.Intersects(P) )(B.Left == P.Right) && (B.Bottom == P.Top) && (B.Right == P.Left)
            if ( B.Intersects(P))
            {
                ball.Speed.Y += 50;
                if (ball.Speed.X < 0)
                    ball.Speed.X -= 50;
                else
                    ball.Speed.X += 50;

                ball.Speed.Y *= -1;
            }
            else if ((B.Left == P.Right) || (B.Right == P.Left))
            {
                ball.Speed.X += 25;
                if (ball.Speed.Y < 0)
                    ball.Speed.Y -= 50;
                else
                    ball.Speed.Y += 50;
                
                ball.Speed.X *= -1;
            }
            
        }
    }
}
