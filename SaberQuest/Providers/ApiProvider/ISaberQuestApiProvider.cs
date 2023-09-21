using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.API.Data.Deals;
using SaberQuest.Models.SaberQuest.Web;
using System;

namespace SaberQuest.Providers.ApiProvider
{
    internal interface ISaberQuestApiProvider
    {
        public void GetDailyChallenges(Action<ChallengeSetModel> callback, Action<ErrorResponseModel> errorCallback);
        public void GetShopItems(Action<DealSetModel> callback, Action<ErrorResponseModel> errorCallback);
    }
}
