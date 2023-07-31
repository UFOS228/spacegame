using System;
using monogametest.Components;

namespace monogametest.GameObjectPrefabs.Parallaxes
{
	public class Layer1ParallaxPrefab : GameObject
	{
        public Layer1ParallaxPrefab()
        {
            name = "Layer1Parallax";
            components = new List<Component>()
            {
                new ParallaxComponent(
                    MyContentManager.Load<Texture2D>("Parallaxes/layer1", ContentType.Textures),
                    Color.White, 2, 1, 0.35f)
            };
        }
    }
    public class AspidParallaxNebPrefab : GameObject
    {
        public AspidParallaxNebPrefab()
        {
            name = "AspidParallaxNeb";
            components = new List<Component>()
            {
                new ParallaxComponent(
                    MyContentManager.Load<Texture2D>("Parallaxes/AspidParallaxNeb", ContentType.Textures),
                    Color.White, 1, 0.99f, 0.4f)
            };
        }
    }
}

