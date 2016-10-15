using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UrbanDictCrawler
{
	public class UrbanDictController
	{
		public UrbanDictController()
		{
		}

		private const string apiBaseUrl 	= "http://api.urbandictionary.com";
		private const string apiQueryPath 	= "/v0/define?";
		private const string apiTermPath 	= "term=";
		private const string apiDefIdPath 	= "defid=";

		private HttpClient GetHttpClient()
		{
			var client = new HttpClient();
			client.BaseAddress = new Uri(apiBaseUrl);
			Debug.WriteLine("HTTPClient: REQUEST HTTP ADDR: " + apiBaseUrl);
			return client;
		}

		private async Task<T> ExecuteAsync<T>(string queryStr)
		{
			string CallUrl = apiBaseUrl + apiQueryPath + queryStr;

			Debug.WriteLine("ExecuteAsync: REQUEST HTTP ADDR: " + CallUrl);

			using (var client = GetHttpClient())
			{
				var json = await client.GetStringAsync(CallUrl);
				var result = JsonConvert.DeserializeObject<T>(json);
				return result;
			}
		}

		public async Task<UrbanDictObject> QueryByTerm(string queryStr)
		{
			var queryContent = string.Format(apiTermPath + Uri.EscapeDataString(queryStr));
			var result = await this.ExecuteAsync<UrbanDictObject>(queryContent);
			return result;
		}

		public async Task<UrbanDictObject> QueryById(string queryStr)
		{
			var queryContent = string.Format(apiDefIdPath + Uri.EscapeDataString(queryStr));
			var result = await this.ExecuteAsync<UrbanDictObject>(queryContent);
			return result;
		}
			
	}
}
