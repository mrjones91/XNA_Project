﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project03
{
    public class TitleScreen : GameScreen
    {
        SpriteFont font;
        //FadeAnimation fade;
        //Texture2D image;
        MenuManager menu;

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("MenuFont");
            menu = new MenuManager();
            menu.LoadContent(content, "Title");
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            menu.UnloadContent();
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();
            menu.Update(gameTime, inputManager);
            //base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            menu.Draw(spriteBatch);
        }
    }
}
