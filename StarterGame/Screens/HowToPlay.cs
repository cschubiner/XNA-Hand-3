//#region File Description
//-----------------------------------------------------------------------------
// GamePlayCHANGENAMEScreen.cs
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
  
    class HowToPlay : GameScreen
    {
        //#region Fields
        ContentManager Content;

        Texture2D[] images;

        GamePadState gamepadstate;
        GamePadState lastgamepadstate;
        int imageToDisplay = 0;
        KeyboardState keyboardlast;
        KeyboardState keyboard;





        public HowToPlay()
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
     //       graphics = new GraphicsDeviceManager(this);
       
        }


        public override void LoadContent()
        {
            if (Content == null)
                Content = new ContentManager(ScreenManager.Game.Services, "Content");
            for (int i = 0; i < Gamer.SignedInGamers.Count; i++)
            {
                Gamer.SignedInGamers[i].Presence.PresenceMode = GamerPresenceMode.AtMenu;
            }
            images = new Texture2D[4];

            images[0] = Content.Load<Texture2D>("HowTo\\HowTo (1)");
            images[1] = Content.Load<Texture2D>("HowTo\\HowTo (2)");
            images[2] = Content.Load<Texture2D>("HowTo\\HowTo (3)");
            images[3] = Content.Load<Texture2D>("HowTo\\HowTo (4)");

            gamepadstate = GamePad.GetState(BackgroundScreen.playerwhoselectedplay);
            lastgamepadstate = GamePad.GetState(BackgroundScreen.playerwhoselectedplay);
            keyboardlast = Keyboard.GetState();
            base.LoadContent();
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


        
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
           // HighscoreComponent.Global.ClearHighscores();
            if (IsActive)
            {
                gamepadstate = GamePad.GetState(BackgroundScreen.playerwhoselectedplay);
                keyboard = Keyboard.GetState();
                if ((gamepadstate.IsButtonDown(Buttons.A) && lastgamepadstate.IsButtonUp(Buttons.A)) || (keyboard.IsKeyDown(Keys.Enter) && keyboardlast.IsKeyUp(Keys.Enter)))
                {
                    if (imageToDisplay < images.Length-1)
                        imageToDisplay++;
                    else
                    {
                        LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
                    }


                }

                if (gamepadstate.IsButtonDown(Buttons.B) && lastgamepadstate.IsButtonUp(Buttons.B))
                {
                    if (imageToDisplay == 0)
                    {
                        LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                         new MainMenuScreen());
                    }
                    else
                    {
                        imageToDisplay--;
                    }
                }

                keyboardlast = Keyboard.GetState();
                lastgamepadstate = GamePad.GetState(BackgroundScreen.playerwhoselectedplay);
           }



            }//this ends update is active. put stuff before it.
        

 



                #region Random Crap

                int RandomNumber(int min, int max)
        {
            Random random = new Random(DateTime.Now.Millisecond*DateTime.Now.Minute);
            return random.Next(min, max);
        }
       
        void youwinMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, false, null, new BackgroundScreen(),
                                                           new MainMenuScreen());
        }
        void youwinMessageBoxCancelled(object sender, PlayerIndexEventArgs e)
        {
            LoadingScreen.Load(ScreenManager, true, e.PlayerIndex,
                               new HowToPlay());
        }

                #endregion



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
                                               new Color(0,0,0,0), 0, 0);
            Game1.spriteBatch = ScreenManager.SpriteBatch;
            
              ScreenManager.GraphicsDevice.Clear(Color.Black);

              Game1.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

              //Draw the backgroundTexture sized to the width
              //and height of the screen.
              try
              {
                  Game1.spriteBatch.Draw(images[imageToDisplay], Vector2.Zero, Color.White);
              }
              catch { }
              Game1.spriteBatch.End();



            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }


        
    }
}
