using System;
using monogame_test;

namespace monogametest
{
	public class ContentField
	{
		public ContentField(string pathToFile, Type typeOfThisFile)
		{
			path = pathToFile;
			type = typeOfThisFile;
		}
		public string path;
		public Type type;
		public object value;
	}
	public static class MyContentManager
	{
		private static List<ContentField> contentFields = new List<ContentField>()
		{
			new ContentField("shuttlewhite", typeof(Texture2D)),
		};
		public static void ContentInit(Game1 game)
		{
			foreach (var contentField in contentFields)
			{
				contentField.value = LoadInBase(game, contentField.type, contentField.path);
            }
		}
		private static T LoadInBase<T>(Game1 game, T type, string path)
		{
            return game.Content.Load <T> (path);
        }

		public static T Load<T>(string path)
		{
			foreach (var contentField in contentFields)
			{
				if (contentField.path == path)
				{
					return (T) contentField.value;
				}
			}
			throw new NullReferenceException();
		}
	}
}

