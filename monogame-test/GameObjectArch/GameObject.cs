using System;
using monogame_test;

namespace monogametest
{
	public class GameObject
	{
		public GameObject(string nameOfObj, Component[] component, Vector2 pos, Vector2 scalee,
			float rot = 0, string[] tagsOfThisObj = null)
		{
			name = nameOfObj;
            tags = tagsOfThisObj.ToList();
			components = component.ToList();
			position = pos;
			scale = scalee;
			rotation = rot;
        }
        public GameObject(string nameOfObj, Component[] component, string[] tagsOfThisObj = null)
        {
			name = nameOfObj;
			if (tagsOfThisObj != null)tags = tagsOfThisObj.ToList();
            components = component.ToList();
        }
		public GameObject() { }

		public string name = "Text";
		public List<string> tags = new List<string>();
		public Vector2 position = Vector2.Zero;
		public float rotation = 0;
		public Vector2 scale = Vector2.One;
		public List<Component> components;
		public Game1 game;

		public void AddComponent(Component component)
		{
			component.gameObject = this;
			component.Init();
			components.Add(component);
		}
		public T GetComponent<T>()
		{
			foreach (object comp in components)
			{
				if (comp.GetType() == typeof(T))
				{
					return (T) comp;
				}
			}
			throw new NullReferenceException();
		}
        public bool TryGetComponent<T>(out T comp)
		{
			try
			{
				comp = GetComponent<T>();
				return true;
			}
			catch
			{
				comp = default;
				return false;
			}
		}
		public void AddTag(string tag)
		{
			tags.Add(tag);
		}
		public void RemoveTag(string tag)
		{
			tags.Remove(tag);
		}

    }
}

