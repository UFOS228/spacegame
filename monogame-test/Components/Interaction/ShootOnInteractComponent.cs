using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace monogametest.Components
{
    public class ShootOnInteractComponent : InteractableComponent
    {
        private GunComponent gun;
        public override void Init()
        {
            base.Init();
            GetComponent(out gun);
        }
        public override void OnInteract(PlayerComponent player)
        {
            base.OnInteract(player);
            player.GetComponent(out PlayerInteraction playerInteraction);
            if (playerInteraction.dir == Vector2.Zero) return;
            gun.Shoot(player.gameObject.position, MathF.Atan2(playerInteraction.dir.Y, playerInteraction.dir.X));
        }
    }
}
