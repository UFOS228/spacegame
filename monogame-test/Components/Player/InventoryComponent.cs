using System;
using System.Diagnostics;
using spacegame.Prefabs;

namespace spacegame.Components
{
	public enum InventorySlotType {none = -1, leftHand = 0, rightHand = 1, suit = 2, pocket = 3, head = 4, shoes = 5}
	public class InventorySlot
	{
		public InventorySlot(Texture2D texture, Color color)
		{
			slotTexture = texture;
			slotTextureColor = color;
		}
		public InventorySlot(InventorySlotType type, Color? color = null)
		{
            slotTexture = type switch
            {
                InventorySlotType.leftHand => MyContentManager.Load<Texture2D>("UI/hand_l", ContentType.Textures),
                InventorySlotType.rightHand => MyContentManager.Load<Texture2D>("UI/hand_r", ContentType.Textures),
                InventorySlotType.suit => MyContentManager.Load<Texture2D>("UI/suit", ContentType.Textures),
                InventorySlotType.pocket => MyContentManager.Load<Texture2D>("UI/pocket", ContentType.Textures),
                InventorySlotType.head => MyContentManager.Load<Texture2D>("UI/head", ContentType.Textures),
                InventorySlotType.shoes => MyContentManager.Load<Texture2D>("UI/shoes", ContentType.Textures),
                InventorySlotType.none => MyContentManager.Load<Texture2D>("UI/SlotBackground", ContentType.Textures),
                _ => MyContentManager.Load<Texture2D>("UI/SlotBackground", ContentType.Textures),
            };
            slotTextureColor = color ?? Color.White;
            slotTextureAlpha = 150;
        }
        public Texture2D slotTexture;
        public byte slotTextureAlpha
        {
            get { return slotTextureColor.A; }
            set { slotTextureColor.A = value; }
        }
        public Color slotTextureColor = Color.White;
		public GameObject itemInSlot = null;
        public int slotId;
    }
    public class InventoryComponent : Component
	{
        public InventoryComponent(InventorySlot[] speedSlt, InventorySlot[] wearSlt, float yOffset = -128, float invScale = 1.5f)
        {
            speedSlots = speedSlt.ToList();
            wearSlots = wearSlt.ToList();
            baseOffsetY = yOffset;
            inventoryScale = invScale;
        }
        public InventoryComponent() { }
        public float baseOffsetY = -128;
        public float inventoryScale = 1.5f;
        public int inventoryXSize = 2;
        public List<InventorySlot> speedSlots = new List<InventorySlot>()
        {
            new InventorySlot(InventorySlotType.pocket) { itemInSlot = ObjectManager.SpawnObject(new TestItem(), Vector2.Zero)},
            new InventorySlot(InventorySlotType.pocket),
            new InventorySlot(InventorySlotType.pocket),
            new InventorySlot(InventorySlotType.pocket),
        };
		public List<InventorySlot> wearSlots = new List<InventorySlot>()
        {
            new InventorySlot(InventorySlotType.shoes, Color.Orange),
            new InventorySlot(InventorySlotType.suit, Color.Orange) { itemInSlot = ObjectManager.SpawnObject(new BasicUniformPrefab(), Vector2.Zero)},
            new InventorySlot(InventorySlotType.head, Color.Orange),
        };

        private int renderedSlots = 0;
        private int selectedSlot = 0;
        private int selectedSpeedSlot = 0;

