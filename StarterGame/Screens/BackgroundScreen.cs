//#region File Description
//-----------------------------------------------------------------------------
// BackgroundScreen.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------


//#region Using Statements
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Media;


namespace StarterGame
{
    /// <summary>
    /// The background screen sits behind all the other menu screens.
    /// It draws a background image that remains fixed in place regardless
    /// of whatever transitions the screens on top of it may be doing.
    /// </summary>
    class BackgroundScreen : GameScreen
    {
        //
        


        //
        ContentManager content;
        Texture2D backgroundTexture;
        public static bool player1isplaying = false;
        public static bool player2isplaying = false;
        public static bool player3isplaying = false;
        public static bool player4isplaying = false;
        public static int numberOfPlayersPlaying = 0;
        public static PlayerIndex playerwhoselectedplay;



        Song soundEffectsong;
        double soundtimeplayed = 0;


        //#region Initialization


        /// <summary>
        /// Constructor.
        /// </summary>
        public BackgroundScreen()
        {
            TransitionOnTime = TimeSpan.FromSeconds(0.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

        }


        /// <summary>
        /// Loads graphics content for this screen. The background texture is quite
        /// big, so we use our own local ContentManager to load it. This allows us
        /// to unload before going from the menus into the game itself, wheras if we
        /// used the shared ContentManager provided by the Game class, the content
        /// would remain loaded forever.
        /// </summary>
        public override void LoadContent()
        {
            if (content == null)
                content = new ContentManager(ScreenManager.Game.Services, "Content");
       
            soundEffectsong = content.Load<Song>("Game Customization\\gamemusic (1)");
            MediaPlayer.Play(soundEffectsong);

            backgroundTexture = content.Load<Texture2D>("Game Customization\\BackgroundForAll");



            
     
        }

    
        public override void UnloadContent()
        {
            content.Unload();
        }




        //#region Update and Draw


        /// <summary>
        /// Updates the background screen. Unlike most screens, this should not
        /// transition off even if it has been covered by another screen: it is
        /// supposed to be covered, after all! This overload forces the
        /// coveredByOtherScreen parameter to false in order to stop the base
        /// Update method wanting to transition off.
        /// </summary>
        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {

            try
            {

                Microsoft.Xna.Framework.Input.GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
                Microsoft.Xna.Framework.Input.GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
                Microsoft.Xna.Framework.Input.GamePad.SetVibration(PlayerIndex.Three, 0f, 0f);
                Microsoft.Xna.Framework.Input.GamePad.SetVibration(PlayerIndex.Four, 0f, 0f);

                soundtimeplayed += gameTime.ElapsedGameTime.Ticks;
                if (soundtimeplayed >= soundEffectsong.Duration.Ticks - 9000000)
                {
                    MediaPlayer.Play(soundEffectsong);
                    soundtimeplayed = 0;
                }

              

                int MinX = ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.X;
                int MinY = ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y;




            }
            catch { }
           

            base.Update(gameTime, otherScreenHasFocus, false);

        }


        /// <summary>
        /// Draws the background screen.
        /// </summary>
        public override void Draw(GameTime gameTime)
        {
            SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
            Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
            Rectangle fullscreen = new Rectangle(0, 0, viewport.Width, viewport.Height);
            byte fade = TransitionAlpha;

            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(backgroundTexture, fullscreen,
                             new Color(fade, fade, fade));

            try
            {
                if (ScreenManager.GetScreens().Length <= 2)
                {
              

                    for (int i = 0; i < Gamer.SignedInGamers.Count; i++)
                    {
                        Gamer.SignedInGamers[i].Presence.PresenceMode = GamerPresenceMode.AtMenu;
                    }

                    //menu screen updates should go right under this!
                    //and drawing code too!! wow 2 in 1!

                    //
                  

                }

      


                //
#if WINDOWS
                spriteBatch.DrawString(ScreenManager.Font, "v" + Global_Variables.version, new Vector2(1210, 690), Color.White, 0, Vector2.Zero, .6f, SpriteEffects.None, 0);
                #region Old entering code

                if (Guide.IsTrialMode == false)
                {

                    Microsoft.Xna.Framework.Input.GamePadState gamepad1 =
                        Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.One);
                    Microsoft.Xna.Framework.Input.GamePadState gamepad2 =
                        Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Two);
                    Microsoft.Xna.Framework.Input.GamePadState gamepad3 =
                        Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Three);
                    Microsoft.Xna.Framework.Input.GamePadState gamepad4 =
                        Microsoft.Xna.Framework.Input.GamePad.GetState(PlayerIndex.Four);
                    int numberplayersconnected = 0;
                    if (gamepad1.IsConnected)
                        numberplayersconnected++;
                    if (gamepad2.IsConnected)
                        numberplayersconnected++;
                    if (gamepad3.IsConnected)
                        numberplayersconnected++;
                    if (gamepad4.IsConnected)
                        numberplayersconnected++;
                    if (numberplayersconnected > 1)
                    {
                        if (gamepad1.IsConnected &&
                            player1isplaying == false)
                            spriteBatch.DrawString(ScreenManager.Font
                                ,
                    "\n\rPlayer 1: Press X To Enter!",
                     new Vector2(Convert.ToInt32(ScreenManager.GraphicsDevice.Viewport.Width * .6)
  ,
                       ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 5
  ),

                  Color.LightGreen);

                        else if (gamepad1.IsConnected && player1isplaying)
                        {

                            spriteBatch.DrawString(ScreenManager.Font
                                  ,
                      "\n\rPlayer 1: Press Y To Leave!",
                       new Vector2(Convert.ToInt32(ScreenManager.GraphicsDevice.Viewport.Width * .6)
    ,
                         ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 5
    ),

                    Color.LightGreen);
                        }
                        if (gamepad2.IsConnected &&
                            player2isplaying == false)
                            spriteBatch.DrawString(ScreenManager.Font
                                  ,
                      "\n\r\n\rPlayer 2: Press X To Enter!",
                       new Vector2(Convert.ToInt32(ScreenManager.GraphicsDevice.Viewport.Width * .6)
    ,
                         ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 5
    ),

                    Color.LightBlue);
                        else if (gamepad2.IsConnected && player2isplaying)
                        {
                            spriteBatch.DrawString(ScreenManager.Font
                                   ,
                       "\n\r\n\rPlayer 2: Press Y To Leave!",
                        new Vector2(Convert.ToInt32(ScreenManager.GraphicsDevice.Viewport.Width * .6)
     ,
                          ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 5
     ),

                     Color.LightBlue);
                        }
                        if (gamepad3.IsConnected &&
                            player3isplaying == false)
                            spriteBatch.DrawString(ScreenManager.Font
                                   ,
                       "\n\r\n\r\n\rPlayer 3: Press X To Enter!",
                        new Vector2(Convert.ToInt32(ScreenManager.GraphicsDevice.Viewport.Width * .6)
     ,
                          ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 5
     ),

                     Color.Yellow);
                        else if (gamepad3.IsConnected && player3isplaying)
                        {
                            spriteBatch.DrawString(ScreenManager.Font
                                   ,
                       "\n\r\n\r\n\rPlayer 3: Press Y To Leave!",
                        new Vector2(Convert.ToInt32(ScreenManager.GraphicsDevice.Viewport.Width * .6)
     ,
                          ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 5
     ),

                     Color.Yellow);
                        }
                        if (gamepad4.IsConnected &&
                            player4isplaying == false)
                            spriteBatch.DrawString(ScreenManager.Font
                                   ,
                       "\n\r\n\r\n\r\n\rPlayer 4: Press X To Enter!",
                        new Vector2(Convert.ToInt32(ScreenManager.GraphicsDevice.Viewport.Width * .6)
     ,
                          ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 5
     ),

                     Color.Purple);
                        else if (gamepad4.IsConnected && player4isplaying)
                        {
                            spriteBatch.DrawString(ScreenManager.Font
                                   ,
                       "\n\r\n\r\n\r\n\rPlayer 4: Press Y To Leave!",
                        new Vector2(Convert.ToInt32(ScreenManager.GraphicsDevice.Viewport.Width * .6)
     ,
                          ScreenManager.GraphicsDevice.Viewport.TitleSafeArea.Y + 5
     ),

                     Color.White);
                        }
                        if (gamepad1.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.X))
                            player1isplaying = true;
                        if (gamepad2.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.X))
                            player2isplaying = true;
                        if (gamepad3.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.X))
                            player3isplaying = true;
                        if (gamepad4.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.X))
                            player4isplaying = true;

                        if (gamepad1.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Y))
                            player1isplaying = false;
                        if (gamepad2.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Y))
                            player2isplaying = false;
                        if (gamepad3.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Y))
                            player3isplaying = false;
                        if (gamepad4.IsButtonDown(Microsoft.Xna.Framework.Input.Buttons.Y))
                            player4isplaying = false;
                    }
                }

                #endregion

#endif


            }
            catch { }


            spriteBatch.End();
        }



    }
    public enum TextAlignment
    {

        Top,

        Left,

        Middle,

        Right,

        Bottom,

        TopLeft,

        TopRight,

        BottomLeft,

        BottomRight

    }
}
