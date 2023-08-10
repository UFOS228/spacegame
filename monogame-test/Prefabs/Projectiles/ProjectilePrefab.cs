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
                new LifeTimeComponent(2),
            };
        }
    }
}
