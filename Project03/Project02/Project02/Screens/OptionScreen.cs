using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;


namespace Project03
{
    public class OptionScreen : GameScreen
    {
        SpriteFont font;
        Vector2 locale;

        MenuManager menu;

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("MenuFont");
            menu = new MenuManager();
            menu.LoadContent(content, "Option");

            font = this.content.Load<SpriteFont>("SpriteFont1");
            locale = new Vector2(50, 50);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menu.UnloadContent();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            spriteBatch.DrawString(font, "Option1", locale, Color.Tomato);
            menu.Draw(spriteBatch);
        }

        public override void Update(GameTime gameTime)
        {
            //base.Update(gameTime);
            inputManager.Update();
            menu.Update(gameTime, inputManager);
        }

    }
}
