using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.API.Data.Crafting;
using SaberQuest.Models.SaberQuest.API.Data.Deals;
using SaberQuest.Models.SaberQuest.Web;
using System;
using System.Collections.Generic;

namespace SaberQuest.Providers.ApiProvider
{
    internal interface ISaberQuestApiProvider
    {
        public void ProvideToken(string token);
        public void GetDailyChallenges(Action<ChallengeSetModel> callback, Action<ErrorResponseModel> errorCallback);
		public void GetShopItems(Action<DealSetModel> callback, Action<ErrorResponseModel> errorCallback);
        public void GetAllItems(Action<List<ItemModel>> callback, Action<ErrorResponseModel> errorCallback);
        public void GetAllRecipes(Action<List<RecipeModel>> callback, Action<ErrorResponseModel> errorCallback);
		//blocks because lazy
		public UserModel GetUser(long user, Action<ErrorResponseModel> errorCallback);
	}
}
