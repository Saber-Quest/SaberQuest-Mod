using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.API.Data.Deals
{
	internal class DealModel
	{
		public string Id { get; set; }
		public int Price { get; set; }
		public string Rarity { get; set; }
		//Currently selling
		public int Value { get; set; }
	}
}
