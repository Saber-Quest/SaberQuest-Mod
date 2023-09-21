using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.API.Data.Challenges
{
    public class ChallengeSetModel
    {
        public string Name { get; set; }
		public string Description { get; set; }
		public string Type { get; set; }
		public List<ChallengeModel> Difficulties { get; set; }
		public string Image { get; set; }
        public DateTime resetTime { get; set; }
    }
}
