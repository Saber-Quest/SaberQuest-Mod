using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.Web;
using SiraUtil.Logging;
using SiraUtil.Web;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Providers.ApiProvider
{
	internal class MockSaberQuestApiProvider : ISaberQuestApiProvider
	{
		private readonly SiraLog _logger;

		private const string MockDailyChallenges = "{\r\n\t\"id\": \"daily\",\r\n\t\"name\": \"Daily Challenges\",\r\n\t\"type\": \"xAccuracyStars\",\r\n\t\"shortName\": \"PP W/ Acc\",\r\n\t\"description\": \"Get PP on either service while keeping accuracy\",\r\n\t\"image\": \"\",\r\n\t\"resetTime\": \"2023-08-07T20:11:53.053Z\",\r\n\t\"difficulties\": [{\r\n\t\t\"challengeSet\": \"daily\",\r\n\t\t\"name\": \"Easy\",\r\n\t\t\"value\": \"<line-height=70%><color=#FFAAAA>BL: 3 PP<br><color=#FFFF55>SS: 2 PP<br><color=#5555FF>ACC: 90%\"\r\n\t}, {\r\n\t\t\"challengeSet\": \"daily\",\r\n\t\t\"name\": \"Normal\",\r\n\t\t\"value\": \"<line-height=70%><color=#FFAAAA>BL: 5 PP<br><color=#FFFF55>SS: 4 PP<br><color=#5555FF>ACC: 92%\"\r\n\t}, {\r\n\t\t\"challengeSet\": \"daily\",\r\n\t\t\"name\": \"Hard\",\r\n\t\t\"value\": \"<line-height=70%><color=#FFAAAA>BL: 8 PP<br><color=#FFFF55>SS: 7 PP<br><color=#5555FF>ACC: 94.5%\"\r\n\t}, {\r\n\t\t\"challengeSet\": \"daily\",\r\n\t\t\"name\": \"Extreme\",\r\n\t\t\"value\": \"<line-height=70%><color=#FFAAAA>BL: 10 PP<br><color=#FFFF55>SS: 9 PP<br><color=#5555FF>ACC: 96.5%\"\r\n\t}]\r\n}";

		private MockSaberQuestApiProvider(SiraLog siraLog)
		{
			_logger = siraLog;
		}

		public void GetDailyChallenges(Action<ChallengeSetModel> callback, Action<ErrorResponseModel> errorCallback)
		{
			var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ChallengeSetModel>(MockDailyChallenges);
			callback(obj);
		}
	}
}