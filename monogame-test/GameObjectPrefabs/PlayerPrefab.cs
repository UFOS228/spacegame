using System;
using monogametest.Components;

namespace monogametest.GameObjectPrefabs
{
	public class PlayerPrefab : GameObject
	{
        public override void Init()
        {
            base.Init();
            name = "Player";
            tags = new List<string> { "player" };
            components = new List<Component>()
            {
                new PlayerComponent(),
                new RendererComponent(MyContentManager.Load<Texture2D>("sceleton", ContentType.Textures), Color.White, SpriteEffects.None, 0.1f)
            };
            scale = new Vector2(2);
        }
    }
}

