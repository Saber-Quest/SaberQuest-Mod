using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.Websocket.Types
{
	internal class GambleModel
	{
		public string UserId { get; set; }
		public string ItemWon { get; set; }
		public string Rarity { get; set; }
	}
}
