using spacegame;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacegame
{
    public static class DebugManager
    {
        public static void Draw()
        {
            

        }
        public static void Update()
        {
            if (Keyboard.GetState().IsKeyDown(Keys.F10))
            {
                Debug.WriteLine("------------Object manifest------------");
                for (int i = 0; i < ObjectManager.objectsOnMap.Count; i++)
                {
                    Debug.WriteLine("Object:" + ObjectManager.objectsOnMap[i].name + " Pos:" + ObjectManager.objectsOnMap[i].position);
                }
                Debug.WriteLine("---------------------------------------");
            }
        }
    }
}
