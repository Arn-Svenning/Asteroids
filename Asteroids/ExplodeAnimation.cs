using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asteroids
{
    internal class ExplodeAnimation
    {
        Texture2D animation;
        Rectangle sourceRectangle;
        Vector2 position;

        float elapsedTime;
        float frameTime;
        int numberOfFrames;
        int currentFrame;
        int width;
        int height;
        int frameWidth;
        int frameHeight;
        bool looping;

        public ExplodeAnimation(ContentManager Content, string asset, float frameSpeed, int numberOfFrames, bool looping)
        {
            this.frameTime = frameSpeed;
            this.numberOfFrames = numberOfFrames;
            this.looping = looping;
            this.animation = Content.Load<Texture2D>(asset);
            frameWidth = (animation.Width / numberOfFrames);
            frameHeight = (animation.Height);
            position = new Vector2(frameWidth, frameHeight);
        }

        public void playAnimation(GameTime gameTime)
        {
            elapsedTime += (float)gameTime.ElapsedGameTime.TotalMilliseconds;
            sourceRectangle = new Rectangle(currentFrame * frameWidth, 0, frameWidth, frameHeight);

            if (elapsedTime >= frameTime)
            {
                if (currentFrame >= numberOfFrames - 1)
                {
                    currentFrame = 0;

                }
                else
                {
                    currentFrame++;
                }
                elapsedTime = 0;
            }
        }

        public void draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(animation, position, sourceRectangle, Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
        }
    }
}
