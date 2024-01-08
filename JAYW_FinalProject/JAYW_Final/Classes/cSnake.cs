/*+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+.
 * FILE         : cSnake.cs
 * PROJECT      : JAYW_Final.cs
 * PROGRAMMER   : John Isabelo Aldeguer
 *                YuYuan (Levis) Wang
 * FIRST VERSION: 2021 - 12 - 10
 * DESCRIPTION  :
 *      This Program controls the Snake movement
 *+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-*/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace JAYW_Final.Classes
{
    class cSnake : DrawableGameComponent
    {
        const int updateInterval = 50;

        int size = 0;
        int milliSecondsSinceLastUpdate = 0;
        int oldPosX = 0;
        int oldPosY = 0;

        public int Score { get; set; } = 0;
        public bool Run { get; set; } = false;
        public int PosX { get; set; } = 0;
        public int PosY { get; set; } = 0;
        public int DirX { get; set; } = 1;
        public int DirY { get; set; } = 0;
        public int HighScore { get; set; } = 0;

        SpriteBatch spriteBatch;
        Texture2D pixel;
        GraphicsDevice graphics;
        List<Rectangle> tailList;


        public cSnake(Game game, GraphicsDevice graphics, SpriteBatch spriteBatch, int size) : base(game)
        {
            this.size = size;
            this.spriteBatch = spriteBatch;
            this.graphics = graphics;

            pixel = new Texture2D(graphics, 1, 1);
            pixel.SetData(new Color[] { Color.White });

            PosX = graphics.Viewport.Width / 2;
            PosY = graphics.Viewport.Height / 2;

            tailList = new List<Rectangle>();
            tailList.Add(new Rectangle(PosX, PosY, size, size));

        }

        public override void Update(GameTime gameTime)
        {
            milliSecondsSinceLastUpdate += gameTime.ElapsedGameTime.Milliseconds;
            if (milliSecondsSinceLastUpdate >= updateInterval && Run)
            {
                milliSecondsSinceLastUpdate = 0;

                oldPosX = PosX;
                oldPosY = PosY;

                PosX = PosX + DirX * size;
                PosY = PosY + DirY * size;

                if (PosY == -size || PosY == graphics.Viewport.Height || PosX == -size || PosX == graphics.Viewport.Width)
                {
                    Run = false;
                    PosX = oldPosX;
                    PosY = oldPosY;
                    return;
                }

                if (tailList.Count > 1)
                {
                    for (int i = tailList.Count-1; i > 0; i--)
                    {
                        if (PosX == tailList[i].X && PosY == tailList[i].Y)
                        {
                            Run = false;
                            PosX = oldPosX;
                            PosY = oldPosY;
                            return;
                        }
                        tailList[i] = new Rectangle(tailList[i - 1].X, tailList[i - 1].Y, size, size);
                    }
                }
            }
            tailList[0] = new Rectangle(PosX, PosY, size, size);
            base.Update(gameTime);
        }

        public void AddTail()
        {
            tailList.Add(new Rectangle(PosX,PosY,size,size));
        }

        public void ResetSnake()
        {
            tailList.Clear();
            Score = 0;
            PosX = graphics.Viewport.Width / 2;
            PosY = graphics.Viewport.Height / 2;

            tailList.Add(new Rectangle(PosX, PosY, size, size));
            Run = true;
        }

        public override void Draw(GameTime gameTime)
        {
            
            spriteBatch.Begin();
            if (Run)
            {
                foreach (var item in tailList)
                {
                    spriteBatch.Draw(pixel, new Rectangle(item.X - 1, item.Y - 1, size + 2, size + 2), Color.Gray);
                    spriteBatch.Draw(pixel, item, Color.AntiqueWhite);
                }
            }
            else
            {
                foreach (var item in tailList)
                {
                    spriteBatch.Draw(pixel, new Rectangle(item.X - 1, item.Y - 1, size + 2, size + 2), Color.Gray);
                    spriteBatch.Draw(pixel, item, Color.Red);
                }
            }
            

            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
