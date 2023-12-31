﻿using System;
using spacegame.Components;

namespace spacegame.Prefabs
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
                //new PhysicsComponent() {bounceFactor = 1},
                new PlayerComponent(index),
                new DelayComponent(0.5f),
                new RendererComponent(MyContentManager.Load<Texture2D>("sceleton", ContentType.Textures), Color.White, 0.1f),
                new PlayerInteraction(),
                new InventoryComponent(),
                new CollisionComponent(new Vector2(45,64)),
            };
            scale = new Vector2(2);
        }
    }
}

