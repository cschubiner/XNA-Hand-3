using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;

namespace StarterGame
{
    class Player
    {
        public Texture2D sprite;
        public Vector2 position;
        public float rotation;
        public Vector2 center;
        public Vector2 velocity;
        public bool alive;
        public PlayerIndex playerIndex;
        public int number;
        public Vector2 acceleration;
        public static float jumpvelocity= 9.5f;
        public static float jumpvelocityInitial = 9.5f;
        public float leftrightAccelerationSpeed = .25f;
        public float windResistance = .05f*1.5f*1.1f;
        public  float maxVelocityX = 15*1.3f;
        public int VibrationTimer = 0;
        public bool shouldVibrate = false;
        public float leftMotorVibration = 0;
        public float rightMotorVibration = 0;





        public Player(Texture2D loadedTexture)
        {

            rotation = 0.0f;
            position = Vector2.Zero;
            sprite = loadedTexture;
            center = new Vector2(sprite.Width / 2, sprite.Height / 2);
            velocity = Vector2.Zero;
            alive = false;
            acceleration = new Vector2(0,
              0.5f*.5f);
        }
    }
}
