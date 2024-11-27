using System;
using spacegame.Components;

namespace spacegame.Prefabs
{
    public class TestItem : GameObject
    {
        public override void Init()
        {
            base.Init();
            name = "Base uniform";
            components = new Component[]
            {
                new ItemComponent(MyContentManager.Load<Texture2D>("Item/skub", ContentType.Textures), 1),
                new PlaySoundOnInteractComponent(new SoundEffectCollection(new List<SoundEffect>()
                {
                    MyContentManager.Load<SoundEffect>("Fun/skub", ContentType.Audio),
                    MyContentManager.Load<SoundEffect>("Fun/skub2", ContentType.Audio),
                    MyContentManager.Load<SoundEffect>("Fun/skub3", ContentType.Audio),
                }.ToArray()), 1, new RandomGradient(-0.1f, 0.9f), 1),
                new GunComponent(typeof(ProjectilePrefab), 10, GunType.Auto, 100f, 0.1f, 
                new SoundEffectCollection(new SoundEffect[] {MyContentManager.Load<SoundEffect>("Weapon/laser3", ContentType.Audio),})),
                new ShootOnInteractComponent(),
            };
        }
    }
}