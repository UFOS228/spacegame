using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogametest.Components
{
	public class PlayerInteraction : Component
	{
        public float arrowDistance = 50;
        public Texture2D arrowTexture;
        public float textureScale = 0.05f;
        public Color arrowColor = Color.Cyan;
        public float arrowDepth = 0.02f;
        private PlayerComponent playerComponent
        {
            get
            {
                return GetComponent<PlayerComponent>();
            }
        }
        private Vector2 dir;

        public override void Init()
        {
            base.Init();
            arrowTexture = MyContentManager.Load<Texture2D>("UI/arrowUp", ContentType.Textures);
            arrowColor.A = 150;
        }
        public override void OnDraw()
        {
            base.OnDraw();
            dir = Vector2.Zero;
            if (playerComponent.playerIndex == PlayerIndex.One)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Up))
                {
                    dir = new Vector2(dir.X, -1);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Down))

                {
                    dir = new Vector2(dir.X, 1);
                }

                if (Keyboard.GetState().IsKeyDown(Keys.Right))
                {
                    dir = new Vector2(1, dir.Y);
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                {
                    dir = new Vector2(-1, dir.Y);
                }
            }
            else
            {
                if (GamePad.GetState(playerComponent.playerIndex).ThumbSticks.Right.Length() != 0f)
                {
                    dir = new Vector2(GamePad.GetState(playerComponent.playerIndex).ThumbSticks.Right.X, GamePad.GetState(playerComponent.playerIndex).ThumbSticks.Right.Y);
                }
            }

            if (dir != Vector2.Zero)
                game._spriteBatch.Draw(arrowTexture, gameObject.position + (dir * arrowDistance) + game.cameraPosCenteredLerped, null,
                arrowColor, GameManager.DegreesToRadian(GetArrowRot()), new Vector2(arrowTexture.Width / 2, arrowTexture.Height / 2), textureScale, SpriteEffects.None, arrowDepth);
        }

        public float GetArrowRot()
        {
            if (dir == new Vector2(1, -1))
            {
                //вправо вверх
                return 45;
            }
            else if (dir == new Vector2(1, 0))
            {
                //вправо
                return 90;
            }
            else if (dir == new Vector2(1, 1))
            {
                //Вправо вниз
                return 135;
            }
            else if (dir == new Vector2(0, 1))
            {
                //вниз
                return 180;
            }
            else if (dir == new Vector2(-1, 1))
            {
                //Влево вниз
                return 225;
            }
            else if (dir == new Vector2(-1, 0))
            {
                //влево
                return 270;
            }
            else if (dir == new Vector2(-1, -1))
            {
                //Влево вверх
                return 315;
            }
            return 0;
        }
    }
}

