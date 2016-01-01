//#region File Description
//-----------------------------------------------------------------------------
// ScreenManager.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------


//#region Using Statements
using System;
using System.Diagnostics;
using System.Collections.Generic;
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
    /// The screen manager is a component which manages one or more GameScreen
    /// instances. It maintains a stack of screens, calls their Update and Draw
    /// methods at the appropriate times, and automatically routes input to the
    /// topmost active screen.
    /// </summary>
    public class ScreenManager : DrawableGameComponent
    {
        //#region Fields


        List<GameScreen> screens = new List<GameScreen>();
        List<GameScreen> screensToUpdate = new List<GameScreen>();

        InputState input = new InputState();

        SpriteBatch spriteBatch;

        //SHOULD PLAY INTRO VIDEO??
        //make the video be named intromovie.wmv
        bool shouldPlayIntroVideo = Global_Variables.showIntroMovie;
        //--------------------------------------
        bool shouldShowStartMenu = Global_Variables.showStartMenu;

        SpriteFont font;
        SpriteFont bigFont;
        Texture2D blankTexture;
        Texture2D starttexture;
        Texture2D starttexturetrial;
        bool intromoviepassed = false;
        bool isInitialized;
        const int msToWaitBeforeAllowingVideoSkip = 700;
        bool traceEnabled;
        Video video;
        VideoPlayer player;
        Texture2D videoTexture;
        int intromovieJUSTpassedTimer = 0;

        

        //#region Properties


        /// <summary>
        /// A default SpriteBatch shared by all the screens. This saves
        /// each screen having to bother creating their own local instance.
        /// </summary>
        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }


        /// <summary>
        /// A default font shared by all the screens. This saves
        /// each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont Font
        {
            get { return font; }
        }

        /// <summary>
        /// A default font shared by all the screens. This saves
        /// each screen having to bother loading their own local copy.
        /// </summary>
        public SpriteFont BigFont
        {
            get { return bigFont; }
        }


        /// <summary>
        /// If true, the manager prints out a list of all the screens
        /// each time it is updated. This can be useful for making sure
        /// everything is being added and removed at the right times.
        /// </summary>
        public bool TraceEnabled
        {
            get { return traceEnabled; }
            set { traceEnabled = value; }
        }


        

        //#region Initialization


        /// <summary>
        /// Constructs a new screen manager component.
        /// </summary>
        public ScreenManager(Game game)
            : base(game)
        {
        }


        /// <summary>
        /// Initializes the screen manager component.
        /// </summary>
        public override void Initialize()
        {
            
           
            base.Initialize();

            isInitialized = true;
        }


        /// <summary>
        /// Load your graphics content.
        /// </summary>
        protected override void LoadContent()
        {
            // Load content belonging to the screen manager.
            ContentManager content = Game.Content;

            spriteBatch = new SpriteBatch(GraphicsDevice);
            font = content.Load<SpriteFont>("Fonts\\menufont");
            bigFont = content.Load<SpriteFont>("Fonts\\bigfont");
           
            if (shouldPlayIntroVideo)
            {
                video = content.Load<Video>("Game Customization\\intromovie");
                player = new VideoPlayer();
                player.Play(video);
            }

            if (shouldShowStartMenu)
            {
                starttexture = content.Load<Texture2D>("Game Customization\\FullVersion-StartScreen");
                starttexturetrial = content.Load<Texture2D>("Game Customization\\TrialVersion-StartScreen");
            }

            blankTexture = content.Load<Texture2D>("Random\\blank");
            // Tell each of the screens to load their content.
            foreach (GameScreen screen in screens)
            {
                screen.LoadContent();
            }
        }


        /// <summary>
        /// Unload your graphics content.
        /// </summary>
        protected override void UnloadContent()
        {
            // Tell each of the screens to unload their content.
            foreach (GameScreen screen in screens)
            {
                screen.UnloadContent();
            }
        }




        
        public override void Update(GameTime gameTime)
        {
            // Read the keyboard and gamepad.
            input.Update();
           
            if (Global_Variables.shouldLoadMainMenu)
            {
                Global_Variables.shouldLoadMainMenu = false;

                LoadingScreen.Load(this, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());

            }
         
            // Make a copy of the master screen list, to avoid confusion if
            // the process of updating one screen adds or removes others.
            screensToUpdate.Clear();

            if (shouldPlayIntroVideo)
            {
                if (intromoviepassed)
                {
                    intromovieJUSTpassedTimer += gameTime.ElapsedGameTime.Milliseconds;
                }
            }
            foreach (GameScreen screen in screens)
                screensToUpdate.Add(screen);

            bool otherScreenHasFocus = !Game.IsActive;
            bool coveredByOtherScreen = false;

            // Loop as long as there are screens waiting to be updated.
            while (screensToUpdate.Count > 0)
            {
                // Pop the topmost screen off the waiting list.
                GameScreen screen = screensToUpdate[screensToUpdate.Count - 1];

                screensToUpdate.RemoveAt(screensToUpdate.Count - 1);

                // Update the screen.
                screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

                if (screen.ScreenState == ScreenState.TransitionOn ||
                    screen.ScreenState == ScreenState.Active)
                {
                    // If this is the first active screen we came across,
                    // give it a chance to handle input.
                    if (!otherScreenHasFocus)
                    {
                        screen.HandleInput(input);

                        otherScreenHasFocus = true;
                    }

                    // If this is an active non-popup, inform any subsequent
                    // screens that they are covered by it.
                    if (!screen.IsPopup)
                        coveredByOtherScreen = true;
                }
            }

            // Print debug trace?
            if (traceEnabled)
                TraceScreens();
        }


        /// <summary>
        /// Prints a list of all the screens, for debugging.
        /// </summary>
        void TraceScreens()
        {
            List<string> screenNames = new List<string>();

            foreach (GameScreen screen in screens)
                screenNames.Add(screen.GetType().Name);

            // // Trace.WriteLine(string.Join(", ", screenNames.ToArray()));
        }


        /// <summary>
        /// Tells each screen to draw itself.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {

            if (this.GetScreens().Length == 0)
            {
                Rectangle fullscreen = new Rectangle(0, 0, this.GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height);




                spriteBatch.Begin();
                if (shouldPlayIntroVideo)
                {
                    if (intromoviepassed == false)
                        if (player.State != MediaState.Stopped)
                            videoTexture = player.GetTexture();
                        else intromoviepassed = true;
                    if (videoTexture != null && intromoviepassed == false)
                    {

                        spriteBatch.Draw(videoTexture, fullscreen, Color.White);

                    }
                    for (PlayerIndex i = PlayerIndex.One; i <= PlayerIndex.Four; i++)
                    {
                        if (Microsoft.Xna.Framework.Input.GamePad.GetState(i).IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Start) | Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) | Microsoft.Xna.Framework.Input.GamePad.GetState(i).IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.A)
                            && player.PlayPosition.TotalMilliseconds > msToWaitBeforeAllowingVideoSkip)
                        {
                            intromoviepassed = true;

                            player.Stop();
                        }
                    }
                }
                if (intromoviepassed || shouldPlayIntroVideo == false)
                {
                    if (shouldShowStartMenu)
                    {
                        if (Guide.IsTrialMode)
                            spriteBatch.Draw(starttexturetrial, fullscreen, Color.White);
                        else spriteBatch.Draw(starttexture, fullscreen, Color.White);

                        for (PlayerIndex i = PlayerIndex.One; i <= PlayerIndex.Four; i++)
                        {
                            if (Microsoft.Xna.Framework.Input.GamePad.GetState(i).IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Start) | Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Enter) | Microsoft.Xna.Framework.Input.GamePad.GetState(i).IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.A))
                                if (intromovieJUSTpassedTimer > 300 || shouldPlayIntroVideo == false)
                                {
                                    if (i == PlayerIndex.One)
                                        BackgroundScreen.player1isplaying = true;
                                    if (i == PlayerIndex.Two)
                                        BackgroundScreen.player2isplaying = true;
                                    if (i == PlayerIndex.Three)
                                        BackgroundScreen.player3isplaying = true;
                                    if (i == PlayerIndex.Four)
                                        BackgroundScreen.player4isplaying = true;

                                    this.AddScreen(new BackgroundScreen(), null);
                                    this.AddScreen(new MainMenuScreen(), null);

                                    break;
                                }
                        }
                    }
                    else
                    {
                        this.AddScreen(new BackgroundScreen(), null);
                        this.AddScreen(new MainMenuScreen(), null);
                    }
                }
                spriteBatch.End();

            }

            foreach (GameScreen screen in screens)
            {
                if (screen.ScreenState == ScreenState.Hidden)
                    continue;

                screen.Draw(gameTime);
            }
        }
        

        
        

        //#region Public Methods


        /// <summary>
        /// Adds a new screen to the screen manager.
        /// </summary>
        public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
        {
            screen.ControllingPlayer = controllingPlayer;
            screen.ScreenManager = this;
            screen.IsExiting = false;

            // If we have a graphics device, tell the screen to load content.
            if (isInitialized)
            {
                screen.LoadContent();
            }

            screens.Add(screen);
}


        /// <summary>
        /// Removes a screen from the screen manager. You should normally
        /// use GameScreen.ExitScreen instead of calling this directly, so
        /// the screen can gradually transition off rather than just being
        /// instantly removed.
        /// </summary>
        public void RemoveScreen(GameScreen screen)
        {
            // If we have a graphics device, tell the screen to unload content.
            if (isInitialized)
            {
                screen.UnloadContent();
            }

            screens.Remove(screen);
            screensToUpdate.Remove(screen);
        }


        /// <summary>
        /// Expose an array holding all the screens. We return a copy rather
        /// than the real master list, because screens should only ever be added
        /// or removed using the AddScreen and RemoveScreen methods.
        /// </summary>
        public GameScreen[] GetScreens()
        {
            return screens.ToArray();
        }


        /// <summary>
        /// Helper draws a translucent black fullscreen sprite, used for fading
        /// screens in and out, and for darkening the background behind popups.
        /// </summary>
        public void FadeBackBufferToBlack(int alpha)
        {
            Viewport viewport = GraphicsDevice.Viewport;

            spriteBatch.Begin();

            spriteBatch.Draw(blankTexture,
                             new Rectangle(0, 0, viewport.Width, viewport.Height),
                             new Color(0, 0, 0, (byte)alpha));

            spriteBatch.End();
        }


        
    }
}
