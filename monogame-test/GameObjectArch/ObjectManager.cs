using System;
using static System.Formats.Asn1.AsnWriter;
using System.ComponentModel;
using spacegame;

namespace spacegame
{
	public static class ObjectManager
	{
		public static Game1 game;
		public static List<GameObject> objectsOnMap = new List<GameObject>();
		public static List<GameObject> destroyQueue = new List<GameObject>();
		public static void Destroy(GameObject obj)
		{
			foreach (var item in obj.components)
			{
				((Component) item).OnDestroy();
			}
			destroyQueue.Add(obj);
		}
		public static GameObject SpawnObject(GameObject obj, Vector2 pos, float rot = 0)
		{
			obj.Init();
			obj.game = game;
            obj.position = pos;
            obj.rotation = rot;
			objectsOnMap.Add(obj);
            for (int i = 0; i < obj.components.Length; i++)
            {
                ((Component)obj.components[i]).gameObject = obj;
                ((Component)obj.components[i]).Init();
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
		public static GameObject FindObjectWithTag(string tag)
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
		public static void DestroyAllQueue()
        {
            foreach (var item in destroyQueue)
            {
				objectsOnMap.Remove(item);
			}
			destroyQueue.Clear();
        }
	}
}

