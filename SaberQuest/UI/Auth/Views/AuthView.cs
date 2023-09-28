using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.MenuButtons;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using SaberQuest.Providers;
using SaberQuest.Providers.ApiProvider;
using SaberQuest.Providers.BSChallenger.Providers;
using SaberQuest.Stores;
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

			Task.Run(async () =>
			{
				var websocket = new AuthWebsocketProvider();
				websocket.Initialize((x) =>
				{
					_tokenStorageProvider.StoreToken(x);
					_apiProvider.ProvideToken(x);
					var userStore = UserStore.Get();
					if(!userStore.IsFailure)
					{
						userStore.Value.SetUser();
						_authFlow.GoToMainFlow();
					}
				});
				while (!websocket.started)
				{
					await Task.Delay(100);
				}
				Application.OpenURL("https://dev.saberquest.xyz/login/mod/beatleader");
			});
		}
	}
}