using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;

namespace Project03
{
    public class SplashScreen : GameScreen
    {
        SpriteFont font;
        List<FadeAnimation> fade;
        List<Texture2D> images;
        List<SoundEffect> sounds;
        List<SoundEffectInstance> soundsInst;

        bool[] hasPlayed;

        FileManager fileManager;
        int imageNumber;

        public override void LoadContent(ContentManager Content, InputManager inputManager)
        {
            base.LoadContent(Content, inputManager);
            if (font == null)
                font = this.content.Load<SpriteFont>("SpriteFont1");
            imageNumber = 0;
            fileManager = new FileManager();
            fade = new List<FadeAnimation>();
            images = new List<Texture2D>();
            sounds = new List<SoundEffect>();
            soundsInst = new List<SoundEffectInstance>();
            hasPlayed = new bool[2];

            fileManager.LoadContent("Load/Splash.txt", attributes, contents);

            for (int i = 0; i < attributes.Count; i++)
            {
                for (int j = 0; j < attributes[i].Count; j++)
                {
                    switch (attributes[i][j])
                    {
                        case "Image":
                            images.Add(this.content.Load<Texture2D>(contents[i][j]) );
                            fade.Add(new FadeAnimation());
                            break;
                        case "Sound":
                            sounds.Add(this.content.Load<SoundEffect>(contents[i][j]));
                            break;
                    }
                }
            }

            
            
            for (int i = 0; i < fade.Count; i++)
            {
                fade[i].LoadContent(content, images[i], "", sounds[i], new Vector2( 0, 0));
                fade[i].Scale = 1.0f; //check back here if wonky..
                
                fade[i].IsActive = true;
                hasPlayed[i] = false;
            }

            for (int i = 0; i < hasPlayed.Length; i++)
            {
                
            }
        }

        public override void UnloadContent()
        {
            base.UnloadContent();
            fileManager = null;
        }

        public override void Update(GameTime gameTime)
        {
            inputManager.Update();

            fade[imageNumber].Update(gameTime);

            if (!hasPlayed[imageNumber])
            {
                sounds[imageNumber].Play();
                hasPlayed[imageNumber] = true;
            }

            if (fade[imageNumber].Alpha == 0.0f)
            {
                imageNumber++;
                sounds[1].Play();
            }
            if (imageNumber >= fade.Count - 1 || inputManager.KeyPressed(Keys.Z))
            {
                if (fade[imageNumber].Alpha != 1.0f)
                    ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager, fade[imageNumber].Alpha);
                else
                    ScreenManager.Instance.AddScreen(new TitleScreen(), inputManager);
            }
            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            fade[imageNumber].Draw(spriteBatch);
        }
    }
}
