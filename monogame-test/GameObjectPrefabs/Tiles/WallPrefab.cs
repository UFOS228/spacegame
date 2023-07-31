using System;
using monogame_test;
using monogametest.Components;

namespace monogametest.GameObjectPrefabs
{
	public class WallPrefab : GameObject
	{
		public WallPrefab()
		{
            name = "Wall";
            components = new List<Component>() {new RendererComponent(
                    MyContentManager.Load<Texture2D>("shuttlewhite"), Color.White,
                    SpriteEffects.None, 0.8f)};
            scale = new Vector2(2);
        }
	}
}

