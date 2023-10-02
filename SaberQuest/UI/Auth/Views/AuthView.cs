using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using SaberQuest.Providers;
using SaberQuest.Providers.ApiProvider;
using SaberQuest.Providers.BSChallenger.Providers;
using SaberQuest.Stores;
using System;
using System.Linq;
using System.Threading.Tasks;
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

		public override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);

			//Action because uhhhh
			var getNewToken = () =>
			{
				var websocket = new AuthWebsocketProvider();
				websocket.Initialize((x) =>
				{
					Console.WriteLine("Token recieved, storing");
					_tokenStorageProvider.StoreToken(x);
					_apiProvider.ProvideToken(x);
					Console.WriteLine("Getting new user");
					var userStore = UserStore.Get();
					if (!userStore.IsFailure)
					{
						Console.WriteLine("Setting new user");
						userStore.Value.SetUser(async _ => {
							await Task.Delay(500);
							HMMainThreadDispatcher.instance.Enqueue(() =>
							{
								_authFlow.GoToMainFlow();
							});
						}, () => { });
					}
				});
				Console.WriteLine("Opening auth url");
				Application.OpenURL("https://dev.saberquest.xyz/login/mod/beatleader");
			};

			Console.WriteLine("Starting AuthView");
			var token = _tokenStorageProvider.GetToken();

			if (!string.IsNullOrEmpty(token))
			{
				Console.WriteLine("Existing token exists");
				_apiProvider.ProvideToken(token);
				var userStore = UserStore.Get();
				userStore.Value.SetUser(async _ =>
				{
					await Task.Delay(500);
					HMMainThreadDispatcher.instance.Enqueue(() =>
					{
						_authFlow.GoToMainFlow();
					});
				}, () => getNewToken());
				Console.WriteLine("Set Current User");
			}
			else
			{
				getNewToken();
			}
		}
	}
}