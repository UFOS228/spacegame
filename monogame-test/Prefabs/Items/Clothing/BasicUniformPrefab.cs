using System;
using monogametest.Components;

namespace monogametest.Prefabs
{
	public class BasicUniformPrefab : GameObject
	{
        public override void Init()
        {
            base.Init();
            name = "Base uniform";
            AddComponent(new WearableComponent(MyContentManager.Load<Texture2D>("Item/Wearable/greyUniform", ContentType.Textures),
                MyContentManager.Load<Texture2D>("Item/Wearable/greyUniform", ContentType.Textures),
                0.5f, 1));
        }
    }
}

