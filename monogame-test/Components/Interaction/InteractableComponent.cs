using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace spacegame.Components
{
    public class InteractableComponent : Component
    {
        public int myInteractId = 0;

        public InteractableComponent(int myInteractId)
        {
            this.myInteractId = myInteractId;
        }
        public InteractableComponent()
        {
            myInteractId = 0;
        }
        public void OnInteract(PlayerComponent player, int interactId)
        {
            if (myInteractId == interactId || myInteractId < 0)
            {
                onInteract(player, interactId);
            }
        }
        public virtual void onInteract(PlayerComponent player, int interactId)
        { 

        }
    }
}
