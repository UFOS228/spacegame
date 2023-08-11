using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogametest.Components
{
    public class ProjectileComponent : Component
    {
        public float damage;
        public SoundEffectCollection hitSound;
        public RandomGradient hitPitch;
        public SoundEffectCollection missSound;
        public RandomGradient missPitch;

        public override void Init()
        {
            base.Init();
            GetComponent(out PhysicsComponent physics);
            physics.OnCollideEvent += OnCollide;
        }
        public virtual void OnCollide(CollisionComponent collisionComponent)
        {
            if (collisionComponent.TryGetComponent(out PlayerComponent player))
            {
                player.health -= damage;
                if (hitSound != null)
                    hitSound.Play3DRandom(gameObject.position, 1, hitPitch);
            }
            else
            {
                if (missSound != null)
                    missSound.Play3DRandom(gameObject.position, 1, missPitch);
            }
            ObjectManager.Destroy(gameObject);
        }
    }
}
