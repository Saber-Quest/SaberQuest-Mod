using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.API.Data.Deals
{
	internal class DealSetModel
	{
		public string Message { get; set; }
		public List<DealModel> Deals { get; set; }
		public SpecialOfferModel SpecialOffer { get; set; }
	}
}
