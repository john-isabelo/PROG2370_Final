/*+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+.
 * FILE         : cFood.cs
 * PROJECT      : JAYW_Final.cs
 * PROGRAMMER   : John Isabelo Aldeguer
 *                YuYuan (Levis) Wang
 * FIRST VERSION: 2021 - 12 - 10
 * DESCRIPTION  :
 *      This Program controls the Food movement
 *+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-*/

using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace JAYW_Final.Classes
{
    class cFood : DrawableGameComponent
    {
        SpriteBatch spriteBatch;
        Texture2D pixel;

        int size;
        public int PosX { get; set; }
        public int PosY { get; set; }
        public bool Active { get; set; } = false;

        public cFood(Game game,SpriteBatch spriteBatch,GraphicsDevice graphics, int size): base(game)
        {
            this.spriteBatch = spriteBatch;
            this.size = size;
            pixel = new Texture2D(graphics, 1, 1);
            pixel.SetData(new Color[] { Color.White });
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            if (Active)
            {
                spriteBatch.Draw(pixel, new Rectangle(PosX, PosY, size, size), Color.Gold);
            }
            
            spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
