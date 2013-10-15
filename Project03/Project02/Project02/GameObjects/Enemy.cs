using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project03
{
    public class Enemy: Entity
    {
        public override void LoadContent(ContentManager content, InputManager inputmanager)
        {
            base.LoadContent(content, inputmanager);
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
        }

        public override void Update(GameTime gameTime, InputManager inputManager, Collision col, Layers layer)
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}
