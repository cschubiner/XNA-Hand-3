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

    class AAgameplayScreen : GameScreen
    {

        ContentManager Content;
        Rectangle safeAreaRectangle;
        #region sound
        double soundtimeplayed = 0;
        int backgroundsongchosen;
        public static Song[] backgroundMusics;
        #endregion
        #region networking
        NetworkSession networkSession;

        PacketWriter packetWriter = new PacketWriter();
        PacketReader packetReader = new PacketReader();
        #endregion
        #region particle effects

        private static Random random = new Random();
        public static Random Random
        {
            get { return random; }
        }

        public static ExplosionParticleSystemDefault explosion;
        public static ExplosionSmokeParticleSystemDefault smoke;
        public static SmokePlumeParticleSystemDefault smokePlume;


        #endregion

        ContentManager content;
        SpriteFont gameFont;

        Texture2D background;

        Texture2D gamescreen;
        Texture2D buyfull;

        bool isGameDone;

        bool IsTrialMode;
        float pauseAlpha;


        Player[] players;


        public AAgameplayScreen(NetworkSession networkSession)
        {
            this.networkSession = networkSession;
            TransitionOnTime = TimeSpan.FromSeconds(1.5);
            TransitionOffTime = TimeSpan.FromSeconds(0.5);

        }


        public override void LoadContent()
        {
            if (Content == null)
                Content = new ContentManager(ScreenManager.Game.Services, "Content");
            for (int i = 0; i < Gamer.SignedInGamers.Count; i++)
            {
                Gamer.SignedInGamers[i].Presence.PresenceMode = GamerPresenceMode.TryingForRecord;
            }
            safeAreaRectangle = Global_Variables.safeAreaRectangle;
            #region songs
            backgroundMusics = new Song[2];
            backgroundMusics[0] = Content.Load<Song>("Game Customization\\gamemusic (1)");
            backgroundMusics[1] = Content.Load<Song>("Game Customization\\gamemusic (2)");

            backgroundsongchosen = RandomIntWORKING(0, backgroundMusics.Length-1);
            MediaPlayer.Play(backgroundMusics[backgroundsongchosen]);


            #endregion

            #region player initializing
            BackgroundScreen.numberOfPlayersPlaying = 0;
            /*if (BackgroundScreen.player1isplaying) { BackgroundScreen.numberOfPlayersPlaying++; santas[0].alive = true; }
            if (BackgroundScreen.player2isplaying) { BackgroundScreen.numberOfPlayersPlaying++; santas[1].alive = true; }
            if (BackgroundScreen.player3isplaying) { BackgroundScreen.numberOfPlayersPlaying++; santas[2].alive = true; }
            if (BackgroundScreen.player4isplaying) { BackgroundScreen.numberOfPlayersPlaying++; santas[3].alive = true; }*/
            #endregion

            gameFont = Content.Load<SpriteFont>("Fonts\\gamefont");
            gamescreen = Content.Load<Texture2D>("Assets\\Untitled-1");


            ScreenManager.Game.ResetElapsedTime();
            base.LoadContent();
        }





        public override void Update(GameTime gameTime, bool otherScreenHasFocus,
                                                       bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
            // HighscoreComponent.Global.ClearHighscores();
            if (IsActive)
            {
                #region Background Sound Looping

                soundtimeplayed += gameTime.ElapsedGameTime.Ticks;
                if (soundtimeplayed >= backgroundMusics[backgroundsongchosen].Duration.Ticks - 9000000)
                {
                    try
                    {
                        backgroundsongchosen = RandomIntWORKING(0, backgroundMusics.Length-1);
                        MediaPlayer.Play(backgroundMusics[backgroundsongchosen]);
                        soundtimeplayed = 0;
                    }
                    catch { }
                }

                #endregion


                GamePadState[] gamePadStates = new GamePadState[4];
                gamePadStates[0] = GamePad.GetState(PlayerIndex.One);
                gamePadStates[1] = GamePad.GetState(PlayerIndex.Two);
                gamePadStates[2] = GamePad.GetState(PlayerIndex.Three);
                gamePadStates[3] = GamePad.GetState(PlayerIndex.Four);

                for (int j = 0; j < 4; j++)
                {
                    GamePadState gamePadState = gamePadStates[j];
                   
                }

            }//this ends update is active. put stuff before it.
            #region controller vibration
            /*foreach (Santa player in santas)
            {
                if (player.shouldVibrate)
                {
                    player.VibrationTimer -= gameTime.ElapsedGameTime.Milliseconds;
                    if (player.VibrationTimer < 0)
                    {
                        player.leftMotorVibration = 0;
                        player.rightMotorVibration = 0;
                        player.shouldVibrate = false;
                    }

                    GamePad.SetVibration(player.playerIndex, player.leftMotorVibration, player.rightMotorVibration);

                }
            }*/
            #endregion
        }

        private void GameOver()
        {
            MessageBoxScreen youwinMessageBox;
            youwinMessageBox = new MessageBoxScreen("          Game over!\nYou captured candy canes!\nA button = Exit\nB button = Restart", false);
            //youwinMessageBox = new MessageBoxScreen("          Game over!\nYou captured " + score.ToString() + " candy canes!\nA button = Exit\nB button = Restart", false);
            youwinMessageBox.Accepted += youwinMessageBoxAccepted;
            youwinMessageBox.Cancelled += youwinMessageBoxCancelled;
            ScreenManager.AddScreen(youwinMessageBox, Global_Variables.playerWhoBeganGame);
        }

        #region Random Crap

        private static Rectangle RectFromGameObject(GameObject gameObject)
        {
            return new Rectangle((int)gameObject.position.X, (int)gameObject.position.Y, (int)(gameObject.sprite.Width * gameObject.scale), (int)(gameObject.sprite.Height * gameObject.scale));
        }


        public override void UnloadContent()
        {
            Content.Unload();
        }

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
            smoke.AddParticles(where);
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
            smoke.AddParticles(whereToDraw);
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
            smokePlume.AddParticles(whereToDraw);
            //    // and then reset the timer.
            //    timeTillPuff = TimeBetweenSmokePlumePuffs;
            //}
        }





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
            explosion.AddParticles(where);
            smoke.AddParticles(where);

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
            explosion.AddParticles(whereToDraw);

            smoke.AddParticles(whereToDraw);

            //    // reset the timer.
            //    timeTillExplosion = TimeBetweenExplosions;
            //}
        }



        int RandomIntBROKEN(int min, int max)
        {
            Random random = new Random(DateTime.Now.Millisecond * DateTime.Now.Minute);
            return random.Next(min, max);
        }

        public static float RandomFloat(float min, float max)
        {
            return min + (float)random.NextDouble() * (max - min);
        }

        public static int RandomIntWORKING(int min, int max)
        {

            return (int)((float).5f + min + (float)random.NextDouble() * (max - min));
        }

        public static float GetAngleBetweenTwoVectors(Vector2 initialVector, Vector2 endingVector)
        {

            Vector2 distance = endingVector - initialVector;

            float angle = (float)Math.Atan2(distance.Y, distance.X);
            if (angle < 0)
                angle += MathHelper.Pi * 2;
            //  angle = MathHelper.ToDegrees(angle);

            return angle;
        }

        public static Rectangle CalculateBoundingRectangle(Rectangle rectangle,
                                                           Matrix transform)
        {
            // Get all four corners in local space
            Vector2 leftTop = new Vector2(rectangle.Left, rectangle.Top);
            Vector2 rightTop = new Vector2(rectangle.Right, rectangle.Top);
            Vector2 leftBottom = new Vector2(rectangle.Left, rectangle.Bottom);
            Vector2 rightBottom = new Vector2(rectangle.Right, rectangle.Bottom);

            // Transform all four corners into work space
            Vector2.Transform(ref leftTop, ref transform, out leftTop);
            Vector2.Transform(ref rightTop, ref transform, out rightTop);
            Vector2.Transform(ref leftBottom, ref transform, out leftBottom);
            Vector2.Transform(ref rightBottom, ref transform, out rightBottom);

            // Find the minimum and maximum extents of the rectangle in world space
            Vector2 min = Vector2.Min(Vector2.Min(leftTop, rightTop),
                                      Vector2.Min(leftBottom, rightBottom));
            Vector2 max = Vector2.Max(Vector2.Max(leftTop, rightTop),
                                      Vector2.Max(leftBottom, rightBottom));

            // Return that as a rectangle
            return new Rectangle((int)min.X, (int)min.Y,
                                 (int)(max.X - min.X), (int)(max.Y - min.Y));
        }

        int RandomNumberBROKENVERSION(int min, int max)
        {
            Random random = new Random((int)(DateTime.Now.Ticks / 10000000) * DateTime.Now.Millisecond * DateTime.Now.Minute);
            return random.Next(min, max);

        }


        private void DrawBackgrounds(SpriteBatch spriteBatch, Backgrounds background)
        {

            if (background.position2.X + background.sprite.Width < 0)
            {
                background.position2.X = background.position.X + background.sprite.Width;
            }

            if (background.position.X + background.sprite.Width < 0)
            {
                background.position.X = background.position2.X + background.sprite.Width;
            }
            //the following 2 lines could be optimized to only draw if it's on screen. oh well.
            spriteBatch.Draw(background.sprite, background.position, Color.White);
            spriteBatch.Draw(background.sprite, background.position2, Color.White);
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

        private static void SetAllVibrationToZero()
        {
            GamePad.SetVibration(PlayerIndex.One, 0f, 0f);
            GamePad.SetVibration(PlayerIndex.Two, 0f, 0f);
            GamePad.SetVibration(PlayerIndex.Three, 0f, 0f);
            GamePad.SetVibration(PlayerIndex.Four, 0f, 0f);
        }

        private void DrawBackgroundsHorizontally(SpriteBatch spriteBatch, Backgrounds background)
        {

            if (background.position2.X + background.sprite.Width < 0)
            {
                background.position2.X = background.position.X + background.sprite.Width;
            }

            if (background.position.X + background.sprite.Width < 0)
            {
                background.position.X = background.position2.X + background.sprite.Width;
            }
            //the following 2 lines could be optimized to only draw if it's on screen. oh well.
            spriteBatch.Draw(background.sprite, background.position, Color.White);
            spriteBatch.Draw(background.sprite, background.position2, Color.White);
        }



        private void DrawBackgroundsVertically(SpriteBatch spriteBatch, Backgrounds background)
        {

            //if (background.position2.Y + background.sprite.Height < 0)
            //{
            //    background.position2.Y = background.position.Y - background.sprite.Height;
            //}

            //if (background.position.X + background.sprite.Height < 0)
            //{
            //    background.position.X = background.position2.Y - background.sprite.Height;
            //}

            if (background.position2.Y > 720)
            {
                background.position2.Y = background.position.Y - background.sprite.Height;
            }

            if (background.position.Y > 720)
            {
                background.position.Y = background.position2.Y - background.sprite.Height;
            }


            //the following 2 lines could be optimized to only draw if it's on screen. oh well.
            spriteBatch.Draw(background.sprite, background.position, Color.White);
            spriteBatch.Draw(background.sprite, background.position2, Color.White);
        }



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

        #endregion

        public override void Draw(GameTime gameTime)
        {
            ScreenManager.GraphicsDevice.Clear(ClearOptions.Target,
                                               new Color(0, 0, 0, 0), 0, 0);
            Game1.spriteBatch = ScreenManager.SpriteBatch;
            SpriteBatch spriteBatch = Game1.spriteBatch;
            ScreenManager.GraphicsDevice.Clear(Color.CornflowerBlue);
            Game1.spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

            spriteBatch.Draw(gamescreen, Vector2.Zero, Color.White);

            if (networkSession != null)
            {
                string message = "Players: " + networkSession.AllGamers.Count;
                Vector2 messagePosition = new Vector2(100, 480);
                //  spriteBatch.DrawString(gameFont, message, messagePosition, Color.White);
            }

            Game1.spriteBatch.End();
            // If the game is transitioning on or off, fade it out to black.
            if (TransitionPosition > 0)
                ScreenManager.FadeBackBufferToBlack(255 - TransitionAlpha);
        }

        private static void DrawGameObject(SpriteBatch spriteBatch, GameObject go)
        {
            spriteBatch.Draw(go.sprite, go.position, Color.White);
        }
    }
}
