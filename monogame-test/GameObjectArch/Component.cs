using System;
namespace monogametest
{
	public abstract class Component
	{
		public GameObject gameObject;
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
	}
}

