using System;
using monogametest.Components;

namespace monogametest.Prefabs
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
                new DelayComponent(0.5f),
                new RendererComponent(MyContentManager.Load<Texture2D>("sceleton", ContentType.Textures), Color.White, 0.1f),
                new PlayerInteraction(),
                new InventoryComponent(),
                new CollisionComponent(new Vector2(45,64)),
                new PhysicsComponent() {bounceFactor = 1},
            };
            scale = new Vector2(2);
        }
    }
}

