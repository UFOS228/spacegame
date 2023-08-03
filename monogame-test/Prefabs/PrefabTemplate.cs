using System;
using monogametest.Components;

namespace monogametest.GameObjectPrefabs
{
	public class PrefabTemplate : GameObject
	{
        public override void Init()
        {
            base.Init();
            name = "Sample text";
            components = new List<object>()
            {
                new RendererComponent(MyContentManager.Load<Texture2D>("shuttlewhite"), Color.White, SpriteEffects.None, 1f)
            };
        }
    }
}

