using System;
using System.Collections.Generic;
using System.Xml;
using UrbanDictCrawler;

namespace UrbanToApple
{
	public class UrbanGrabber
	{
		private static string xmlPath = string.Empty;
		public UrbanGrabber(string path)
		{
			xmlPath = path;
		}

		private XmlHandler handler = new XmlHandler(xmlPath);

		public void Init()
		{
			handler.AddHeader();
		}

		public void ContentToXml(UrbanDictObject udObject, string word)
		{
			Dictionary<string, string> explanations = new Dictionary<string, string>();
			List<string> indexList = new List<string>();

			foreach (var item in udObject.ItemList)
			{
				explanations.Add(item.Definition, item.Example);
				indexList.Add(item.Word);
			}

			handler.AddEntry(word, word, indexList, explanations);
		}

		public void Flush()
		{
			handler.Flush();
		}

		public void Dispose()
		{
			handler.AddEnding();
			handler.Dispose();
		}
	}
}
