using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.Storage;

namespace StarterGame
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch1;
        ScreenManager screenManager;
        GamerServicesComponent gamerServices;
        public static SpriteBatch spriteBatch;
        public static bool togglefullscreen = false;
        public static bool clearHighscores = false;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            
            Content.RootDirectory = "Content";
            Components.Add(new MessageDisplayComponent(this));
            NetworkSession.InviteAccepted += (sender, e)
              => NetworkSessionComponent.InviteAccepted(screenManager, e);


            AAgameplayScreen.explosion = new ExplosionParticleSystemDefault(this, 1);
            Components.Add(AAgameplayScreen.explosion);

            // but the smoke from the explosion lingers a while.
            AAgameplayScreen.smoke = new ExplosionSmokeParticleSystemDefault(this, 2);
            Components.Add(AAgameplayScreen.smoke);

            // we'll see lots of these effects at once; this is ok
            // because they have a fairly small number of particles per effect.
            AAgameplayScreen.smokePlume = new SmokePlumeParticleSystemDefault(this, 9);
            Components.Add(AAgameplayScreen.smokePlume);
            

            this.graphics.PreferredBackBufferWidth = 1280;
            this.graphics.PreferredBackBufferHeight = 720;  //1408-792
           
            // this.graphics.IsFullScreen = true;
          
            
            //Window.AllowUserResizing = true;
           
            
            //  Guide.SimulateTrialMode = true;
            this.graphics.ApplyChanges();
            Global_Variables.safeAreaRectangle = graphics.GraphicsDevice.Viewport.TitleSafeArea;
            //Components.Add(text);
           
            ////clear highscores?
            //hsc.ClearHighscores();
            ////
           /* 
            gamerServices = new GamerServicesComponent(this);
            Components.Add(gamerServices);
            // Components.Add(new GamerServicesComponent(this));
            * 
            * */
            base.Initialize();

            // Create the screen manager component.
            screenManager = new ScreenManager(this);

            if (Global_Variables.simulateTrialMode)
                Guide.SimulateTrialMode = true;
            Components.Add(screenManager);
            
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            gamerServices = new GamerServicesComponent(this);
            Components.Add(gamerServices);
            // Components.Add(new GamerServicesComponent(this));

            base.Initialize();
        }
  
        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        /// 
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch1 = new SpriteBatch(GraphicsDevice);
            Game1.spriteBatch = new SpriteBatch(this.GraphicsDevice);
            

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            //if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            //    this.Exit();

            if (togglefullscreen)
            {
                togglefullscreen = false;

                if (this.graphics.IsFullScreen != true)
                    this.graphics.IsFullScreen = true;
                else this.graphics.IsFullScreen = false;

                this.graphics.ApplyChanges();

            }
            if (clearHighscores)
            {
                clearHighscores = false;
            }
      
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            base.Draw(gameTime);
        }
    }
}
