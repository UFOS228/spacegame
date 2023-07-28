using System;
namespace monogametest
{
	public class GameObject
	{
		public GameObject(Component[] component, Vector2 pos, Vector2 scalee, float rot = 0)
		{
			components = component.ToList();
			position = pos;
			scale = scalee;
			rotation = rot;
		}
		public GameObject() { }

		public Vector2 position = Vector2.Zero;
		public float rotation = 0;
		public Vector2 scale = Vector2.One;
		public List<Component> components;

		public void AddComponent(Component component)
		{
			component.gameObject = this;
			component.Init();
			components.Add(component);
		}
	}
}

