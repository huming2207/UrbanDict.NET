﻿using System.Collections.Generic;
using Newtonsoft.Json;

namespace UrbanDictCrawler
{
	[JsonObject]
	public class UrbanDictObject
	{
		[JsonProperty(PropertyName = "tags")]
		public List<string> Tags { get; set; }

		[JsonProperty(PropertyName = "result_type")]
		public string ResultType { get; set; }

		[JsonProperty(PropertyName = "list")]
		public List<UrbanDictList> ItemList { get; set; }

		[JsonProperty(PropertyName = "sounds")]
		public List<string> Sounds { get; set; }
	}
}
