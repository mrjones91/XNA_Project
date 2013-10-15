using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project03
{
    public class GamePlayScreen: GameScreen
    {
        double time, changeTime;
        int minute;
        SpriteFont font;
        Player player;
        Bot dude;
        Map map;
        Color changeColor;
        byte red, gre, blu;
        Random rand;
        //Layers layer;
        Camera camera;

        public override void LoadContent(ContentManager content, InputManager inputmanager)
        {
            rand = new Random();
            red = (byte)rand.Next(255);
            gre = (byte)rand.Next(255);
            blu = (byte)rand.Next(255);
            changeColor = new Color(red, gre, blu);
            time = 0;
            changeTime = 10;
            minute = 0;
            base.LoadContent(content, inputmanager);
            font = this.content.Load<SpriteFont>("SpriteFont1");
            map = new Map();
            dude = new Bot();
            dude.LoadContent(content, inputmanager);
            player = new Player();
            player.LoadContent(content, inputmanager);
            map.LoadContent(content, "Map01");
            camera = new Camera();
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            player.UnloadContent();
            dude.UnloadContent();
            map.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            {
                red ++;
                gre ++;
                blu ++;
                
            }
            if (dude.Health != 0)
                time += 0.0166667;
            else
            {
                changeTime -= 0.016667;
            }
            if (time > 60)
            {
                time -= 60;
                minute++;
            }

            inputManager.Update();
            player.Update(gameTime, inputManager, map.collision, map.layer);
            dude.Update(gameTime, inputManager, map.collision, map.layer);
            map.Update(gameTime);
            camera.Update(dude.Position);
            if (changeTime <= 1)
            {
                ScreenManager.Instance.AddScreen(new SplashScreen(), inputManager);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            //Change timer color over time
            changeColor = new Color(red, gre, blu);
            spriteBatch.End();
            //Restart Batch Draw
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, null, null, null, null, camera.ViewMatrix);
            map.Draw(spriteBatch);
            player.Draw(spriteBatch);
            dude.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Position: " + (int)dude.Position.X + ", " + (int)dude.Position.Y, new Vector2(camera.Position.X, camera.Position.Y), changeColor);
            if (dude.location != null)
            {
                spriteBatch.DrawString(font, "At Node: " + (int)dude.location.x + ", " + (int)dude.location.y, new Vector2(camera.Position.X, camera.Position.Y + 32), Color.White);
                spriteBatch.DrawString(font, "Goal: " + (int)dude.goal.x + ", " + (int)dude.goal.y, new Vector2(camera.Position.X, camera.Position.Y + 64), Color.White);
            }
            spriteBatch.DrawString(font, "# Left: " + dude.Health , new Vector2(camera.Position.X, camera.Position.Y + 96), Color.White);
            if (dude.Health != 0)
                spriteBatch.DrawString(font, "Time: " + minute + "." + time.ToString("0.00"), new Vector2(camera.Position.X, camera.Position.Y + 128), Color.White);
            else
            {
                spriteBatch.DrawString(font, "Now we Here", new Vector2(camera.Position.X, camera.Position.Y + 160), changeColor);
                spriteBatch.DrawString(font, "Countdown: " + changeTime.ToString("0.00") , new Vector2(camera.Position.X, camera.Position.Y + 192), changeColor);
            }
            //map.Draw(spriteBatch);
        }
    }
}