        private bool isInvOpened = false;
        private PlayerComponent playerComponent
        {
            get
            {
                return GetComponent<PlayerComponent>();
            }
        }
        public override void Init()
        {
            base.Init();
            baseOffsetY = -(GetComponent<RendererComponent>().texture.Height * gameObject.scale.Y / inventoryScale) - 15;
        }
        public override void OnDraw()
        {
            base.OnDraw();
            #region {Slot select n interact}
            if (GameManager.IsKeyOrButtonDownOnce(playerComponent.playerIndex, Keys.C, Buttons.RightShoulder))
            {
                selectedSpeedSlot++;
            }
            if (GameManager.IsKeyOrButtonDownOnce(playerComponent.playerIndex, Keys.X, Buttons.LeftShoulder))
            {
                selectedSpeedSlot--;
            }
            if (selectedSpeedSlot >= speedSlots.Count)
            {
                selectedSpeedSlot = 0;
            }
            if (selectedSpeedSlot < 0)
            {
                selectedSpeedSlot = speedSlots.Count - 1;
            }

            //Interact
            if (GameManager.IsKeyOrButtonDownOnce(playerComponent.playerIndex, Keys.Space, Buttons.RightTrigger))
            {
                //if (GameManager.IsKeyOrButtonDown(playerComponent.playerIndex, Keys.Tab, Buttons.LeftTrigger))
                //{
                //    if (inventorySlots[selectedSlot].itemInSlot == null && speedSlots[selectedSpeedSlot].itemInSlot != null)
                //    {
                //        inventorySlots[selectedSlot].itemInSlot = speedSlots[selectedSpeedSlot].itemInSlot;
                //        speedSlots[selectedSpeedSlot].itemInSlot = null;
                //    }
                //    else if (inventorySlots[selectedSlot].itemInSlot != null && speedSlots[selectedSpeedSlot].itemInSlot == null)
                //    {
                //        speedSlots[selectedSpeedSlot].itemInSlot = inventorySlots[selectedSlot].itemInSlot;
                //        inventorySlots[selectedSlot].itemInSlot = null;
                //    }
                //}
                if (speedSlots[selectedSpeedSlot].itemInSlot != null)
                    speedSlots[selectedSpeedSlot].itemInSlot.GetComponent<ItemComponent>().OnUse(this);
            }
            if (GameManager.IsKeyOrButtonDownOnce(playerComponent.playerIndex, Keys.Z, Buttons.LeftTrigger))
            {
                if (speedSlots[selectedSpeedSlot].itemInSlot != null)
                    speedSlots[selectedSpeedSlot].itemInSlot.GetComponent<ItemComponent>().OnAltUse(this);
            }
            #endregion
            renderedSlots = 0;
            #region {SlotRendering}
            float offsetX = -(speedSlots[0].slotTexture.Width * inventoryScale) - (speedSlots[0].slotTexture.Width * inventoryScale * 0.5f);
            for (int i = 0; i < speedSlots.Count; i++)
            {
                //Slot draw
                game._spriteBatch.Draw(speedSlots[i].slotTexture, gameObject.position + game.cameraPosCenteredLerped + new Vector2(offsetX, baseOffsetY), null, i == selectedSpeedSlot ? Color.Cyan : speedSlots[i].slotTextureColor,
                    0, GameManager.OriginCenter(speedSlots[i].slotTexture), inventoryScale, SpriteEffects.None, 0.02f);
                //DrawItemInSlot
                if (speedSlots[i].itemInSlot != null)
                {
                    speedSlots[i].itemInSlot.GetComponent(out ItemComponent item);
                    game._spriteBatch.Draw(item.textureInInv,
                        gameObject.position + game.cameraPosCenteredLerped + new Vector2(offsetX, baseOffsetY), null, Color.White, 0,
                        GameManager.OriginCenter(speedSlots[i].slotTexture),
                        item.inInvScale * inventoryScale, SpriteEffects.None, 0.019f);
                }
                offsetX += speedSlots[i].slotTexture.Width * inventoryScale;
                speedSlots[i].slotId = renderedSlots;
                renderedSlots += 1;
            }
            //if (GameManager.IsKeyOrButtonDown(playerComponent.playerIndex, Keys.Tab, Buttons.LeftTrigger))
            //{
            //    offsetX = -(inventorySlots[0].slotTexture.Width * inventoryScale / 2);
            //    float offsetY = baseOffsetY - (speedSlots[0].slotTexture.Height * inventoryScale);
            //    for (int i = 0; i < inventorySlots.Count; i++)
            //    {
            //        game._spriteBatch.Draw(inventorySlots[i].slotTexture, gameObject.position + game.cameraPosCenteredLerped + new Vector2(offsetX, offsetY), null, i == selectedSlot ? Color.OrangeRed : inventorySlots[i].slotTextureColor,
            //            0, GameManager.OriginCenter(inventorySlots[i].slotTexture), inventoryScale, SpriteEffects.None, 0.02f);
            //        //DrawItemInSlot
            //        if (inventorySlots[i].itemInSlot != null)
            //            game._spriteBatch.Draw(inventorySlots[i].itemInSlot.GetComponent<ItemComponent>().textureInInv,
            //                gameObject.position + game.cameraPosCenteredLerped + new Vector2(offsetX, offsetY), null, Color.White, 0,
            //                GameManager.OriginCenter(inventorySlots[i].itemInSlot.GetComponent<ItemComponent>().textureInInv),
            //                inventorySlots[i].itemInSlot.GetComponent<ItemComponent>().inInvScale * inventoryScale, SpriteEffects.None, 0.019f);
            //        offsetX += inventorySlots[i].slotTexture.Width * inventoryScale;
            //        if ((i + 1) % inventoryXSize == 0)
            //        {
            //            offsetX = -(inventorySlots[0].slotTexture.Width * inventoryScale / 2);
            //            offsetY -= inventorySlots[i].slotTexture.Height * inventoryScale;
            //        }
            //        inventorySlots[i].slotId = renderedSlots;
            //        renderedSlots += 1;
            //    }
            //}
            #endregion
            renderedSlots = 0;
            #region {WearSlotsRendering}
            if (GameManager.IsKeyOrButtonDownOnce(playerComponent.playerIndex, Keys.Tab, Buttons.Y))
            {
                isInvOpened = !isInvOpened;
            }
            if (isInvOpened)
            {
                //offsetX = -((speedSlots[0].slotTexture.Width * inventoryScale / 2) + (wearSlots[0].slotTexture.Width * inventoryScale));
                offsetX = -(speedSlots[0].slotTexture.Width * inventoryScale) - (speedSlots[0].slotTexture.Width * inventoryScale * 0.5f);
                for (int i = 0; i < wearSlots.Count; i++)
                {
                    game._spriteBatch.Draw(wearSlots[i].slotTexture, gameObject.position + game.cameraPosCenteredLerped + new Vector2(offsetX, baseOffsetY * 2), null, wearSlots[i].slotTextureColor,
                        0, GameManager.OriginCenter(wearSlots[i].slotTexture), inventoryScale, SpriteEffects.None, 0.02f);
                    //DrawItemInSlot
                    if (wearSlots[i].itemInSlot != null)
                        game._spriteBatch.Draw(wearSlots[i].itemInSlot.GetComponent<ItemComponent>().textureInInv,
                            gameObject.position + game.cameraPosCenteredLerped + new Vector2(offsetX, baseOffsetY * 2), null, Color.White, 0,
                            GameManager.OriginCenter(wearSlots[i].itemInSlot.GetComponent<ItemComponent>().textureInInv),
                            wearSlots[i].itemInSlot.GetComponent<ItemComponent>().inInvScale * inventoryScale, SpriteEffects.None, 0.019f);
                    offsetX += wearSlots[i].slotTexture.Width * inventoryScale;
                    wearSlots[i].slotId = renderedSlots;
                    renderedSlots += 1;
                }
            }
            #endregion

        }
    }
}

