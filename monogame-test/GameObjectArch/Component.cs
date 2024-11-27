using System;
using spacegame;

namespace spacegame
{
	public abstract class Component
	{
		public GameObject gameObject;
		public Game1 game
		{
			get
			{
				return gameObject.game;
			}
		}
		public virtual void Init()
		{

		}
		public virtual void Update()
		{

		}
		public virtual void OnDraw()
		{

		}
		public virtual void OnDestroy()
		{

		}

        public void AddComponent(Component component)
        {
            gameObject.AddComponent(component);
        }
        public void SetComponent<T>(T value) where T : Component
        {
            gameObject.SetComponent(value);
        }
        public T GetComponent<T>() where T : Component
        {
            return gameObject.GetComponent<T>();
        }
        public void GetComponent<T>(out T comp) where T : Component
        {
            gameObject.GetComponent(out comp);
        }
        public bool TryGetComponent<T>(out T comp) where T : Component
        {
            return gameObject.TryGetComponent(out comp);
        }
    }
}

