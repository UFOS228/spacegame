using System;
using System.Diagnostics;
using monogame_test;

namespace monogametest.Components
{
	public class PlayerComponent : Component
	{
        public bool isMoving = false;
		//public float movementCooldown = 0.001f;
        public int movementAmount = 5;
        public float stepSndCooldown = 0.5f;
        public bool isCameraLocksOnPlayer = false;
        public SoundEffectCollection stepSounds = new SoundEffectCollection();
        public PlayerIndex playerIndex;
        private bool isMoveCooldowns = false;

        public PlayerComponent(PlayerIndex index)
        {
            playerIndex = index;
        }
        public override void Init()
        {
            //gameObject.AddComponent(new DelayComponent(stepSndCooldown));
            stepSounds.sounds = MyContentManager.LoadFilesByNumbers<SoundEffect>("floor", 1, ContentType.Audio, 5).ToList();
            gameObject.GetComponent(out DelayComponent comp);
            comp.OnDelay += OnStepSndCooldowned;
            comp.delayTime = stepSndCooldown;
            Game1.instance.cameraPointsCenter.Add(gameObject);
        }
        public override void Update()
		{
            isMoving = false;
			if (Keyboard.GetState().IsKeyDown(Keys.W) && playerIndex == PlayerIndex.One)
			{
				PlMove(new Vector2(0, -movementAmount));
			}
            else if(Keyboard.GetState().IsKeyDown(Keys.S) && playerIndex == PlayerIndex.One)

            {
                PlMove(new Vector2(0, movementAmount));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && playerIndex == PlayerIndex.One)
            {
                PlMove(new Vector2(movementAmount, 0));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && playerIndex == PlayerIndex.One)
            {
                PlMove(new Vector2(-movementAmount, 0));
            }

            if (GamePad.GetState(playerIndex - 1).ThumbSticks.Left.Length() != 0f && playerIndex != PlayerIndex.One)
            {
                PlMove(new Vector2(GamePad.GetState(playerIndex - 1).ThumbSticks.Left.X, -GamePad.GetState(playerIndex - 1).ThumbSticks.Left.Y) * movementAmount);
            }

        }
        public void PlMove(Vector2 direction, bool isCooldowns = false)
        {
            isMoving = true;
			if (!isMoveCooldowns)
			{
                gameObject.position += direction;
                if (isCameraLocksOnPlayer)
                {
                    gameObject.game.cameraPosCentered = new Vector2(-gameObject.position.X, -gameObject.position.Y);
                }
            }
        }
        public void OnStepSndCooldowned()
        {
            if (!isMoving) return;
            stepSounds.Play3DRandom(gameObject.position, 1, new RandomGradient(-0.6f, 0.3f));
        }
    }
}

