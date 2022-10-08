using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    internal class Ships
    {
        public Vector2 position;
        public Vector2 velocity;
        public Texture2D shipTexture;

        
        public float scale;
        
        public Rectangle shipSize;

        public Random random;
        public Ships(Vector2 position, Vector2 velocity, Texture2D shipTexture, float scale)
        {
            this.position = position;
            this.velocity = velocity;
            this.shipTexture = shipTexture;
            this.scale = scale;
        }
        public void Update()
        {
            scale = random.Next(1, 3);

            position = position + velocity;

            shipSize = new Rectangle((int)position.X, (int)position.Y, shipTexture.Height, shipTexture.Width);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(shipTexture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, 0);
        }
    }
}
