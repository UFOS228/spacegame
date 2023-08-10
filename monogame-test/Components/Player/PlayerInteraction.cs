using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace monogametest.Components
{
	public class PlayerInteraction : Component
	{
        public float arrowDistance = 50;
        public Texture2D arrowTexture;
        public float textureScale = 0.05f;
        public byte arrowAlpha = 200;
        public float arrowDepth = 0.02f;
        public Vector2 dir;
        private PlayerComponent playerComponent;

        public override void Init()
        {
            base.Init();
            GetComponent(out playerComponent);
            arrowTexture = MyContentManager.Load<Texture2D>("UI/arrowUp", ContentType.Textures);
        }
        public override void OnDraw()
        {
            base.OnDraw();
            dir = Vector2.Zero;
            if (playerComponent.playerIndex == PlayerIndex.One)
            {
                var a = Mouse.GetState().Position.ToVector2();
                a.X -= game._graphics.PreferredBackBufferWidth / 2;
                a.Y -= game._graphics.PreferredBackBufferHeight / 2;
                a /= game.zoom;
                //game._spriteBatch.Draw(MyContentManager.Load<Texture2D>("shuttlewhite"), a, Color.Red);
                if (Mouse.GetState().RightButton == ButtonState.Pressed)
                {
                    var angle = GameManager.LootAtPoint(gameObject.position + game.cameraPosCentered, a);
                    dir = new Vector2(MathF.Cos(angle), MathF.Sin(angle));
                }
                //if (Keyboard.GetState().IsKeyDown(Keys.Up))
                //{
                //    dir = new Vector2(dir.X, -1);
                //}
                //else if (Keyboard.GetState().IsKeyDown(Keys.Down))
                //{
                //    dir = new Vector2(dir.X, 1);
                //}

                //if (Keyboard.GetState().IsKeyDown(Keys.Right))
                //{
                //    dir = new Vector2(1, dir.Y);
                //}
                //else if (Keyboard.GetState().IsKeyDown(Keys.Left))
                //{
                //    dir = new Vector2(-1, dir.Y);
                //}
            }
            else
            {
                if (GamePad.GetState(playerComponent.playerIndex - 1).ThumbSticks.Right.Length() != 0f)
                {
                    dir = new Vector2(GamePad.GetState(playerComponent.playerIndex - 1).ThumbSticks.Right.X, -GamePad.GetState(playerComponent.playerIndex - 1).ThumbSticks.Right.Y);
                }
            }
            if (dir != Vector2.Zero)
            {
                var plColor = game.playerColors[(int)playerComponent.playerIndex];
                plColor.A = arrowAlpha;
                game._spriteBatch.Draw(arrowTexture, gameObject.position + (dir * arrowDistance) + game.cameraPosCenteredLerped, null,
                plColor, GetArrowRot(), new Vector2(arrowTexture.Width / 2, arrowTexture.Height / 2), textureScale, SpriteEffects.None, arrowDepth);
            }
        }

        public float GetArrowRot()
        {
            return GameManager.LootAtPoint(gameObject.position, gameObject.position + dir);
            //if (dir == new Vector2(1, -1))
            //{
            //    //вправо вверх
            //    return 45;
            //}
            //else if (dir == new Vector2(1, 0))
            //{
            //    //вправо
            //    return 90;
            //}
            //else if (dir == new Vector2(1, 1))
            //{
            //    //Вправо вниз
            //    return 135;
            //}
            //else if (dir == new Vector2(0, 1))
            //{
            //    //вниз
            //    return 180;
            //}
            //else if (dir == new Vector2(-1, 1))
            //{
            //    //Влево вниз
            //    return 225;
            //}
            //else if (dir == new Vector2(-1, 0))
            //{
            //    //влево
            //    return 270;
            //}
            //else if (dir == new Vector2(-1, -1))
            //{
            //    //Влево вверх
            //    return 315;
            //}
            //return 0;
        }
    }
}

