using System;
namespace monogametest.Components
{
	//public class Sprite
	//{
	//	public Texture2D texture;
	//	public Color color;
	//}
	public class RendererComponent : Component
	{
		public RendererComponent(Texture2D texturee, Color colorr, SpriteEffects flippingg, float depthh)
		{
			texture = texturee;
			color = colorr;
			flipping = flippingg;
			layerDepth = depthh;
		}
        public Texture2D texture;
        public Color color;
		public SpriteEffects flipping;
		public float layerDepth;
    }
}

