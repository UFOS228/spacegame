using System;
using System.IO;
using monogame_test;

namespace monogametest
{
	public enum ContentType {Textures = 0, Audio = 1}
	public class ContentField
	{
		public ContentField(string pathToFile)
		{
			path = pathToFile;
		}
        public ContentField(string fileName, ContentType contentType)
        {
			path = GetPathByType(fileName, contentType);
        }
        public string path;
		public object value;

		public static string GetPathByType(string fileName, ContentType contentType)
		{
            string adding = "";
            switch (contentType)
            {
                case ContentType.Textures:
                    adding = "Textures/";
                    break;
                case ContentType.Audio:
                    break;
                default:
                    break;
            }
            return adding + fileName;
        }
    }
    public static class MyContentManager
	{
		private static List<ContentField> contentFields = new List<ContentField>()
		{
			new ContentField("shuttlewhite"),
			new ContentField("ball"),
			new ContentField("sceleton", ContentType.Textures),
			new ContentField("land", ContentType.Textures),
			new ContentField("Parallaxes/layer1", ContentType.Textures),
			new ContentField("Parallaxes/AspidParallaxNeb", ContentType.Textures),
        };
		public static void ContentInit(Game1 game)
		{
			foreach (var contentField in contentFields)
            {
				contentField.value = game.Content.Load<object>(contentField.path);
            }
		}

		public static T Load<T>(string path)
		{
			foreach (var contentField in contentFields)
			{
				//Console.WriteLine(contentField.path);
				if (contentField.path == path)
                {
					return (T) contentField.value;
				}
			}
			//Console.WriteLine("   " + path);
			throw new NullReferenceException();
		}
        public static T Load<T>(string fileName, ContentType contentType)
        {
			return Load<T>(ContentField.GetPathByType(fileName, contentType));
        }
    }
}

