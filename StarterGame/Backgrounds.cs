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
    class Backgrounds
    {
        public Vector2 position;
        public Texture2D sprite;
        public Vector2 position2;
        public Backgrounds(Texture2D loadedTexture)
        {
            sprite = loadedTexture;
        }
    }
}
