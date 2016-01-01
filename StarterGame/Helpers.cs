using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Net;
using System.Text;

namespace StarterGame
{
    class Helpers
    {

        //inclusive
        public static int RandomNumber(int min, int max)
        {
            Random random = new Random(DateTime.Now.Millisecond * DateTime.Now.Minute);
            return random.Next(min, max + 1);
        }

        public static void Log(string a_string)
        {
#if WINDOWS
            System.Diagnostics.Debug.WriteLine(a_string);
#endif
        }

        public static void Log(int a_int)
        {
            Log(Convert.ToString(a_int));
        }

        public static Vector2 GetCenteredPositionForText(SpriteFont textFont, string textString)
        {
            Vector2 size = textFont.MeasureString(textString);
            return new Vector2(1280 / 2 - size.X / 2, 720 / 2 - size.Y / 2);
        }

        public static void DrawStrokedString(SpriteFont textFont, string textString, Vector2 textPosition, Color textColor, SpriteBatch spriteBatch)
        {
            DrawStrokedString(textString, textPosition, textColor, Color.Black, 2, textFont, spriteBatch);
        }

        public static void DrawStrokedString(string textString, Vector2 textPosition, Color textColor, Color strokeColor, float strokeWidth, SpriteFont textFont, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(textFont, textString, new Vector2(textPosition.X + -1 * strokeWidth, textPosition.Y + -1 * strokeWidth), strokeColor);
            spriteBatch.DrawString(textFont, textString, new Vector2(textPosition.X + 0 * strokeWidth, textPosition.Y + -1 * strokeWidth), strokeColor);
            spriteBatch.DrawString(textFont, textString, new Vector2(textPosition.X + 1 * strokeWidth, textPosition.Y + -1 * strokeWidth), strokeColor);
            spriteBatch.DrawString(textFont, textString, new Vector2(textPosition.X + 1 * strokeWidth, textPosition.Y + 0 * strokeWidth), strokeColor);
            spriteBatch.DrawString(textFont, textString, new Vector2(textPosition.X + 1 * strokeWidth, textPosition.Y + 1 * strokeWidth), strokeColor);
            spriteBatch.DrawString(textFont, textString, new Vector2(textPosition.X + 0 * strokeWidth, textPosition.Y + 1 * strokeWidth), strokeColor);
            spriteBatch.DrawString(textFont, textString, new Vector2(textPosition.X + -1 * strokeWidth, textPosition.Y + 1 * strokeWidth), strokeColor);
            spriteBatch.DrawString(textFont, textString, new Vector2(textPosition.X + -1 * strokeWidth, textPosition.Y + 0 * strokeWidth), strokeColor);
            spriteBatch.DrawString(textFont, textString, textPosition, textColor);
        }

        public static void DrawStrokedString(SpriteFont font, string text, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects spriteEffects, float layerDepth, SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, new Vector2(position.X + -1 * 2, position.Y + -1 * 2), Color.Black, rotation, origin, scale, SpriteEffects.None, layerDepth);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 0 * 2, position.Y + -1 * 2), Color.Black, rotation, origin, scale, SpriteEffects.None, layerDepth);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 1 * 2, position.Y + -1 * 2), Color.Black, rotation, origin, scale, SpriteEffects.None, layerDepth);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 1 * 2, position.Y + 0 * 2), Color.Black, rotation, origin, scale, SpriteEffects.None, layerDepth);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 1 * 2, position.Y + 1 * 2), Color.Black, rotation, origin, scale, SpriteEffects.None, layerDepth);
            spriteBatch.DrawString(font, text, new Vector2(position.X + 0 * 2, position.Y + 1 * 2), Color.Black, rotation, origin, scale, SpriteEffects.None, layerDepth);
            spriteBatch.DrawString(font, text, new Vector2(position.X + -1 * 2, position.Y + 1 * 2), Color.Black, rotation, origin, scale, SpriteEffects.None, layerDepth);
            spriteBatch.DrawString(font, text, new Vector2(position.X + -1 * 2, position.Y + 0 * 2), Color.Black, rotation, origin, scale, SpriteEffects.None, layerDepth);

            spriteBatch.DrawString(font, text, position, color, rotation, origin, scale, SpriteEffects.None, layerDepth);
        }

        

        public static string WrapText(SpriteFont spriteFont, string text, float maxLineWidth)
        {
            string[] words = text.Split(' ');

            StringBuilder sb = new StringBuilder();

            float lineWidth = 0f;

            float spaceWidth = spriteFont.MeasureString(" ").X;

            foreach (string word in words)
            {
                Vector2 size = spriteFont.MeasureString(word);

                if (lineWidth + size.X < maxLineWidth)
                {
                    sb.Append(word + " ");
                    lineWidth += size.X + spaceWidth;
                }
                else
                {
                    sb.Append("\n" + word + " ");
                    lineWidth = size.X + spaceWidth;
                }
            }

            return sb.ToString();
        }
    }
}
