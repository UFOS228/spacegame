using System;
using System.Linq;
using Microsoft.Xna.Framework.Input;
using monogame_test;
using monogametest.Components;

namespace monogametest
{
	public class SoundEffectCollection
	{

		public List<SoundEffect> sounds;
		public void Play(int sndIndex, RandomGradient volume, RandomGradient pitch, RandomGradient pan)
		{
			var snd = sounds[sndIndex].CreateInstance();
			snd.Volume = volume.Get();
			snd.Pitch = pitch.Get();
			snd.Pan = pan.Get();
			snd.Play();
		}
        public void Play3D(int sndIndex, Vector2 listenerPos, Vector2 emitterPos,  RandomGradient volume, RandomGradient pitch)
        {
            var snd = sounds[sndIndex].CreateInstance();
			var listener = new AudioListener();
			var emitter = new AudioEmitter();
			listener.Position = new Vector3(listenerPos, 0);
			emitter.DopplerScale = 0;
			emitter.Position = new Vector3(emitterPos, 0);
            snd.Volume = volume.Get();
			snd.Pitch = pitch.Get();
            snd.Apply3D(listener, emitter);
            snd.Play();
        }
		public void PlayRandom(RandomGradient volume, RandomGradient pitch, RandomGradient pan)
		{
			Play(Game1.instance.random.Next(0, sounds.Count), volume, pitch, pan);
		}
        public void Play3DRandom(Vector2 listenerPos, Vector2 emitterPos, RandomGradient volume, RandomGradient pitch)
        {
            Play3D(Game1.instance.random.Next(0, sounds.Count), listenerPos, emitterPos, volume, pitch);
        }
    }
	public class RandomGradient
	{
		public static implicit operator RandomGradient(float value) { return new RandomGradient(value); }
		public static implicit operator RandomGradient(int value) { return new RandomGradient(value); }

        public RandomGradient(float minimum = 0, float maximum = 1)
		{
			max = maximum;
			min = minimum;
		}
        public RandomGradient(float value)
        {
            max = value;
            min = value;
        }
        public float min;
		public float max;

		public float Get()
		{
			if (min == max)
			{
				return min;
			}
			return Game1.instance.random.Next((int)(min * 100), (int)(max * 100)) / 100f;
		}
    }
	public static class GameManager
	{
		#region {StaticUtils}
		public static bool HasItemInArray<T>(T[] array, T value)
		{
			//foreach (T item in array)
			//{
			//	if (item.Equals(value))
			//	{
			//		return true;
			//	}
			//}
			//return false;
			return array.Contains(value);
		}
		public static T RandomInArray<T>(T[] array)
		{
			return array[Game1.instance.random.Next(0, array.Length)];
		}
		public static Vector2 GeometricalCenter(params Vector2[] points)
		{
			Vector2 sum = Vector2.Zero;
			foreach (var item in points)
			{
				sum += item;
			}
			return sum / points.Length;
		}
		public static float RadianToDegrees(float rad)
		{
			return (float)(double)(rad * 57.2958f);
        }
        public static float DegreesToRadian(float deg)
        {
            return (float)(double)(deg / 57.2958f);
        }
		public static string ArrayToString<T>(T[] array, string separator = ", ")
		{
			string result = "{";
			foreach (var item in array)
			{
				result += item.ToString() + separator;
			}
			result += "}";
			return result;
		}
		public static bool IsKeyOrButtonDown(PlayerIndex index, Keys key, Buttons button)
		{
			return (index == PlayerIndex.One) ? Keyboard.GetState().IsKeyDown(key)
				: GamePad.GetState(index).IsButtonDown(button);
        }
        public static bool IsKeyOrButtonUp(PlayerIndex index, Keys key, Buttons button)
        {
            return (index == PlayerIndex.One) ? Keyboard.GetState().IsKeyUp(key)
                : GamePad.GetState(index).IsButtonUp(button);
        }
        public static bool IsKeyOrButtonDownOnce(PlayerIndex index, Keys key, Buttons button)
        {
            return (index == PlayerIndex.One) ? GetOnceKeyDown(key)
                : GetOnceButtonDown(index, button);
        }
        public static Vector2 OriginCenterScale(Texture2D texture, Vector2? scale = null)
        {
            return new Vector2(texture.Width * (scale == null ? 1 : scale.Value.X) / 2, texture.Height * (scale == null ? 1 : scale.Value.Y) / 2);
        }
        public static Vector2 OriginCenter(Texture2D texture, float scale = 1)
        {
            return new Vector2(texture.Width * scale / 2, texture.Height * scale / 2);
        }
        #endregion
        private static List<Buttons> downedButtons2 = new();
		private static List<Buttons> downedButtons3 = new();
		private static List<Buttons> downedButtons4 = new();
        private static List<Keys> downedKeys = new();
        public static void OnDraw()
		{

		}
        public static void Update()
		{
            for (int i = 0; i < downedKeys.Count; i++)
			{
                if (Keyboard.GetState().IsKeyUp(downedKeys[i]))
				{
					downedKeys.Remove(downedKeys[i]);
				}
			}
			for (int i = 1; i < 4; i++)
			{
                GetPlayerPressedButtonsList((PlayerIndex) i, out List<Buttons> downedButtons);
                for (int i1 = 0; i1 < downedButtons.Count; i1++)
				{
                    if (GamePad.GetState((PlayerIndex) i).IsButtonDown(downedButtons[i1]))
					{
						downedButtons2.Remove(downedButtons[i1]);
					}
				}
			}
        }

		public static bool GetOnceKeyDown(Keys key)
		{
			if (!Keyboard.GetState().IsKeyDown(key)) return false;
			if (downedKeys.Contains(key))
			{
				return false;
			}
			downedKeys.Add(key);
			return true;
		}
		public static bool GetOnceButtonDown(PlayerIndex plIndex, Buttons button)
        {
			if (!GamePad.GetState(plIndex).IsButtonDown(button)) return false;
			List<Buttons> pressedButtons;
            GetPlayerPressedButtonsList(plIndex, out pressedButtons);
            if (pressedButtons.Contains(button))
            {
                return false;
            }
            pressedButtons.Add(button);
            return true;
        }
		private static void GetPlayerPressedButtonsList(PlayerIndex index, out List<Buttons> pressedButtons)
		{
            pressedButtons = index switch
            {
                PlayerIndex.One => throw new Exception("Player one does not have gamepad :("),
                PlayerIndex.Two => downedButtons2,
                PlayerIndex.Three => downedButtons3,
                PlayerIndex.Four => downedButtons4,
                _ => throw new NotImplementedException(),
            };
        }
    }
}

