using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TerrariaStyleWorld
{
    class World
    {
        private Tile[,] mWorld;
        private Point mWorldBounds;

        public World(Point mWorldBounds)
        {
            this.mWorldBounds = mWorldBounds;
        }

        public void Generate(out int numTilesTotal)
        {
            mWorld = new Tile[mWorldBounds.X, mWorldBounds.Y];
            Random rand = new Random(1);

            numTilesTotal = mWorldBounds.X * mWorldBounds.Y;

            Tile.ETileType type;
            for (int y = 0; y < mWorldBounds.Y; y++)
            {
                for (int x = 0; x < mWorldBounds.X; x++)
                {

                    if (y < mWorldBounds.Y - 40)
                    {
                        type = Tile.ETileType.Rock;
                    }
                    else if (y < mWorldBounds.Y - 24)
                    {
                        type = Tile.ETileType.Dirt;
                    }
                    else if (y < mWorldBounds.Y - 20)
                    {
                        type = Tile.ETileType.Grass;
                    }
                    else if (y < mWorldBounds.Y - 1)
                    {
                        type = Tile.ETileType.Air;
                    }
                    else
                    {
                        type = Tile.ETileType.Rock;
                    }
                    ushort variance = 0;
                    double die = rand.NextDouble();
                    if (die < 0.75)
                    {
                        variance = 0;
                    }
                    else if (die < 0.8125)
                    {
                        variance = 1;
                    }
                    else if (die < 0.875)
                    {
                        variance = 2;
                    }
                    else if (die < 0.9375)
                    {
                        variance = 3;
                    }
                    mWorld[x, y] = new Tile(type, new Vector2(x - mWorldBounds.X / 2, y - mWorldBounds.Y / 2) * 10, variance);
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch, Camera camera, Rectangle viewportBounds, ref int numTilesDrawn)
        {
            numTilesDrawn = 0;

            //Point camPos = camera.Position.ToPoint();

            Rectangle camBounds = camera.getBounds(viewportBounds);


            Vector2 tilePos;
            for (int y = 0; y < mWorldBounds.Y; y++)
            {
                tilePos = mWorld[0, y].Position;
                if (camBounds.Bottom - 10 < tilePos.Y && tilePos.Y < camBounds.Top + 20)
                {
                    for (int x = 0; x < mWorldBounds.X; x++)
                    {
                        // if the tile is within the viewport bounds, render it
                        tilePos = mWorld[x, y].Position;

                        if (camBounds.Left - 10 < tilePos.X && tilePos.X < camBounds.Right + 10)
                        {
                            mWorld[x, y].Draw(spriteBatch);
                            numTilesDrawn++;
                        }
                    }
                }
            }

            //for (int y = 0; y < mWorldBounds.Y; y++)
            //{
            //    for (int x = 0; x < mWorldBounds.X; x++)
            //    {
            //        mWorld[x, y].Draw(spriteBatch);
            //        numTilesDrawn++;
            //    }
            //}
        }

    }
}
