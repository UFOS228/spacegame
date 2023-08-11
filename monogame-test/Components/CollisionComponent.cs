using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogametest.Components
{
    public class CollisionComponent : Component
    {
        public CollisionComponent(Vector2 size)
        {
            this.size = size;
        }
        public Vector2 size = new Vector2(1);
        public Rectangle collider
        {
            get
            {
                return new Rectangle((int)gameObject.position.X + (int)game.cameraPosCenteredLerped.X - (int)(size.X / 2),
                    (int)gameObject.position.Y + (int)game.cameraPosCenteredLerped.Y - (int)(size.Y / 2), (int)size.X, (int)size.Y);
            }
        }

        public override void OnDraw()
        {
            base.OnDraw();
            if (game.isRenderColliders)
            {
                game._spriteBatch.Draw(MyContentManager.Load<Texture2D>("shuttlewhite"), collider, GameManager.EditAlpha(Color.Green, 100));
            }
        }
        public bool IsTouchingLeft(Rectangle anotherCollider, int velocityX)
        {
            return collider.Right + velocityX > anotherCollider.Left &&
                collider.Bottom > anotherCollider.Top &&
                collider.Left < anotherCollider.Left &&
                collider.Top < anotherCollider.Bottom;
        }
        public bool IsTouchingRight(Rectangle anotherCollider, int velocityX)
        {
            return collider.Left + velocityX  < anotherCollider.Right &&
                collider.Bottom > anotherCollider.Top &&
                collider.Right > anotherCollider.Left &&
                collider.Top < anotherCollider.Bottom;
        }
        public bool IsTouchingTop(Rectangle anotherCollider, int velocityY)
        {
            return collider.Bottom + velocityY > anotherCollider.Top &&
                collider.Top < anotherCollider.Top &&
                collider.Right > anotherCollider.Left &&
                collider.Left < anotherCollider.Right;
        }
        public bool IsTouchingBottom(Rectangle anotherCollider, int velocityY)
        {
            return collider.Top + velocityY < anotherCollider.Bottom &&
                collider.Bottom > anotherCollider.Bottom &&
                collider.Right > anotherCollider.Left &&
                collider.Left < anotherCollider.Right;
        }
    }
}
