using System;
namespace spacegame.Components
{
	public class WearableComponent : ItemComponent
	{
        public WearableComponent(Texture2D itemTexture, Texture2D onChatacterTexture, float itemScale = 1, float onChScale = 1)
        {
            textureOnCharacter = onChatacterTexture;
            textureOnCharacterScale = onChScale;
            textureInInv = itemTexture;
            inInvScale = itemScale;
        }
        public Texture2D textureOnCharacter;
        public float textureOnCharacterScale = 1;
        public bool isWeared = false;
        public override void OnAltUse(InventoryComponent inv)
        {
            base.OnAltUse(inv);
            isWeared = !isWeared;
        }
        public virtual void OnWears(InventoryComponent inv)
		{
            if (!isWeared) return;
		}
        public virtual void OnWearsDraw(InventoryComponent inv)
        {
            if (!isWeared) return;
            inv.game._spriteBatch.Draw(textureOnCharacter, inv.gameObject.position + game.cameraPosCentered, null, Color.White, 0,
                new Vector2(textureOnCharacter.Width / 2, textureOnCharacter.Height / 2), textureOnCharacterScale, SpriteEffects.None, 0.9f);
        }
    }
}

