﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Project03
{
    public class ScreenManager
    {
        #region Variables
        /// CodingMadeEasy Tutorials!!
        ContentManager content;

        GameScreen currentScreen;
        GameScreen newScreen;
        /// <summary>
        /// Screenmanager Instance.. supercedes
        /// the dictionary by setting this class
        /// to have a single instance 
        /// </summary>
        private static ScreenManager instance;
        // Track order of screens opened
        Stack<GameScreen> screenStack = new Stack<GameScreen>();

        //Screen width and height
        Vector2 dimensions;

        bool transition;

        FadeAnimation fade;

        Texture2D fadeTexture;
        Texture2D nullImage;
        InputManager inputManager;

        #endregion

        #region Properties

        public Texture2D NullImage
        {
            get { return nullImage; }
        }
        public static ScreenManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new ScreenManager();
                return instance;
            }
        }

        public Vector2 Dimensions
        {
            get { return dimensions; }
            set { dimensions = value; }
        }
        #endregion

        #region Main Methods

        public void AddScreen(GameScreen screen, InputManager inputManager)
        {
            transition = true;
            newScreen = screen;
            fade.IsActive = true;
            fade.Alpha = 0.0f;
            fade.ActivateValue = 1.0f;
            this.inputManager = inputManager;

        }

        //Overload for AddScreen to make manual Transitiions prettier
        public void AddScreen(GameScreen screen, InputManager inputManager, float alpha)
        {
            transition = true;
            newScreen = screen;
            fade.IsActive = true;
            fade.ActivateValue = 1.0f;
            if (alpha != 1.0f)
                fade.Alpha = 1.0f - alpha;
            else
                fade.Alpha = alpha;
            fade.Increase = true;
            this.inputManager = inputManager;
        }
        public void Initialize()
        {
            currentScreen = new SplashScreen();
            fade = new FadeAnimation();
            inputManager = new InputManager();
        }
        public void LoadContent(ContentManager Content) 
        {
            content = new ContentManager(Content.ServiceProvider, "Content");
            currentScreen.LoadContent(Content, inputManager);
            nullImage = this.content.Load<Texture2D>("SplashScreen/blank");
            fadeTexture = this.content.Load<Texture2D>("fade");
            fade.LoadContent(content, fadeTexture, "", null, Vector2.Zero);
            fade.Scale = dimensions.X;
        }

        public void Update(GameTime gameTime) 
        {
            if (!transition)
                currentScreen.Update(gameTime);
            else
                Transition(gameTime);
        }
        public void Draw(SpriteBatch spriteBatch) 
        {
            currentScreen.Draw(spriteBatch);
            if (transition)
                fade.Draw(spriteBatch);
        }

        #endregion

        #region Private Methods

        private void Transition(GameTime gameTime)
        {
            fade.Update(gameTime);
            if(fade.Alpha == 1.0f && fade.Timer.TotalSeconds == 1.0f)
            {
                screenStack.Push(newScreen);
                currentScreen.UnloadContent();
                currentScreen = newScreen;
                currentScreen.LoadContent(content, this.inputManager);
            }
            else if (fade.Alpha == 0.0f)
            {
                transition = false;
                fade.IsActive = false;
            }
        }

        #endregion
    }
}
