using System;
namespace monogametest.Components
{
	public class PlayerComponent : Component
	{
		public float movementCooldown = 10f;
        public int movementAmount = 100;
        public bool isCameraLocksOnPlayer = false;
		private bool isMoveCooldowns = false;
        public override void Init()
        {
            
        }
        public override void Update()
		{
            if (isCameraLocksOnPlayer) gameObject.game.cameraPosCentered = gameObject.position;
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
        public async void PlMove(Vector2 direction, bool isCooldowns = true)
        {
			if (!isMoveCooldowns)
			{
				gameObject.position += direction;
				if (isCooldowns)
				{
					await Task.Run(() =>
					{
						isMoveCooldowns = true;
						Thread.Sleep((int)(movementCooldown * 60));
						isMoveCooldowns = false;

					});
				}
			}
        }
    }
}

