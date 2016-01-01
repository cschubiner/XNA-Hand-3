//#region File Description
//-----------------------------------------------------------------------------
// VibrationScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------


//#region Using Statements
using System;
using System.Threading;
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
    /// This screen implements the actual game logic. It is just a
    /// placeholder to get the idea across: you'll probably want to
    /// put some more interesting gameplay in here!
    /// </summary>
    class VibrationScreen : GameScreen
    {
        //#region Fields
        
        ContentManager Content;
        
        SpriteFont gameFont;
        SpriteFont timerfont;
        GamePadState oldstatep1;
        SoundEffect soundEffectswoosh;
        double soundtimeplayed = 0;
        Random randnum = new Random();
        bool changevibrations = true;
        Color backColor = Color.Black;
        double timer2sec = 10000000*OptionsMenuScreen.timerduration;
        int vib1;
        int vib2 = 0;
        int vib3 = 0;
        int vib4 = 0;
        int vib1r = 0;
        int vib2r = 0;
        int vib3r = 0;
        int vib4r = 0;
 
        SpriteFont font;
        

        //#region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public VibrationScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
     //       graphics = new GraphicsDeviceManager(this);
       
        }


        /// <summary>
        /// Load graphics content for the game.
        /// </summary>
        public override void LoadContent()
        {
            if (Content == null)
                Content = new ContentManager(ScreenManager.Game.Services, "Content");
            for (int i = 0; i < Gamer.SignedInGamers.Count; i++)
            {
                Gamer.SignedInGamers[i].Presence.PresenceMode = GamerPresenceMode.TryingForRecord;
            }
         //  backgroundTexture = Content.Load<Texture2D>("backgroundbluemixup");
               
          //  backgroundTexture = Content.Load<Texture2D>("backgroundblack");
            gameFont = Content.Load<SpriteFont>("gamefont2");
        //    soundEffectsong = Content.Load<SoundEffect>("Tabla_Grunge_1");
            soundEffectswoosh = Content.Load<SoundEffect>("SWOOSH");
            oldstatep1 = GamePad.GetState(PlayerIndex.One);
            // A real game would probably have more content than this sample, so
            // it would take longer to load. We simulate that by delaying for a
            Thread.Sleep(1300);

            // once the load has finished, we use ResetElapsedTime to tell the game's
            // timing mechanism that we have just finished a very long frame, and that
            // it should not try to catch up.
            ScreenManager.Game.ResetElapsedTime();
          //  soundEffectsong.Play();
       

            font = Content.Load<SpriteFont>("Fonts\\GameFont");
            timerfont = Content.Load<SpriteFont>("Fonts\\timer2sec");
        }
   

        // Set the coordinates to draw the sprite at.
      //  Vector2 spritePosition = Vector2.Zero;
     

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        {
            Content.Unload();
        }


        

        //#region Update and Draw


        /// <summary>
        /// Updates the state of the game. This method checks the GameScreen.IsActive
        /// property, so the game will stop updating when the pause menu is active,
        /// or if you tab away to a different application.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            soundtimeplayed += gameTime.ElapsedGameTime.Ticks;
            //if (soundtimeplayed >= soundEffectsong.Duration.Ticks - 9000000)
            //{
            //    try
            //    {
            //        soundEffectsong.Play();
            //        soundtimeplayed = 0;
            //    }catch{}
            //    }
            if (IsActive)
            {



                for (PlayerIndex player = PlayerIndex.One; player <= PlayerIndex.Four; player++)
                {
                    GamePadState playerstate = GamePad.GetState(player);
                    if (changevibrations == true)
                    {
                        float coefficient = 1;
                        if (Guide.IsTrialMode)
                            coefficient = (float).80;
                        GamePad.SetVibration(player, playerstate.Triggers.Left * coefficient,coefficient*playerstate.Triggers.Right);
                        if (player == PlayerIndex.One)
                        {
                            vib1 = Convert.ToInt32((playerstate.Triggers.Left *100));
                            vib1r = Convert.ToInt32((playerstate.Triggers.Right *100));
                        }
                            if (player == PlayerIndex.Two){
                                vib2 = Convert.ToInt32((playerstate.Triggers.Left *100));
                                vib2r = Convert.ToInt32((playerstate.Triggers.Right *100));
                        }
                        if (player == PlayerIndex.Three){
                            vib3 = Convert.ToInt32((playerstate.Triggers.Left *100));
                            vib3r = Convert.ToInt32((playerstate.Triggers.Right *100));
                        }
                        if (player == PlayerIndex.Four){
                            vib4 = Convert.ToInt32((playerstate.Triggers.Left *100));
                            vib4r = Convert.ToInt32((playerstate.Triggers.Right *100));
                        }
                    }
                  
                      if (playerstate.IsButtonDown(Buttons.X))
                    {
                        changevibrations = false;
                    }
                    if (playerstate.IsButtonDown(Buttons.Y))
                    {
                        changevibrations = true;
                    }
                }





            }
        }

       
        void youwinMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }
        void youwinMessageBoxCancelled(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new VibrationScreen());
        }


        /// <summary>
        /// Lets the game respond to player input. Unlike the Update method,
        /// this will only be called when the gameplay screen is active.
        /// </summary>
        public override void HandleInput(InputState input)
        {
            if (input == null)
                throw new ArgumentNullException("input");

            // Look up inputs for the active player profile.
            int playerIndex = (int)ControllingPlayer.Value;

            KeyboardState keyboardState = input.CurrentKeyboardStates[playerIndex];
            GamePadState gamePadState = input.CurrentGamePadStates[playerIndex];

            // The game pauses either if the user presses the pause button, or if
            // they unplug the active gamepad. This requires us to keep track of
            // whether a gamepad was ever plugged in, because we don't want to pause
            // on PC if they are playing with a keyboard and have no gamepad at all!
            bool gamePadDisconnected = !gamePadState.IsConnected &&
                                       input.GamePadWasConnected[playerIndex];

            if (input.IsPauseGame(ControllingPlayer) || gamePadDisconnected)
            {
                GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
                GamePad.SetVibration(PlayerIndex.Three, 0f, 0f);
                GamePad.SetVibration(PlayerIndex.Four, 0f, 0f);
                ScreenManager.AddScreen(new PauseMenuScreen(null), ControllingPlayer);
            }
            
        }


    
        public override void Draw(GameTime gameTime)
        {
          
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               Color.Black, 0, 0);
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
           

            
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

          
                Rectangle fullscreen = new Rectangle(0, 0, ScreenManager.GraphicsDevice.Viewport.Width, ScreenManager.GraphicsDevice.Viewport.Height);
              //  spriteBatch.Draw(backgroundTexture, fullscreen, Color.White);
                string addonstring = "";
                if (changevibrations)
                    addonstring = "Press X to lock vibration settings";
                else addonstring = "Press Y to unlock vibration settings";
                if (Guide.IsTrialMode)
                {
                    addonstring += "Vibration strength is severely limited in the trial version";
                    if (vib1 > 55)
                        vib1 = 55;
                    if (vib2 > 55)
                        vib2 = 55;
                    if (vib3 > 55)
                        vib3 = 55;
                    if (vib4 > 55)
                        vib4 = 55;
                    if (vib1r > 55)
                        vib1r = 55;
                    if (vib2r > 55)
                        vib2r = 55;
                    if (vib3r > 55)
                        vib3r = 55;
                    if (vib4r > 55)
                        vib4r = 55;
                }
                spriteBatch.DrawString(gameFont, addonstring + "\n\rHold R to affect the right motor\n\rHold L to affect the left motor\n\r\n\r"
                    + "Player 1: " + vib1 + "%   " + vib1r + "%\n\r"
                    + "Player 2: " + vib2 + "%   " + vib2r + "%\n\r"
                    + "Player 3: " + vib3 + "%   " + vib3r + "%\n\r"
                    + "Player 4: " + vib4 + "%   " + vib4r + "%\n\r"
            , new Vector2(Convert.ToInt32(ScreenManager.GraphicsDevice.Viewport.Width*.3),Convert.ToInt32(ScreenManager.GraphicsDevice.Viewport.Height*.2)), Color.White);
            spriteBatch.End();



            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }


        
    }
}
