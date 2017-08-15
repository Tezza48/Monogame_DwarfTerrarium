using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TerrariaStyleWorld.Graphics;

namespace TerrariaStyleWorld
{
    class Tile
    {
        /*
         class to represent a tile in the game world
         the tile in the world should be 10 units across no matter what the size
         the tile's position will refer to it's centre
             */

        // Const fields
        public static readonly int TILE_SIZE = 8;

        // Enumerations
        public enum ETileType : int
        {
            Grass, Dirt, Rock, Air
        }

        // Static
        public static Texture2D TileTextureAtlas;

        // Private fields
        private ETileType mTileType;
        private Vector2 mPosition;
        private ushort mTileVariation; // Variation on tile type( 0 => 3 )

        public Vector2 Position { get => mPosition; set => mPosition = value; }

        // Ctor
        public Tile(ETileType tileType, Vector2 position, ushort variation)
        {
            mTileType = tileType;
            mPosition = position;
            mTileVariation = variation;
        }

        // Methods
        public void Draw(SpriteBatch spriteBatch)
        {
            Vector2 screenLocation = WorldGrid.WorldToScreen(mPosition - Vector2.One * 5);
            
            Rectangle destRectangle = new Rectangle(screenLocation.ToPoint(), new Point(10));
            
            spriteBatch.Draw(TileTextureAtlas, destRectangle, 
                Atlas.GetRectAt((int)mTileType, TILE_SIZE, TileTextureAtlas.Bounds, mTileVariation), 
                Color.White);
        }

        public Rectangle getBounds()
        {
            return new Rectangle(mPosition.ToPoint(), new Point(TILE_SIZE));
        }
    }
}