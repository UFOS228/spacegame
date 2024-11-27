using System;
using System.Reflection;
using System.Linq;
using System.Diagnostics;

namespace spacegame.Components
{
	public abstract class Tile
	{
		public Texture2D tileTexture;
		public int palleteId;
		public virtual void Init()
		{

		}
	}
    public class AirTile : Tile
    {
        public override void Init()
        {
            base.Init();
            palleteId = 0;
        }
    }
    public class LatticeTile : Tile
	{
        public override void Init()
        {
            base.Init();
			tileTexture = MyContentManager.Load<Texture2D>("Tiles/lattice", ContentType.Textures);
			palleteId = 1;
        }
    }
	public class TilemapComponent : Component
	{
		public static List<Tile> tilePallete = new List<Tile>();
        public static float tilemapsDepth = 0.9f;
        public static float tilemapsScale = 2;
        public int[,] tiles;

        public override void Init()
        {
            IEnumerable<Type> list = Assembly.GetAssembly(typeof(Tile)).GetTypes().Where(type => type.IsSubclassOf(typeof(Tile)));
            foreach (var item in list)
            {
                tilePallete.Add((Tile) Activator.CreateInstance(item));
            }
            foreach (var tile in tilePallete)
            {
                tile.Init();
            }
            tiles = new int[,]
            {
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 1, 1, 1, 0, 0},
                {0, 1, 0, 1, 0, 1, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 1, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
                {0, 0, 0, 0, 0, 0, 0},
            };
        }
        public override void OnDraw()
		{
            for (int Y = 0; Y < tiles.GetLength(0); Y++)
            {
                for (int X = 0; X < tiles.GetLength(1); X++)
                {
                    //Debug.WriteLine(FindTile(tiles[Y, X]).tileTexture.Width);
                    if (FindTile(tiles[Y, X]).tileTexture != null)
                    game._spriteBatch.Draw(
                        FindTile(tiles[Y,X]).tileTexture, 
                        new Vector2((X * game.oneTileScale) + gameObject.position.X + game.cameraPosCenteredLerped.X, (Y * game.oneTileScale) + gameObject.position.Y + game.cameraPosCenteredLerped.Y), 
                        null, Color.White, 0, Vector2.Zero, tilemapsScale, SpriteEffects.None, tilemapsDepth);
                }
            }
		}
        public static Tile FindTile(int id)
        {
            foreach (var tile in tilePallete)
            {
                if (tile.palleteId == id)
                {
                    return tile;
                }
            }
            throw new Exception($"Tile under {id} id, not finded!");
        }
        public Tile GetTile(Vector2 localPos)
        {
            return FindTile(tiles[(int)(localPos.Y / (game.oneTileScale * tilemapsScale)), (int)(localPos.X / (game.oneTileScale * tilemapsScale))]);
        }
    }
}

