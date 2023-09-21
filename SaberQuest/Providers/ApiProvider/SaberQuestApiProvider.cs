using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.API.Data.Deals;
using SaberQuest.Models.SaberQuest.Web;
using SiraUtil.Logging;
using SiraUtil.Web;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Providers.ApiProvider
{
    internal class SaberQuestApiProvider : ISaberQuestApiProvider
    {
        private const string BASE_URL = "http://localhost:5000/";
        private readonly SiraLog _logger;
        private readonly IHttpService _httpService;

        private SaberQuestApiProvider(SiraLog siraLog, IHttpService httpService)
        {
            _logger = siraLog;
            _httpService = httpService;
        }

        public void GetDailyChallenges(Action<ChallengeSetModel> callback, Action<ErrorResponseModel> errorCallback)
        {
            JsonHttpGetRequest(BASE_URL + "challenge/daily/mod", (res) =>
            {
                _logger.Info(res);
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<ChallengeSetModel>(res);
                callback(obj);
            }, errorCallback);
        }

        public void GetShopItems(Action<DealSetModel> callback, Action<ErrorResponseModel> errorCallback)
        {
            JsonHttpGetRequest(BASE_URL + "item/shop", (res) =>
            {
                _logger.Info(res);
                var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<DealSetModel>(res);
                callback(obj);
            }, errorCallback);
        }

        private void JsonHttpGetRequest(string url, Action<string> callback, Action<ErrorResponseModel> errorCallback)
        {
            Task.Run(async () =>
            {
                var res = await _httpService.GetAsync(url);
                if (!res.Successful)
                {
                    errorCallback.Invoke(new ErrorResponseModel(res));
                }
                var stringRes = await res.ReadAsStringAsync();
                callback(stringRes);
            });
        }

        private void JsonHttpPostRequest(string url, object obj, Action<string> callback, Action<ErrorResponseModel> errorCallback)
        {
            Task.Run(async () =>
            {
                var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
                var res = await _httpService.PostAsync(url, content);
                if (!res.Successful)
                {
                    errorCallback.Invoke(new ErrorResponseModel(res));
                }
                var stringRes = await res.ReadAsStringAsync();
                callback(stringRes);
            });
        }
    }
}
