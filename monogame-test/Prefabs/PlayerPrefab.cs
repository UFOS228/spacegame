using System;
using monogametest.Components;

namespace monogametest.GameObjectPrefabs
{
	public class PlayerPrefab : GameObject
	{
        private PlayerIndex index;
        public PlayerPrefab(PlayerIndex plIndex)
        {
            index = plIndex;
        }
        public override void Init()
        {
            base.Init();
            name = "Player";
            tags = new List<string> { "player" };
            components = new Component[]
            {
                new PlayerComponent(index),
                new RendererComponent(MyContentManager.Load<Texture2D>("sceleton", ContentType.Textures), Color.White, SpriteEffects.None, 0.1f),
                new PlayerInteraction(),
                new InventoryComponent(),
            };
            scale = new Vector2(2);
        }
    }
}

