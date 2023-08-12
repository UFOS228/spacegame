using System;
using System.Diagnostics;
using monogame_test;

namespace monogametest.Components
{
	public class PlayerComponent : PhysicsComponent
    {
        public bool isMoving = false;
        public float health = 100;
		//public float movementCooldown = 0.001f;
        public int movementAmount = 5;
        public float shiftMultiplier = 0.5f;
        public float stepSndCooldown = 0.5f;
        public bool isCameraLocksOnPlayer = false;
        public SoundEffectCollection stepSounds = new SoundEffectCollection();
        public PlayerIndex playerIndex;
        private bool isMoveCooldowns = false;

        private PhysicsComponent physicsComponent;

        public PlayerComponent(PlayerIndex index)
        {
            playerIndex = index;
        }
        public override void Init()
        {
            base.Init();
            //gameObject.AddComponent(new DelayComponent(stepSndCooldown));
            stepSounds.sounds = MyContentManager.LoadFilesByNumbers<SoundEffect>("floor", 1, ContentType.Audio, 5).ToList();
            gameObject.GetComponent(out DelayComponent comp);
            comp.OnDelay += OnStepSndCooldowned;
            comp.delayTime = stepSndCooldown;
            Game1.instance.cameraPointsCenter.Add(gameObject);
            GetComponent(out physicsComponent);
        }
        public override void Update()
		{
            isMoving = false;

            if (Keyboard.GetState().IsKeyDown(Keys.W) && playerIndex == PlayerIndex.One)
			{
				PlMove(new Vector2(physicsComponent.velocity.X, -movementAmount * (Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? shiftMultiplier : 1)));
			}
            else if(Keyboard.GetState().IsKeyDown(Keys.S) && playerIndex == PlayerIndex.One)

            {
                PlMove(new Vector2(physicsComponent.velocity.X, movementAmount * (Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? shiftMultiplier : 1)));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D) && playerIndex == PlayerIndex.One)
            {
                PlMove(new Vector2(movementAmount * (Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? shiftMultiplier : 1), physicsComponent.velocity.Y));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A) && playerIndex == PlayerIndex.One)
            {
                PlMove(new Vector2(-movementAmount * (Keyboard.GetState().IsKeyDown(Keys.LeftShift) ? shiftMultiplier : 1), physicsComponent.velocity.Y));
            }

            if ((MathF.Round(GamePad.GetState(playerIndex - 1).ThumbSticks.Left.X, 1) != 0f || MathF.Round(GamePad.GetState(playerIndex - 1).ThumbSticks.Left.Y, 1) != 0f) && playerIndex != PlayerIndex.One)
            {
                var vector = GamePad.GetState(playerIndex - 1).ThumbSticks.Left;
                vector.Y = -vector.Y;
                PlMove(Vector2.Round(vector * movementAmount));
            }
            base.Update();

        }
        public void PlMove(Vector2 direction, bool isCooldowns = false)
        {
            isMoving = true;
			if (!isMoveCooldowns)
			{
                //gameObject.position += direction;
                physicsComponent.velocity = direction;
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

