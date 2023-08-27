using System;
using System.IO;
using monogame_test;

namespace spacegame
{
	public enum ContentType {Textures = 0, Audio = 1, Fonts = 2}
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
                    adding = "Audio/";
                    break;
                case ContentType.Fonts:
					adding = "Fonts/";
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
			new ContentField("Tiles/lattice", ContentType.Textures),
			new ContentField("Tiles/plating", ContentType.Textures),
			new ContentField("Tiles/reinforced", ContentType.Textures),
			new ContentField("Structures/full", ContentType.Textures),
			new ContentField("UI/arrowUp", ContentType.Textures),
			new ContentField("UI/hand_l", ContentType.Textures),
			new ContentField("UI/hand_r", ContentType.Textures),
			new ContentField("UI/head", ContentType.Textures),
			new ContentField("UI/pocket", ContentType.Textures),
			new ContentField("UI/shoes", ContentType.Textures),
			new ContentField("UI/suit", ContentType.Textures),
			new ContentField("UI/uniform", ContentType.Textures),
			new ContentField("UI/SlotBackground", ContentType.Textures),
			new ContentField("Item/Wearable/greyUniform", ContentType.Textures),
			new ContentField("Item/skub", ContentType.Textures),
			new ContentField("floor1", ContentType.Audio),
            new ContentField("floor2", ContentType.Audio),
			new ContentField("floor3", ContentType.Audio),
			new ContentField("floor4", ContentType.Audio),
			new ContentField("floor5", ContentType.Audio),
        };
		private static Game1 game;
		public static void ContentInit(Game1 gamee)
		{
			//foreach (var contentField in contentFields)
   //         {
			//	contentField.value = game.Content.Load<object>(contentField.path);
   //         }
			game = gamee;
		}

		public static T Load<T>(string path)
		{
			//foreach (var contentField in contentFields)
			//{
			//	//Console.WriteLine(contentField.path);
			//	if (contentField.path == path)
   //             {
			//		return (T) contentField.value;
			//	}
			//}
			////Console.WriteLine("   " + path);
			//throw new NullReferenceException();
			return game.Content.Load<T>(path);
		}
        public static T Load<T>(string fileName, ContentType contentType)
        {
			return Load<T>(ContentField.GetPathByType(fileName, contentType));
        }
		public static T[] LoadFilesByNumbers<T>(string filenamesWithoutNumber, int startnumber, ContentType contentType, int count)
		{
			List<T> values = new List<T>();
			for (int i = startnumber; i < count; i++)
			{
				values.Add(Load<T>(filenamesWithoutNumber + i, contentType));
			}
			return values.ToArray();
		}
    }
}

