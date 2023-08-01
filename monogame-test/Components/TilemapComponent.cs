using System;
using System.Reflection;
using System.Linq;

namespace monogametest.Components
{
	public abstract class Tile
	{
		public Texture2D tileTexture;

		public virtual void Init()
		{

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

            foreach (Type itm in list)
            {
                Console.WriteLine(itm);
            }
        }
        public override void OnDraw()
		{
			game._spriteBatch.Draw();
		}
    }
}

