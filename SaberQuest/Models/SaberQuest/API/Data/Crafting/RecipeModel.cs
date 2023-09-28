using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.API.Data.Crafting
{
	[JsonObject(NamingStrategyType = typeof(SnakeCaseNamingStrategy))]
	internal class RecipeModel
	{
		public string Item1Id { get; set; }
		public string Item2Id { get; set; }
		public string CraftedId { get; set; }
	}
}
