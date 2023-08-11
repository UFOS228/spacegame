using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogametest.Components
{
    public class PhysicsComponent : Component
    {
        public delegate void OnCollide(CollisionComponent colide);
        public event OnCollide OnCollideEvent;
        //TODO public float mass = 10f;
        public Vector2 velocity = Vector2.Zero;
        public float bounceFactor = 0.2f;
        public float friction = 0.2f;
        public List<object> ignore = new List<object>();

        private CollisionComponent collision;
        public override void Init()
        {
            base.Init();
            OnCollideEvent += OnColliding;
            GetComponent(out collision);
        }
        public override void Update()
        {
            base.Update();
            if (velocity == Vector2.Zero) return;
            CollisionCheck();
            //Friction
            velocity = GameManager.MoveTowards(velocity, Vector2.Zero, friction);
        }
        public void CollisionCheck()
        {
            for (int i = 0; i < ObjectManager.objectsOnMap.Count; i++)
            {
                if (ObjectManager.objectsOnMap[i] == gameObject) continue;
                if (ignore.ToArray().Contains(ObjectManager.objectsOnMap[i])) continue;
                if (ObjectManager.objectsOnMap[i].TryGetComponent(out CollisionComponent collisionComponent))
                {
                    if (collision.IsTouchingRight(collisionComponent.collider, (int) velocity.X) || collision.IsTouchingLeft(collisionComponent.collider, (int)velocity.X))
                    {
                        if (ObjectManager.objectsOnMap[i].TryGetComponent(out PhysicsComponent physics))
                        {
                            physics.velocity.X = velocity.X * bounceFactor;
                            velocity.X = 0;
                            OnCollideEvent(collisionComponent);
                        }
                        else
                        {
                            velocity.X = -velocity.X * 0.001f;
                            OnCollideEvent(collisionComponent);
                        }
                    }
                    if (collision.IsTouchingTop(collisionComponent.collider, (int)velocity.Y) || collision.IsTouchingBottom(collisionComponent.collider, (int)velocity.Y))
                    {
                        if (ObjectManager.objectsOnMap[i].TryGetComponent(out PhysicsComponent physics))
                        {
                            physics.velocity.Y = velocity.Y * bounceFactor;
                            velocity.Y = 0;
                            OnCollideEvent(collisionComponent);
                        }
                        else
                        {
                            velocity.Y = -velocity.Y * 0.001f;
                            OnCollideEvent(collisionComponent);
                        }
                    }
                }
            }
            gameObject.position += velocity;
        }
        public void AddForce(Vector2 force)
        {
            velocity += force;
        }
        public void OnColliding(CollisionComponent collision)
        {

        }
    }
}
