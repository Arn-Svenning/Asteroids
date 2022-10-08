using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Xml;

namespace Asteroids
{
    internal class Meteors
    {
       public Vector2 position;
       public Vector2 velocity;
       public Texture2D meteor;

       public Rectangle meteorSize;

        public bool alive;
        

        public Meteors (Vector2 position, Vector2 velocity, Texture2D meteor)
        {
            this.position = position;
            this.velocity = velocity;
            this.meteor = meteor;

            alive = true;
        }       

        public void Update()
        {
            position = position + velocity;
            meteorSize = new Rectangle((int)position.X, (int)position.Y, meteor.Height, meteor.Width);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(meteor, position, Color.White);
            
        }

        public bool IsDestroyed(int x, int y)
        {
            bool isDestroyed = false;
            Rectangle rect = new Rectangle((int)position.X, (int)position.Y, meteor.Width, meteor.Height);

            if(rect.Contains(x, y))
            {
                isDestroyed = true;
                alive = false;
            }
            return isDestroyed;
                       

        }
    }
}
