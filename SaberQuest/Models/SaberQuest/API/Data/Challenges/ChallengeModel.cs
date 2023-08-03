using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.API.Data.Challenges
{
	public class ChallengeModel
    {
		public string Difficulty { get; set; }
		public string Type { get; set; }
		public string ShortName { get; set; }
		public string Description { get; set; }
		public string Value { get; set; }
	}
}
