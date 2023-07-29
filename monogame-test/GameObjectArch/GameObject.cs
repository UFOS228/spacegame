using System;
using monogame_test;

namespace monogametest
{
	public class GameObject
	{
		public GameObject(Game1 currentGame, Component[] component, Vector2 pos, Vector2 scalee, float rot = 0)
		{
			game = currentGame;
			components = component.ToList();
			position = pos;
			scale = scalee;
			rotation = rot;
        }
		public GameObject(Game1 currentGame)
		{
			game = currentGame;
        }
		public GameObject() { }

		public string name = "Text";
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

    }
}

