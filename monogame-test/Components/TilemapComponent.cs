using System;
using System.Reflection;
using System.Linq;
using monogame_test;

namespace monogametest.Components
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
            tileTexture = new Texture2D(Game1.instance._graphics.GraphicsDevice, 32, 32);
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
		public static List<GameObject> tilePallete = new List<GameObject>();
		public int[,] tiles;

        public override void Init()
        {
            Type ourtype = typeof(Tile);
            IEnumerable<Type> list = Assembly.GetAssembly(ourtype).GetTypes().Where(type => type.IsSubclassOf(ourtype));
        }
        public override void OnDraw()
		{
			//game._spriteBatch.Draw();
		}
    }
}

