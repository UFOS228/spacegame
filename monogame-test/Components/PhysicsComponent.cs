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
        public Vector2 velocity = Vector2.Zero;
        public float friction = 0.2f;
        public override void Update()
        {
            base.Update();
            if (velocity == Vector2.Zero) return;
            gameObject.position += velocity;
            velocity = GameManager.MoveTowards(velocity, Vector2.Zero, friction);
        }
        public void AddForce(Vector2 force)
        {
            velocity += force;
        }
    }
}
