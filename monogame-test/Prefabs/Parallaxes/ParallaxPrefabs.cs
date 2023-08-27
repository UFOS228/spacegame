using System;
using spacegame.Components;

namespace spacegame.Prefabs.Parallaxes
{
	public class Layer1ParallaxPrefab : GameObject
	{
        public override void Init()
        {
            base.Init();
            name = "Layer1Parallax";
            components = new Component[]
            {
                new ParallaxComponent(
                MyContentManager.Load<Texture2D>("Parallaxes/layer1", ContentType.Textures),
                    Color.White, 6, 1, 0.35f)
            };
        }
    }
    public class AspidParallaxNebPrefab : Layer1ParallaxPrefab
    {
        public override void Init()
        {
            base.Init();
            name = "AspidParallaxNeb";
            components = new Component[]
            {
                new ParallaxComponent(
                MyContentManager.Load<Texture2D>("Parallaxes/AspidParallaxNeb", ContentType.Textures),
                Color.White, 6, 0.99f, 0.4f)
            };
        }
    }
}

