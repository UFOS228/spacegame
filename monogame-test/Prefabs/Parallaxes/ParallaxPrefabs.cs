using System;
using monogametest.Components;

namespace monogametest.GameObjectPrefabs.Parallaxes
{
	public class Layer1ParallaxPrefab : GameObject
	{
        public override void Init()
        {
            base.Init();
            name = "Layer1Parallax";
            AddComponent(new ParallaxComponent(
                    MyContentManager.Load<Texture2D>("Parallaxes/layer1", ContentType.Textures),
                    Color.White, 6, 1, 0.35f));
        }
    }
    public class AspidParallaxNebPrefab : Layer1ParallaxPrefab
    {
        public override void Init()
        {
            base.Init();
            name = "AspidParallaxNeb";
            SetComponent(new ParallaxComponent(
                MyContentManager.Load<Texture2D>("Parallaxes/AspidParallaxNeb", ContentType.Textures),
                Color.White, 6, 0.99f, 0.4f));
        }
    }
}

