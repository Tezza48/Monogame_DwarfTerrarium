using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TerrariaStyleWorld.Graphics
{
    /*
     The world grid refers the graph in which the game works on using Y up and X right,
     This class conteins functions to convert these World pcoordinates into screen positions
         */
    static class WorldGrid
    {
        public static Vector2 WorldToScreen(Vector2 worldPos)
        {
            return new Vector2(worldPos.X, -worldPos.Y);
        }
        public static Point WorldToScreen(Point worldPos)
        {
            return new Point(worldPos.X, -worldPos.Y);
        }
    }
}
