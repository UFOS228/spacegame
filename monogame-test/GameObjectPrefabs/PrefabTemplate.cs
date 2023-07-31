using System;
using monogametest.Components;

namespace monogametest.GameObjectPrefabs
{
	public class PrefabTemplate : GameObject
	{
        public PrefabTemplate()
        {
            name = "Sample text";
            components = new List<Component>()
            {
                new RendererComponent(MyContentManager.Load<Texture2D>("shuttlewhite"), Color.White, SpriteEffects.None, 1f)
            };
        }
    }
}

