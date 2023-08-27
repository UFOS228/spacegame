using System;
using System.Xml;
using System.Text.Json;
using System.Text;
using spacegame;
using spacegame.Components;

namespace spacegame
{
	public class UfDataSection
	{
		public UfDataSection(string k, string val)
		{
			key = k;
			value = val;
		}
		public string key, value;
	}
	public static class ConfigManager
	{
		//private static UfDataSection[] configSections;

  //      public static void Init(ContentManager config)
		//{
		//	configSections = UfDataRead(config.Load<string>("config.ufdata"));
			
  //      }
		//public static UfDataSection[] UfDataRead(string ufdatatext)
		//{
  //          List<UfDataSection> data = new List<UfDataSection>();
  //          foreach (var section in ufdatatext.Split("\n"))
  //          {
  //              data.Add(new UfDataSection(section.Split(":")[0], section.Split(":")[1]));
  //          }
		//	return data.ToArray();
  //      }
		//public static string UfDataGetKey(string key, UfDataSection[] sections)
		//{
		//	foreach (var item in sections)
		//	{
		//		if (item.key.ToLower().Replace(' ', char.MinValue) == key.ToLower().Replace(' ', char.MinValue))
		//		{
		//			return item.value;
  //              }
		//	}
		//	throw new NullReferenceException();
		//}
		//public static string ConfigGetKey(string key)
		//{
		//	return UfDataGetKey(key, configSections);
		//}
	}
}

