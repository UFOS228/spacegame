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

		public string name;
		public List<string> tags;
		public Vector2 position;
		public float rotation;
		public Vector2 scale;
		public List<Component> components = new List<Component>();
		public Game1 game;

		public virtual void Init()
		{
			name = "Text";
			tags = new List<string>();
			position = Vector2.Zero;
			rotation = 0;
			scale = Vector2.One;

        }
		public void AddComponent(Component component)
		{
			component.gameObject = this;
			component.Init();
			components.Add(component);
		}
		public void SetComponent<T>(T value) where T:Component
		{
            components[GetComponentIndex<T>()] = value;
        }
		public int GetComponentIndex<T>() where T: Component
		{
            for (int i = 0; i < components.Count; i++)
            {
                if (components[i] is T)
                {
					return i;
                }
            }
			throw new NullReferenceException();
        }
		public T GetComponent<T>() where T: Component
		{
            return (T)components[GetComponentIndex<T>()];
		}
        public void GetComponent<T>(out T comp) where T : Component
        {
            comp = (T)components[GetComponentIndex<T>()];
        }
        public bool TryGetComponent<T>(out T comp) where T: Component
		{
			try
			{
				comp = (T) components[GetComponentIndex<T>()];
				return true;
			}
			catch
			{
				comp = null;
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

