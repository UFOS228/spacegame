using System;
namespace monogametest.Components
{
	public class ItemComponent : Component
	{
        public ItemComponent() { }
        public ItemComponent(Texture2D itemtext, float scale)
        {
            textureInInv = itemtext;
            inInvScale = scale;
        }
        public Texture2D textureInInv;
        public float inInvScale = 1;
        public virtual void OnPickedUp(InventoryComponent inv)
		{
			
		}
        public virtual void OnDropped(InventoryComponent inv)
        {

        }
        public virtual void OnUse(InventoryComponent inv)
        {
            if (TryGetComponent(out InteractableComponent interactableComponent))
            {
                inv.GetComponent(out PlayerComponent player);
                gameObject.GetComponents(out InteractableComponent[] comps);
                foreach (var item in comps)
                {
                    item.OnInteract(player);
                }
            }
        }
        public virtual void OnAltUse(InventoryComponent inv)
        {
            if (TryGetComponent(out InteractableComponent interactableComponent))
            {
                inv.GetComponent(out PlayerComponent player);
                gameObject.GetComponents(out InteractableComponent[] comps);
                foreach (var item in comps)
                {
                    item.OnInteractAlt(player);
                }
            }
        }
    }
}

