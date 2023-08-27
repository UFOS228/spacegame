using System;
namespace spacegame.Components
{
	public class DelayComponent : Component
	{
        /// <summary>
        /// A delay constructor.
        /// </summary>
        /// <param name="time">A delay time in seconds.</param>
        public DelayComponent(float time)
        {
            delayTime = time;
        }
        public float delayTime = 1f;
        public delegate void DelayEvent();
        public event DelayEvent OnDelay;

        public bool isCooldowned = true;
        public async override void Update()
        {
            if (isCooldowned)
            {
                await Task.Run(() =>
                {
                    isCooldowned = false;
                    Thread.Sleep((int)(delayTime * 1000));
                    OnDelay();
                    isCooldowned = true;
                });
            }
        }
    }
}

