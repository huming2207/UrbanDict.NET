using Newtonsoft.Json;

namespace UrbanDictCrawler
{
	[JsonObject]
	public class UrbanDictList
	{
		[JsonProperty(PropertyName = "definition")]
		public string definition { get; set; }

		[JsonProperty(PropertyName = "permalink")]
		public string permalink { get; set; }

		[JsonProperty(PropertyName = "thumbs_up")]
		public int thumbs_up { get; set; }

		[JsonProperty(PropertyName = "author")]
		public string author { get; set; }

		[JsonProperty(PropertyName = "word")]
		public string word { get; set; }

		[JsonProperty(PropertyName = "defid")]
		public string defid { get; set; }

		[JsonProperty(PropertyName = "current_vote")]
		public string current_vote { get; set; }

		[JsonProperty(PropertyName = "example")]
		public string example { get; set; }

		[JsonProperty(PropertyName = "thumbs_down")]
		public int thumbs_down { get; set; }
	}
}
