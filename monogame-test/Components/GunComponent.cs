using spacegame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace spacegame.Components
{
    public enum GunType
    {
        SemiAuto = 0,
        Auto = 1,
    }
    public class GunComponent : Component
    {
        public Type bullet;
        public RandomGradient damage = 10;
        public SoundEffectCollection shootSnd;
        public GunType gunType;
        public float projSpeed = 1f;
        public float shootCooldown = 0.5f;

        private bool isCooldowned = true;

        public GunComponent(Type bullet, RandomGradient damage, GunType gunType, float projSpeed, float shootCooldown, SoundEffectCollection? shootSnd = null)
        {
            this.bullet = bullet;
            this.damage = damage;
            this.gunType = gunType;
            this.projSpeed = projSpeed;
            this.shootCooldown = shootCooldown;
            this.shootSnd = shootSnd;
        }

        public async void Shoot(Vector2 pos, float rot)
        {
            if (!isCooldowned) return;
            ObjectManager.SpawnObject((GameObject) Activator.CreateInstance(bullet), pos, rot).GetComponent(out PhysicsComponent bulletPhysComp);
            bulletPhysComp.ignore.Add(gameObject);
            bulletPhysComp.velocity = new Vector2(MathF.Cos(rot), MathF.Sin(rot)) * projSpeed;
            bulletPhysComp.friction = 0;
            if (shootSnd != null) shootSnd.Play3DRandom(pos, 1, new RandomGradient(-0.2f, 0.1f));
            await Task.Run(() =>
            {
                isCooldowned = false;
                Thread.Sleep((int)(shootCooldown * 1000));
                isCooldowned = true;
            });
        }
    }
}
