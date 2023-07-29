using System;
using static System.Formats.Asn1.AsnWriter;
using System.ComponentModel;
using monogame_test;

namespace monogametest
{
	public static class ObjectManager
	{
		public static Game1 game;
		public static List<GameObject> objectsOnMap = new List<GameObject>();
		public static void Destroy(GameObject obj)
		{
			foreach (var item in obj.components)
			{
				item.OnDestroy();
			}
			objectsOnMap.Remove(obj);
		}
		public static float Distance(Vector2 pos1, Vector2 pos2)
		{
			return MathF.Sqrt(MathF.Pow(pos2.X - pos1.X, 2) + MathF.Pow(pos2.Y - pos1.Y, 2));
		}
		public static GameObject SpawnObject(GameObject obj, Vector2 pos, float rot = 0)
		{
			obj.game = game;
            obj.position = pos;
            obj.rotation = rot;
            foreach (var item in obj.components)
            {
                item.gameObject = obj;
                item.Init();
            }
			return obj;
        }
	}
}

