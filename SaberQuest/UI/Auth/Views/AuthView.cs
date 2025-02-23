using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using IPA.Utilities.Async;
using SaberQuest.Providers;
using SaberQuest.Providers.ApiProvider;
using SaberQuest.Providers.BSChallenger.Providers;
using SaberQuest.Stores;
using SiraUtil.Web;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using Zenject;

namespace SaberQuest.UI.Auth.Views
{
	[HotReload(RelativePathToLayout = @"AuthView.bsml")]
	[ViewDefinition("SaberQuest.UI.Auth.Views.AuthView")]
	internal class AuthView : BSMLAutomaticViewController
	{
		private SaberQuestAuthenticationFlowCoordinator _authFlow = null;
		private TokenStorageProvider _tokenStorageProvider = null;
		private ISaberQuestApiProvider _apiProvider = null;
		[Inject] private IPlatformUserModel _platformUserModel = null;

        [Inject]
		internal void Construct(SaberQuestAuthenticationFlowCoordinator authFlow, TokenStorageProvider tokenStorageProvider, ISaberQuestApiProvider apiProvider)
		{
			_authFlow = authFlow;
			_tokenStorageProvider = tokenStorageProvider;
			_apiProvider = apiProvider;
		}

		[UIAction("#post-parse")]
		internal void PostParse()
		{
			if (gameObject.GetComponent<Touchable>() == null)
				gameObject.AddComponent<Touchable>();
			foreach (var x in GetComponentsInChildren<Backgroundable>().Select(x => x.GetComponent<ImageView>()))
			{
				if (!x || x.color0 != Color.white || x.sprite.name != "RoundRect10")
					continue;

				ReflectionUtil.SetField(x, "_skew", 0f);
				x.overrideSprite = null;
				x.SetImage("#RoundRect10BorderFade");
				x.color = new Color(1f, 1f, 1f, 0.4f);
			}
		}

		[UIComponent("text")]
        private TextMeshProUGUI _text = null;

		[UIObject("loading")]
		private GameObject _loading = null;

        public override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);

            Task.Run(async() =>
            {
                UserInfo userInfo = await _platformUserModel.GetUserInfo(CancellationToken.None);
                XPlatformAccessTokenData tokendata = await _platformUserModel.RequestXPlatformAccessToken(CancellationToken.None);

                string username = userInfo.userName;
                string userId = userInfo.platformUserId;
                string platform = userInfo.platform.ToString().ToUpper();
                string token = tokendata.token;

                UnityMainThreadTaskScheduler.Factory.StartNew(() =>
				{
                    _apiProvider.Authenticate(username, userId, token, platform, (user) =>
                    {
                        _authFlow.GoToMainFlow();
                    }, (err) =>
                    {
                        Console.WriteLine($"Failed to authenticate user because of error: {err?.ToString()}");
                        _text.text = "Failed to authenticate!\nReport this to the developers!";
                        _loading.SetActive(false);
                    });
                });
            });
        }
	}
}