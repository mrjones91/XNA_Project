using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project03
{
    public class Entity
    {
        protected int health;
        protected SpriteSheetAnimation moveAnimation;
        protected Animation animation;
        protected float moveSpeed;

        protected ContentManager content;
        protected FileManager fileManager;

        protected Texture2D image;
        protected Vector2 position;

        protected List<List<string>> attributes, contents;

        public virtual Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        public virtual Texture2D Image
        {
            get { return image; }
        }


        public virtual void LoadContent(ContentManager content, InputManager inputmanager)
        {
            this.content = new ContentManager(content.ServiceProvider, "Content");
            attributes = new List<List<string>>();
            contents = new List<List<string>>();
        }

        public virtual void UnloadContent()
        {
            content.Unload();
        }

        public virtual void Update(GameTime gameTime, InputManager inputmanager, Collision col, Layers layer)
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            
        }

    }
}
