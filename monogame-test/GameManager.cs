using System;
using System.Linq;
using monogame_test;

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
			snd.Apply3D(listener, emitter);
            snd.Volume = volume.Get();
			snd.Pitch = pitch.Get();
            snd.Play();
        }
		public void PlayRandom(RandomGradient volume, RandomGradient pitch, RandomGradient pan)
		{
			Play(Game1.instance.random.Next(0, sounds.Count), volume, pitch, pan);
		}
        public void Play3DRandom(Vector2 listenerPos, Vector2 emitterPos, RandomGradient volume, RandomGradient pitch, RandomGradient pan)
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
			Console.WriteLine(min);
			Console.WriteLine(max);
			Console.WriteLine(Game1.instance.random.Next((int) (min * 100), (int) (max * 100)) / 100f);
			return Game1.instance.random.Next((int)(min * 100), (int)(max * 100)) / 100f;
		}
    }
	public static class GameManager
	{
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
	}
}

