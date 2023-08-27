using System;
namespace spacegame.Components
{
	public class ParallaxComponent : Component
	{
		public ParallaxComponent(Texture2D parallaxLayerTexture, Color parallaxTextureColor, float textureScale, float depthh, float moveScalee)
		{
			texture = parallaxLayerTexture;
			depth = depthh;
			moveScale = moveScalee;
			position = Vector2.Zero;
			color = parallaxTextureColor;
			scale = textureScale;
		}

		public Texture2D texture;
		public float scale;
		public Color color;
		public Vector2 position, position2, position3, position4;
		public float depth;
		public float moveScale;

        public override void Update()
        {
			position = game.cameraPosLerped * moveScale;
			position.X %= texture.Width * scale;
			position.Y %= texture.Height * scale;
            position2.Y = position.Y;
            if (position.X >= 0)
			{
				position2.X = position.X - (texture.Width * scale);
			}
			else
			{
                position2.X = position.X + (texture.Width * scale);
            }

			position3.X = position.X;
			position4.X = position2.X;
            if (position.Y >= 0)
            {
                position3.Y = position.Y - (texture.Height * scale);
            }
            else
            {
                position3.Y = position.Y + (texture.Height * scale);
            }
			position4.Y = position3.Y;

            //Console.WriteLine(gameObject.name + " pos1:" + position);
            //Console.WriteLine(gameObject.name + " pos2:" + position2);
            //Console.WriteLine(gameObject.name + " pos3:" + position3);
            //Console.WriteLine(gameObject.name + " pos4:" + position4);
        }
        public override void OnDraw()
        {
            gameObject.game._spriteBatch.Draw(texture, position, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
			gameObject.game._spriteBatch.Draw(texture, position2, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
            gameObject.game._spriteBatch.Draw(texture, position3, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
            gameObject.game._spriteBatch.Draw(texture, position4, null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
            gameObject.game._spriteBatch.Draw(texture, position3 - new Vector2(0, texture.Height * scale), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
            gameObject.game._spriteBatch.Draw(texture, position4 - new Vector2(0, texture.Height * scale), null, Color.White, 0, Vector2.Zero, scale, SpriteEffects.None, depth);
        }
    }
}

