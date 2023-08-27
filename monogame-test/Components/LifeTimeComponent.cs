using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacegame.Components
{
    public class LifeTimeComponent : Component
    {
        public LifeTimeComponent(float time)
        {
            lifeTime = time;
        }
        public float lifeTime = 3;
        public async override void Init()
        {
            base.Init();
            await Task.Run(() =>
            {
                Thread.Sleep((int)(lifeTime * 1000));
                ObjectManager.Destroy(gameObject);
            });
        }
    }
}
