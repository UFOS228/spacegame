using monogametest.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogametest.Prefabs
{
    public class ProjectilePrefab : GameObject
    {
        public override void Init()
        {
            base.Init();
            name = "Bullet";
            components = new Component[]
            {
                new RendererComponent(MyContentManager.Load<Texture2D>("Projectiles/bullet", ContentType.Textures), Color.White, 0.6f),
                new PhysicsComponent(),
                new CollisionComponent(new Vector2(10,4)),
                new LifeTimeComponent(2),
                new ProjectileComponent() {
                    hitSound = new(MyContentManager.LoadFilesByNumbers<SoundEffect>("Weapon/Hits/bullet_meat", 1, ContentType.Audio, 4)),
                    hitPitch = new RandomGradient(-0.2f, 0.2f),
                    missSound = new(MyContentManager.LoadFilesByNumbers<SoundEffect>("Weapon/Hits/ric", 1, ContentType.Audio, 5)),
                    missPitch = new RandomGradient(-0.2f, 0.1f),
                },
            };
        }
    }
}
