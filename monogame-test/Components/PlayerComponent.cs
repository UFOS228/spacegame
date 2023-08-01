using System;
namespace monogametest.Components
{
	public class PlayerComponent : Component
	{
        public bool isMoving = false;
		//public float movementCooldown = 0.001f;
        public int movementAmount = 10;
        public float stepSndCooldown = 0.5f;
        public bool isCameraLocksOnPlayer = true;
        public SoundEffectCollection stepSounds = new SoundEffectCollection();
        private bool isMoveCooldowns = false;
        public override void Init()
        {
            gameObject.AddComponent(new DelayComponent(stepSndCooldown));
            gameObject.GetComponent(out DelayComponent delComp);
            stepSounds.sounds = MyContentManager.LoadFilesByNumbers<SoundEffect>("floor", 1, ContentType.Audio, 5).ToList();
            delComp.OnDelay += OnStepSndCooldowned;
        }
        public override void Update()
		{
            isMoving = false;
			if (Keyboard.GetState().IsKeyDown(Keys.W))
			{
				PlMove(new Vector2(0, -movementAmount));
			}
            else if(Keyboard.GetState().IsKeyDown(Keys.S))

            {
                PlMove(new Vector2(0, movementAmount));
            }

            if (Keyboard.GetState().IsKeyDown(Keys.D))
            {
                PlMove(new Vector2(movementAmount, 0));
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                PlMove(new Vector2(-movementAmount, 0));
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
            stepSounds.PlayRandom(1, new RandomGradient(-0.6f, 0.3f),0f);
        }
    }
}

