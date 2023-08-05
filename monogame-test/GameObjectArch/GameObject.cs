using monogame_test;

namespace monogametest
{
    public class GameObject
	{
		public GameObject(string nameOfObj, object[] component, Vector2 pos, Vector2 scalee,
			float rot = 0, string[] tagsOfThisObj = null)
		{
            name = nameOfObj;
            tags = tagsOfThisObj.ToList();
			components = component;
			position = pos;
			scale = scalee;
			rotation = rot;
        }
        public GameObject(string nameOfObj, object[] component, string[] tagsOfThisObj = null)
        {
            name = nameOfObj;
			if (tagsOfThisObj != null)tags = tagsOfThisObj.ToList();
            components = component;
        }
		public GameObject() { }

		public bool isActive = true;
		public string name;
		public List<string> tags;
		public Vector2 position;
		public float rotation;
		public Vector2 scale;
		public object[] components = new Component[0];
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
			components.ToList().Add(component);
            //components = ;
		}
		public void SetComponent<T>(T value) where T:Component
		{
			for (int i = 0; i < components.Length; i++)
			{
                if (components[i] is T)
                {
					components[i] = value;
                }
            }
            throw new NullReferenceException();
        }
		//public int GetComponentIndex<T>() where T: Component
		//{
  //          for (int i = 0; i < components.Count; i++)
  //          {
		//		Console.WriteLine(components[i]);
  //              if (components[i] is T)
  //              {
		//			return i;
  //              }
		//		else if (components[i].GetType().IsSubclassOf(typeof(T)))
  //              {
  //                  return i;
  //              }
  //          }
		//	throw new NullReferenceException();
  //      }
		public ref object GetComponentRef<T>() where T: Component
		{
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] is T)
                {
                    return ref components[i];
                }
            }
            throw new NullReferenceException();
        }
        public T GetComponent<T>() where T : Component
        {
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] is T)
                {
                    return components[i] as T;
                }
            }
            throw new NullReferenceException();
        }
        public void GetComponent<T>(out T comp) where T : Component
        {
            for (int i = 0; i < components.Length; i++)
            {
                if (components[i] is T)
                {
					comp = components[i] as T;
                }
            }
            throw new NullReferenceException();
        }
        public bool TryGetComponent<T>(out T comp) where T: Component
		{
			try
			{
				comp = GetComponent<T>();
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

