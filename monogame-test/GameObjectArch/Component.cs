using System;
using monogame_test;

namespace monogametest
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
	}
}

