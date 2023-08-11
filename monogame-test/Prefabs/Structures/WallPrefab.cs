using System;
using monogame_test;
using monogametest.Components;

namespace monogametest.Prefabs
{
	public class WallPrefab : GameObject
	{
        public override void Init()
		{
            base.Init();
            name = "Wall";
            scale = new Vector2(2);
            components = new Component[]
            {
                new RendererComponent(MyContentManager.Load<Texture2D>("Structures/full", ContentType.Textures), Color.White, 0.8f),
                new CollisionComponent(new Vector2(64)),
            };
        }
	}
}

