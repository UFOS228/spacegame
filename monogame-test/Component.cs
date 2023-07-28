using System;
using monogametest;

namespace Game
{
	public abstract class IComponent
	{
		public abstract void Init(GameObject );
		public abstract void Update();
	}
}

