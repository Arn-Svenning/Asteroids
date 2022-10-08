using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace Asteroids
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        Animation animation1;
        
        Texture2D gameOver;
        Texture2D welcome;
       
        Texture2D meteor;
        Meteors meteorClass;

        Texture2D spaceShip;
        
        MouseState mouseState, previousMouseState;

        bool isDestroyed;
        bool isPlaying;
        bool pressed;
        bool released;

        Random random;

        List<Meteors> meteorsList;
        List<ExplodeAnimation> explosionList;

        Ships spaceShipClass;
        Ships[] spaceShipArray;

        int score;
        int lives = 20;

        float shipScale;

        SpriteFont font;

        string scoreString;
        string livesString;

        Texture2D crossHair;
        Rectangle crossHairRect;

        SoundEffect explodeEffect;
        Song backgroundSong;
       
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;                        
        }

        protected override void Initialize()
        {
                       
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            
            meteorsList = new List<Meteors>();
            
            spaceShip = Content.Load<Texture2D>("SpaceShip");
            spaceShipArray = new Ships[5];
                        
            random = new Random();

            for (int i = 0; i < spaceShipArray.Length; i++)
            {                
                int spaceshipPositionX = random.Next(0, Window.ClientBounds.Width - spaceShip.Width);
                int spaceShipPositionY = random.Next(0, Window.ClientBounds.Height - spaceShip.Height);
                Vector2 spaceShipPosition = new Vector2(spaceshipPositionX, spaceShipPositionY);

                int spaceShipVelocityX = 0;
                int spaceShipVelocityY = 0;
                Vector2 spaceShipVelocity = new Vector2(spaceShipVelocityX, spaceShipVelocityY);

                shipScale = random.Next(1,3);

                spaceShipClass = new Ships(spaceShipPosition, spaceShipVelocity, spaceShip, shipScale);
                spaceShipArray[i] = spaceShipClass;                
            }
            /////Audio                                  
            explodeEffect = Content.Load<SoundEffect>("jump-and-spark-6136");
            backgroundSong = Content.Load<Song>("12-Amiga");
            MediaPlayer.Play(backgroundSong);
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Volume = 0.2f;

            crossHair = Content.Load<Texture2D>("pixil-frame-0");

            animation1 = new Animation(Content, "spritesheet", 150f, 9, true);
            

            welcome = Content.Load<Texture2D>("Welcome");
            gameOver = Content.Load<Texture2D>("Over");

            random = new Random();
            meteor = Content.Load<Texture2D>("Asteroid 01 - Base");

            font = Content.Load<SpriteFont>("font");

           

            for (int i = 0; i < 7; i++)
            {
                //Random position of the meteors
                int positionX = random.Next(0, Window.ClientBounds.Width - meteor.Width);
                int positionY = random.Next(0, Window.ClientBounds.Height - meteor.Height);
                Vector2 position = new Vector2(positionX, positionY);

                //Random speed of the meteors
                int velocityX = -1;
                int velocityY = random.Next(-1, 3);
                Vector2 velocity = new Vector2(velocityX, velocityY);

                //The randomzied meteor
                meteorClass = new Meteors(position, velocity, meteor);

                meteorsList.Add(meteorClass);
            }
            


            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            previousMouseState = mouseState;
            mouseState = Mouse.GetState();

            crossHairRect = new Rectangle(mouseState.X - crossHair.Width/2, mouseState.Y - crossHair.Height/2, crossHair.Width,crossHair.Height);

            if(mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released && !isPlaying)
            {
                
                isPlaying = true;

                
            }

            if (!isPlaying)
                return;
                       
            ///if statement won't run if you haven't pressed left mouse button.

            if (mouseState.LeftButton == ButtonState.Pressed && previousMouseState.LeftButton == ButtonState.Released)
            {
                pressed = true;
                released = true;
            }
            else
            {
                pressed = false;
                released = false;
            }
            if(lives > 0)
            {
                                
                foreach (Meteors meteorClass in meteorsList)
                {
                    meteorClass.Update();
                    

                    if (meteorClass.position.Y < 0 - meteor.Height || meteorClass.position.X < 0 - meteor.Width && meteorClass.alive == true)
                    {
                        meteorClass.alive = false;

                        if(meteorClass.alive == false)
                        {
                            lives--;
                            
                        }
                    }
                    else if (meteorClass.meteorSize.Contains(mouseState.X, mouseState.Y) && pressed && released)
                    {
                        isDestroyed = meteorClass.IsDestroyed(mouseState.X, mouseState.Y);
                       

                        if(isDestroyed)
                        {
                            score += 1;

                            explodeEffect.Play();
                        }
                    }
                    if (!meteorClass.alive)
                    {
                        meteorsList.Remove(meteorClass);

                        //Random position of the meteors
                        int positionX = random.Next(0, Window.ClientBounds.Width - meteor.Width);
                        int positionY = random.Next(0, Window.ClientBounds.Height - meteor.Height);
                        Vector2 position = new Vector2(positionX, positionY);

                        //Random speed of the meteors
                        int velocityX = -1;
                        int velocityY = random.Next(-1, 3);
                        Vector2 velocity = new Vector2(velocityX, velocityY);

                        //The randomzied meteor
                        Meteors meteors = new Meteors(position, velocity, meteor);

                        meteorsList.Add(meteors);

                        break;
                    }


                }
            }
            if(lives <= 0)
            {
                meteorsList.Clear();
                
                
            }
                       
            animation1.playAnimation(gameTime);
                        
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            animation1.draw(spriteBatch);
            
            if (isPlaying)
            {
                foreach(Ships spaceShipClass in spaceShipArray)
                {
                    spaceShipClass.Draw(spriteBatch);
                }

                spriteBatch.Draw(crossHair, crossHairRect, Color.White);

                foreach (Meteors meteorClass in meteorsList)
                {
                    meteorClass.Draw(spriteBatch);

                }

                ////Shows score and lives left
                String textAndScore = "Your Current Score " + scoreString;
                scoreString = Convert.ToString(score);
                spriteBatch.DrawString(font, textAndScore, new Vector2(285, 0), Color.White);

                String textAndLives = "Lives Left " + livesString;
                livesString = Convert.ToString(lives);
                spriteBatch.DrawString(font, textAndLives, Vector2.Zero, Color.White);

                if(lives == 0)
                {
                    spriteBatch.Draw(gameOver, new Vector2(45, 150), Color.White);
                    spriteBatch.DrawString(font, "Exit Game To Try Again", new Vector2(270, 300), Color.White);
                }
            }
            else
            {                
                spriteBatch.Draw(welcome, new Vector2(45, 150), Color.White);
                spriteBatch.DrawString(font, "Press Anywhere To Start", new Vector2(270, 250), Color.White);
                               
            }
                       
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
        
    }
}