using System;
using monogametest.Components;

namespace monogametest.Prefabs
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
            };
        }
    }
}