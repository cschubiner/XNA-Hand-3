using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace StarterGame
{
    /// <summary>
    /// Each player controls a tank, which they can drive around the screen.
    /// This class implements the logic for moving and drawing the tank, and
    /// responds to input that is passed in from outside. The OnlinePlayer class does
    /// not implement any networking functionality, however: that is all
    /// handled by the main game class.
    /// </summary>
    class OnlinePlayer
    {
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 center;
        public Vector2 velocity;
        public bool alive;
        public int number;
        public Vector2 acceleration;
        public static float jumpvelocity = 9.5f;
        public static float jumpvelocityInitial = 9.5f;
        public float leftrightAccelerationSpeed = .25f;
        public float windResistance = .05f * 1.5f * 1.1f;
        public float maxVelocityX = 15 * 1.3f;
        public int VibrationTimer = 0;
        public bool shouldVibrate = false;
        public float leftMotorVibration = 0;
        public float rightMotorVibration = 0;
        public Vector2[] logpositions;
        public bool justCollidedWithLog=false;
        public bool isTheWinner = false;

        /// <summary>
        /// Constructs a new OnlinePlayer instance.
        /// </summary>
        public OnlinePlayer(int gamerIndex, ContentManager content,
                    int screenWidth, int screenHeight)
        {
            number = gamerIndex;
            if (gamerIndex == 0)
            {
                sprite = content.Load<Texture2D>("Sprites\\player1");
            }
            if (gamerIndex == 1)
            {
                sprite = content.Load<Texture2D>("Sprites\\player2");
            }
            if (gamerIndex ==2)
            {
                sprite = content.Load<Texture2D>("Sprites\\player3");
            }
            if (gamerIndex == 3)
            {
                sprite = content.Load<Texture2D>("Sprites\\player4");
            }
            if (gamerIndex > 3)
            {
                sprite = content.Load<Texture2D>("Sprites\\player"+((int)(AAgameplayScreen.RandomFloat(1,4))).ToString());

            }

            alive = true;
            velocity = new Vector2(0, AAgameplayScreen.RandomFloat(-8,-12));

            position = new Vector2(1280 / 2 - sprite.Width, 720);
            center = new Vector2(sprite.Width / 2, sprite.Height / 2);

            logpositions = new Vector2[18];
            acceleration = new Vector2(0,
              0.5f * .5f);

        }


        /// <summary>
        /// Moves the tank in response to the current input settings.
        /// </summary>
        public void Update()
        {
            // Gradually turn the tank and turret to face the requested direction.
           
        }


        /// <summary>
        /// Gradually rotates the tank to face the specified direction.
        /// </summary>
    
        /// <summary>
        /// Draws the tank and turret.
        /// </summary>
        public void Draw(SpriteBatch spriteBatch)
        {
          
        }
    }
}