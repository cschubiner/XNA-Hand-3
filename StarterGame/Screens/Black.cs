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
  
    class Black : GameScreen
    {
        //#region Fields
        ContentManager Content;


        NetworkSession networkSession;
      

        #region particle
       
        private static Random random = new Random();
        public static Random Random
        {
            get { return random; }
        }

        /*
        PARTICLE SYSTEM COMMENTED
        
        public static ExplosionParticleSystemDefault explosion;
        public static ExplosionSmokeParticleSystemDefault smoke;
        public static SmokePlumeParticleSystemDefault smokePlume;
        
        PARTICLE SYSTEM COMMENTED
        */

        #endregion




        public Black(NetworkSession networkSession)
        {
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);
     //       graphics = new GraphicsDeviceManager(this);
            this.networkSession = networkSession;
        }


        public override void LoadContent()
        {
            if (Content == null)
                Content = new ContentManager(ScreenManager.Game.Services, "Content");
            for (int i = 0; i < Gamer.SignedInGamers.Count; i++)
            {
                if (BackgroundScreen.numberOfPlayersPlaying==1)
                Gamer.SignedInGamers[i].Presence.PresenceMode = GamerPresenceMode.SinglePlayer;
                else
                    Gamer.SignedInGamers[i].Presence.PresenceMode = GamerPresenceMode.PlayingWithFriends;
            }

            base.LoadContent();
        }
      
        

        // Set the coordinates to draw the sprite at.
      //  Vector2 spritePosition = Vector2.Zero;
     

        /// <summary>
        /// Unload graphics content used by the game.
        /// </summary>
        public override void UnloadContent()
        { 
         
        }


        
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
           // HighscoreComponent.Global.ClearHighscores();
            if (IsActive)
            {


               
                    if (Gamer.SignedInGamers[PlayerIndex.One] != null&&GamePad.GetState(PlayerIndex.One).IsConnected)
                    {
                        BackgroundScreen.player1isplaying = true;
                    }

                    if (Gamer.SignedInGamers[PlayerIndex.Two] != null && GamePad.GetState(PlayerIndex.Two).IsConnected)
                    {
                        BackgroundScreen.player2isplaying = true;
                    }
                    if (Gamer.SignedInGamers[PlayerIndex.Three] != null && GamePad.GetState(PlayerIndex.Three).IsConnected)
                    {
                        BackgroundScreen.player3isplaying = true;
                    }
                    if (Gamer.SignedInGamers[PlayerIndex.Four] != null && GamePad.GetState(PlayerIndex.Four).IsConnected)
                    {
                        BackgroundScreen.player4isplaying = true;
                    }

                    LoadingScreen.Load(ScreenManager, true, BackgroundScreen.playerwhoselectedplay,
                                      new AAgameplayScreen(null));


           }



            }//this ends update is active. put stuff before it.
        

        private void CreateSmokeEVERYWHERE() //(dt)
        {
            //timeTillPuff -= dt;
            //if (timeTillPuff < 0)
            //{
            Vector2 where = Vector2.Zero;
            // create the explosion at some random point on the screen.
            where.X = RandomFloat(0, ScreenManager.GraphicsDevice.Viewport.Width);
            where.Y = RandomFloat(0, ScreenManager.GraphicsDevice.Viewport.Height);

            // the overall explosion effect is actually comprised of two particle
            // systems: the fiery bit, and the smoke behind it. add particles to
            // both of those systems.
            //smoke.AddParticles(where); PARTICLE SYSTEM COMMENTED
            //    // and then reset the timer.
            //    timeTillPuff = TimeBetweenSmokePlumePuffs;
            //}
        }

        private void CreateSmoke(Vector2 whereToDraw)
        {
            //timeTillPuff -= dt;
            //if (timeTillPuff < 0)
   
            // the overall explosion effect is actually comprised of two particle
            // systems: the fiery bit, and the smoke behind it. add particles to
            // both of those systems.
            //smoke.AddParticles(whereToDraw); PARTICLE SYSTEM COMMENTED
            //    // and then reset the timer.
            //    timeTillPuff = TimeBetweenSmokePlumePuffs;
            //}
        }

        private void CreateSmokePlume(Vector2 whereToDraw)
        {
            //timeTillPuff -= dt;
            //if (timeTillPuff < 0)

            // the overall explosion effect is actually comprised of two particle
            // systems: the fiery bit, and the smoke behind it. add particles to
            // both of those systems.
            //smokePlume.AddParticles(whereToDraw); PARTICLE SYSTEM COMMENTED
            //    // and then reset the timer.
            //    timeTillPuff = TimeBetweenSmokePlumePuffs;
            //}
        }

        #region Helper Functions

        //  a handy little function that gives a random float between two
        // values. This will be used in several places in the sample, in particilar in
        // ParticleSystem.InitializeParticle.
        public static float RandomFloat(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        #endregion

        private void CreateFireExplosionWithSmokeEVERYWHERE() //(dt)
        {
            //timeTillExplosion -= dt;
            //if (timeTillExplosion < 0)
            //{
            Vector2 where = Vector2.Zero;
            // create the explosion at some random point on the screen.
            where.X = RandomFloat(0, ScreenManager.GraphicsDevice.Viewport.Width);
            where.Y = RandomFloat(0, ScreenManager.GraphicsDevice.Viewport.Height);

            // the overall explosion effect is actually comprised of two particle
            // systems: the fiery bit, and the smoke behind it. add particles to
            // both of those systems.
            //explosion.AddParticles(where); PARTICLE SYSTEM COMMENTED
            //smoke.AddParticles(where); PARTICLE SYSTEM COMMENTED

            //    // reset the timer.
            //    timeTillExplosion = TimeBetweenExplosions;
            //}
        }

        private void CreateFireExplosionWithSmoke(Vector2 whereToDraw)
        {
            //timeTillExplosion -= dt;
            //if (timeTillExplosion < 0)
            //{
    
            // the overall explosion effect is actually comprised of two particle
            // systems: the fiery bit, and the smoke behind it. add particles to
            // both of those systems.
            //explosion.AddParticles(whereToDraw); PARTICLE SYSTEM COMMENTED

            //smoke.AddParticles(whereToDraw); PARTICLE SYSTEM COMMENTED
            
            //    // reset the timer.
            //    timeTillExplosion = TimeBetweenExplosions;
            //}
        }



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
                               new AAgameplayScreen(null));
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
                ScreenManager.AddScreen(new PauseMenuScreen(networkSession), ControllingPlayer);
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



              Game1.spriteBatch.End();



            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }


        
    }
}
