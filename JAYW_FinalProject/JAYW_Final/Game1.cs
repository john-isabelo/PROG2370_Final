/*+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-++-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+.
 * FILE         : Game1.cs
 * PROJECT      : JAYW_Final.cs
 * PROGRAMMER   : John Isabelo Aldeguer
 *                YuYuan (Levis) Wang
 * FIRST VERSION: 2021 - 12 - 10
 * DESCRIPTION  :
 *      This Program controls the main games
 *      
 * Source File   
 * Link         : https://www.youtube.com/watch?v=NAKAGw9Lrk0
 * Title        : "C# MonoGame Snake"
 * Author       : Romain Marcazzan
 *+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+*/

using JAYW_Final.Classes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace JAYW_Final
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        SpriteFont font;
        cSnake snake;
        cFood food;
        Random rnd;

        const int gameHeight = 100;
        const int gameWidth = 100;
        const int snakeSize = 10;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            rnd = new Random();
            graphics.PreferredBackBufferWidth = gameWidth * snakeSize;
            graphics.PreferredBackBufferHeight = gameHeight * snakeSize;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            // TODO: use this.Content to load your game content here
            snake = new cSnake(this, GraphicsDevice, spriteBatch, snakeSize);
            food = new cFood(this, spriteBatch, GraphicsDevice, snakeSize);

            font = Content.Load<SpriteFont>("Font");

            this.Components.Add(snake);
            this.Components.Add(food);
        }

        protected override void UnloadContent()
        {

            base.UnloadContent();
        }

        public void SetFoodLocation()
        {
            food.PosX = rnd.Next(0, GraphicsDevice.Viewport.Width / snakeSize) * snakeSize;
            food.PosY = rnd.Next(0, GraphicsDevice.Viewport.Height / snakeSize) * snakeSize;
            food.Active = true;
        }

        public void CheckSnakeFood()
        {
            if (snake.PosX == food.PosX && snake.PosY == food.PosY)
            {
                snake.Score++;
                if (snake.Score > snake.HighScore)
                {
                    snake.HighScore = snake.Score;
                }
                SetFoodLocation();
                snake.AddTail();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            // TODO: Add your update logic here
            if (!food.Active)
            {
                SetFoodLocation();
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                snake.DirX = 0;
                snake.DirY = -1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                snake.DirX = 0;
                snake.DirY = 1;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                snake.DirX = 1;
                snake.DirY = 0;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Left))
            {
                snake.DirX = -1;
                snake.DirY = 0;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Space) && !snake.Run)
            {
                snake.Run = true;
                snake.ResetSnake();
            }
            CheckSnakeFood();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            //GraphicsDevice.Clear(new Color(51,51,51));
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            spriteBatch.DrawString(font,"Score: " + snake.Score.ToString() 
                + "                                                                             High Score: " + snake.HighScore.ToString()
                ,new Vector2(snakeSize), Color.LightGray);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
