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
		public static GameObject SpawnObject(GameObject obj, Vector2 pos, float rot = 0)
		{
			obj.Init();
			obj.game = game;
            obj.position = pos;
            obj.rotation = rot;
			objectsOnMap.Add(obj);
            for (int i = 0; i < obj.components.Count; i++)
            {
                obj.components[i].gameObject = obj;
                obj.components[i].Init();
            }
			return obj;
        }
		public static GameObject[] FindObjectsWithTag(string tag)
		{
			List<GameObject> obj = new List<GameObject>();
			foreach (var item in objectsOnMap)
			{
				if (GameManager.HasItemInArray(item.tags.ToArray(), tag))
				{
					obj.Add(item);
				}
			}
			return obj.ToArray();
		}
		public static GameObject? FindObjectWithTag(string tag)
		{
			try
			{
				return FindObjectsWithTag(tag)[0];
			}
			catch
			{
				return null;
			}
		}
	}
}

