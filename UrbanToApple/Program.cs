using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using UrbanDictCrawler;

namespace UrbanToApple
{
	class MainClass
	{
		// List for storaging keywords
		private Dictionary<string, bool> keywordList = new Dictionary<string, bool>();

		// UrbanGrabber stuff
		private UrbanGrabber ubGrabber = new UrbanGrabber(string.Empty);

		// List for storaging terms which have been recorded
		private List<string> finishedQueries = new List<string>();

		public static void Main(string[] args)
		{
			Console.WriteLine("Urban2Apple");
			Console.WriteLine("Urban Dictionary to Apple Mac OSX Dictionary Converter");
			Console.WriteLine("By Jackson Ming Hu @ RMIT University, 2016");
			Console.WriteLine("=========================================================================");
			Console.WriteLine("This project is used for educational & offline caching proposes only. \n" + 
			                  "Please ALWAYS RESPECT the original and Urban Dictionary!\n" + 
			                  "...and ALWAYS RESPECT/FOLLOW THE RELATED LAW!");
			Console.WriteLine("=========================================================================");

			MainClass runMain = new MainClass();
			runMain.startCrawler();
		}

		// The results from this method grows very fast,
		// between O(2n) to O(10n), so mostly speaking we don't need to worry about running out cache.
		private async Task grabKeywords(string defaultTerm = "test")
		{
			var crawler = new UrbanDictController();
			var result = await crawler.QueryByTerm(defaultTerm);
			foreach (string keyword in result.Tags)
			{
				Console.WriteLine("[INFO] Grabbing keywords: " + keyword);
				if (keywordList.ContainsKey(keyword))
				{
					keywordList.Add(keyword, false);
				}
				else
				{
					Console.WriteLine("[INFO] Keyword " + keyword + " already cached.");
				}
			}

		}

		private async Task grabContent()
		{
			foreach (KeyValuePair<string, bool> queryPair in keywordList)
			{
				// Only grab the content if the list is valid (not yet been crawled)
				if (queryPair.Value)
				{
					Console.WriteLine("[INFO] Grabbing contents for keyword: " + queryPair.Key);
					var crawler = new UrbanDictController();
					var result = await crawler.QueryByTerm(queryPair.Key);
					ubGrabber.ContentToXml(result, queryPair.Key);

					// Refresh the value to "false" 
					//  to indicate the keyword item ifself has been crawled.
					keywordList.Remove(queryPair.Key);
					keywordList.Add(queryPair.Key, false);
				}
				else
				{ 
					Console.WriteLine("[INFO] Content for keyword " + queryPair.Key + " has been crawled.");
				}

			}
		}

		private void startCrawler(string firstEntry = "test")
		{
			Task keywordGrabber = grabKeywords(firstEntry);
			Task contentGrabber = grabContent();
			ubGrabber.Init();
			keywordGrabber.Start();
			contentGrabber.Start();
		}



	}
}
