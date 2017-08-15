using Microsoft.Xna.Framework;

namespace TerrariaStyleWorld.Graphics
{
    class Atlas
    {
        public static Rectangle GetRectAt(int position, int tileSize, Rectangle texBounds, ushort variation = 0)
        {
            int xPos = position % texBounds.Width;
            int yPos = variation + (position / texBounds.Height * 4);

            return new Rectangle(xPos * tileSize , yPos * tileSize , tileSize, tileSize);
        }
    }
}
