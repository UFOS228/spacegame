using System;
using Microsoft.Xna.Framework.Graphics;

namespace spacegame.Components
{
	//public class Sprite
	//{
	//	public Texture2D texture;
	//	public Color color;
	//}
	public class RendererComponent : Component
	{
		public RendererComponent(Texture2D texturee, Color colorr, float depthh, SpriteEffects flippingg = SpriteEffects.None)
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

        public override void OnDraw()
        {
            base.OnDraw();
            if (Vector2.Distance(game.cameraPosCentered, -gameObject.position) <= (game._graphics.PreferredBackBufferHeight + game._graphics.PreferredBackBufferWidth) * (game.minZoom + game.maxZoom))
            {
                game._spriteBatch.Draw(texture, gameObject.position + game.cameraPosCenteredLerped, null,
                    color, gameObject.rotation, new Vector2(texture.Width / 2, texture.Height / 2), gameObject.scale, flipping, layerDepth);
            }
        }
    }
}

