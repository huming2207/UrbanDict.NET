using System;
using UrbanDictCrawler;

namespace ConsoleTester
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Urban Dictionary DotNET Client");
			Console.WriteLine("By Jackson Ming Hu @ RMIT University 2016");
			Console.Write("Enter a word or phrase you would like to search: ");
			string queryStr = Console.ReadLine();

			UrbanDictController crawler = new UrbanDictController();
			var result = crawler.QueryByTerm(queryStr).Result; // Add the ".Result" since we are not using async here.

			Console.WriteLine("\n\nYou've got " + result.ItemList.Count.ToString() + " results.\n");

			for (int i = 0; i < result.ItemList.Count; i++)
			{
				Console.WriteLine("*****************************************");
				Console.WriteLine("The #" + i.ToString() + " author is: " + result.ItemList[i].Author);
				Console.WriteLine("The #" + i.ToString() + " result is: \n\n" + result.ItemList[i].Definition);
				Console.WriteLine("*****************************************\n\n");
			}

		}
	}
}
