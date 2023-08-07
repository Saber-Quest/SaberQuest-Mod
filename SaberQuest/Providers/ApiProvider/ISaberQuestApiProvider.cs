using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Providers.ApiProvider
{
	internal interface ISaberQuestApiProvider
	{
		public void GetDailyChallenges(Action<ChallengeSetModel> callback, Action<ErrorResponseModel> errorCallback);
	}
}
