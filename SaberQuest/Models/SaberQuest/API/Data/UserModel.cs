using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.API.Data
{
	public class UserModel
    {
		public UserInfo UserInfo { get; set; }
		public StatsModel Stats { get; set; }
		public TodayModel Today { get; set; }
	}

	public class ImagesModel
	{
		public string Avatar { get; set; }
		public object Banner { get; set; }
		public object Border { get; set; }
	}

	public class StatsModel
	{
		public int ChallengesCompleted { get; set; }
		public int Rank { get; set; }
		public int Qp { get; set; }
		public int Value { get; set; }
	}

	public class TodayModel
	{
		public int Diff { get; set; }
		public bool Completed { get; set; }
	}

	public class UserInfoModel
	{
		public string Id { get; set; }
		public string Username { get; set; }
		public ImagesModel Images { get; set; }
		public string Preference { get; set; }
		public object Patreon { get; set; }
		public object AutoComplete { get; set; }
	}
}
