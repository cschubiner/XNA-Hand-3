using System;
using System.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace StarterGame
{
    abstract class GameObject
    {
        public Texture2D sprite;
        public Vector2 position;
        public Vector2 velocity;
        public Vector2 acceleration;
        public float scale = 1.0f;

        public GameObject(Texture2D sprite, Vector2 position, Vector2 velocity, Vector2 acceleration)
        {
            this.sprite = sprite;
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
        }


        public GameObject( Vector2 position, Vector2 velocity, Vector2 acceleration)
        {
     
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
        }

        public void Update()
        {
            velocity += acceleration;
            position += velocity;
        }
    }
}