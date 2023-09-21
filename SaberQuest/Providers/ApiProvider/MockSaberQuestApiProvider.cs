using Newtonsoft.Json;
using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.API.Data.Deals;
using SaberQuest.Models.SaberQuest.Web;
using SiraUtil.Logging;
using SiraUtil.Web;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Providers.ApiProvider
{
    internal class MockSaberQuestApiProvider : ISaberQuestApiProvider
    {
        private readonly SiraLog _logger;

        //TODO: Put this data somewhere, embedded resource json file?
        private const string MockDailyChallenges = "{\r\n\t\"id\": \"daily\",\r\n\t\"name\": \"Daily Challenges\",\r\n\t\"type\": \"xAccuracyStars\",\r\n\t\"shortName\": \"PP W/ Acc\",\r\n\t\"description\": \"Get PP on either service while keeping accuracy\",\r\n\t\"image\": \"\",\r\n\t\"resetTime\": \"2023-08-07T20:11:53.053Z\",\r\n\t\"difficulties\": [{\r\n\t\t\"challengeSet\": \"daily\",\r\n\t\t\"name\": \"Easy\",\r\n\t\t\"value\": \"<line-height=70%><color=#FFAAAA>BL: 3 PP<br><color=#FFFF55>SS: 2 PP<br><color=#5555FF>ACC: 90%\"\r\n\t}, {\r\n\t\t\"challengeSet\": \"daily\",\r\n\t\t\"name\": \"Normal\",\r\n\t\t\"value\": \"<line-height=70%><color=#FFAAAA>BL: 5 PP<br><color=#FFFF55>SS: 4 PP<br><color=#5555FF>ACC: 92%\"\r\n\t}, {\r\n\t\t\"challengeSet\": \"daily\",\r\n\t\t\"name\": \"Hard\",\r\n\t\t\"value\": \"<line-height=70%><color=#FFAAAA>BL: 8 PP<br><color=#FFFF55>SS: 7 PP<br><color=#5555FF>ACC: 94.5%\"\r\n\t}, {\r\n\t\t\"challengeSet\": \"daily\",\r\n\t\t\"name\": \"Extreme\",\r\n\t\t\"value\": \"<line-height=70%><color=#FFAAAA>BL: 10 PP<br><color=#FFFF55>SS: 9 PP<br><color=#5555FF>ACC: 96.5%\"\r\n\t}]\r\n}";
        private const string MockCurrentDeals = "{\r\n\t\"message\": \"Success\",\r\n\t\"deals\": [{\r\n\t\t\t\"id\": \"rsl\",\r\n\t\t\t\"price\": 35,\r\n\t\t\t\"rarity\": \"Rare\",\r\n\t\t\t\"imageURL\": \"https://saberquest.xyz/images/red_slider_icon.png\",\r\n\t\t\t\"name\": \"Red Slider\",\r\n\t\t\t\"value\": 20\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": \"rto\",\r\n\t\t\t\"price\": 30,\r\n\t\t\t\"rarity\": \"Rare\",\r\n\t\t\t\"imageURL\": \"https://saberquest.xyz/images/red_slider_icon.png\",\r\n\t\t\t\"name\": \"Red Tower\",\r\n\t\t\t\"value\": 16\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": \"rcp\",\r\n\t\t\t\"price\": 7,\r\n\t\t\t\"rarity\": \"Common\",\r\n\t\t\t\"imageURL\": \"https://saberquest.xyz/images/red_cube_pieces_icon.png\",\r\n\t\t\t\"name\": \"Red Note Pieces\",\r\n\t\t\t\"value\": 1\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": \"bn\",\r\n\t\t\t\"price\": 13,\r\n\t\t\t\"rarity\": \"Uncommon\",\r\n\t\t\t\"imageURL\": \"https://saberquest.xyz/images/blue_notes_icon.png\",\r\n\t\t\t\"name\": \"Blue Notes\",\r\n\t\t\t\"value\": 4\r\n\t\t},\r\n\t\t{\r\n\t\t\t\"id\": \"rs\",\r\n\t\t\t\"price\": 10,\r\n\t\t\t\"rarity\": \"Rare\",\r\n\t\t\t\"imageURL\": \"https://saberquest.xyz/images/red_saber_icon.png\",\r\n\t\t\t\"name\": \"Red Saber\",\r\n\t\t\t\"value\": 0\r\n\t\t}\r\n\t],\r\n\t\"specialOffer\": {\r\n\t\t\"title\": \"Special Offer!\",\r\n\t\t\"description\": \"You can spend 20qp to get a random item.\",\r\n\t\t\"endTime\": \"never lol\",\r\n\t\t\"imageURL\": \"https://saberquest.xyz/images/gambling.png\"\r\n\t}\r\n}";

        private MockSaberQuestApiProvider(SiraLog siraLog)
        {
            _logger = siraLog;
        }

        public void GetDailyChallenges(Action<ChallengeSetModel> callback, Action<ErrorResponseModel> errorCallback)
        {
            var obj = JsonConvert.DeserializeObject<ChallengeSetModel>(MockDailyChallenges);
            callback(obj);
        }

        public void GetShopItems(Action<DealSetModel> callback, Action<ErrorResponseModel> errorCallback)
        {
            var obj = JsonConvert.DeserializeObject<DealSetModel>(MockCurrentDeals);
            callback(obj);
        }

		public UserModel GetUser(int user, Action<ErrorResponseModel> errorCallback)
		{
			throw new NotImplementedException();
		}

		public void GetAllItems(Action<List<ItemModel>> callback, Action<ErrorResponseModel> errorCallback)
		{
			throw new NotImplementedException();
		}
	}
}