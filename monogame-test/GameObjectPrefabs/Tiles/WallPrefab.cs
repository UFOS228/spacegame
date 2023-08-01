using System;
using monogame_test;
using monogametest.Components;

namespace monogametest.GameObjectPrefabs
{
	public class WallPrefab : GameObject
	{
        public override void Init()
		{
            base.Init();
            name = "Wall";
            scale = new Vector2(2);
            AddComponent(new RendererComponent(
                    MyContentManager.Load<Texture2D>("shuttlewhite"), Color.White,
                    SpriteEffects.None, 0.8f));
        }
	}
}

