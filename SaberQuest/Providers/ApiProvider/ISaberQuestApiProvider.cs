using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.API.Data.Deals;
using SaberQuest.Models.SaberQuest.Web;
using System;
using System.Collections.Generic;

namespace SaberQuest.Providers.ApiProvider
{
    internal interface ISaberQuestApiProvider
    {
        public void GetDailyChallenges(Action<ChallengeSetModel> callback, Action<ErrorResponseModel> errorCallback);
        public void GetShopItems(Action<DealSetModel> callback, Action<ErrorResponseModel> errorCallback);
        public void GetAllItems(Action<List<ItemModel>> callback, Action<ErrorResponseModel> errorCallback);
		//blocks because lazy
		public UserModel GetUser(int user, Action<ErrorResponseModel> errorCallback);
	}
}
