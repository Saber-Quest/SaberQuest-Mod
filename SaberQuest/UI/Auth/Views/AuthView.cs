using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using SaberQuest.Providers;
using SaberQuest.Utils;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace SaberQuest.UI.Auth.Views
{
	[HotReload(RelativePathToLayout = @"AuthView.bsml")]
	[ViewDefinition("SaberQuest.UI.Auth.Views.AuthView")]
	internal class AuthView : BSMLAutomaticViewController
	{
		internal enum LoadingType
		{
			SigningUp,
			LoggingIn,
			CheckingForAccount
		}

		[Inject]
		private AuthFlowCoordinator _authFlow = null;

		[Inject]
		private RefreshTokenStorageProvider _refreshTokenStorageProvider = null;

		internal LoadingType loadingToUse = LoadingType.CheckingForAccount;

		[UIObject("loginScreen")]
		private GameObject _login = null;

		[UIObject("signupScreen")]
		private GameObject _signup = null;

		[UIObject("idleScreen")]
		private GameObject _idle = null;

		[UIComponent("text")]
		private TextMeshProUGUI text = null;

		[UIObject("usernameObj")]
		private GameObject signupUsername = null;

		[UIObject("passwordObj")]
		private GameObject signupPassword = null;

		[UIObject("usernameObj2")]
		private GameObject loginUsername = null;

		[UIObject("passwordObj2")]
		private GameObject loginPassword = null;

		[UIValue("username")]
		public string username { get; set; } = "Username";
		[UIValue("password")]
		public string password { get; set; } = "Password";



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
			_idle.SetActive(true);
			_login.SetActive(false);
			_signup.SetActive(false);

			BSMLUtils.FixStringSetting(signupUsername.transform);
			BSMLUtils.FixStringSetting(signupPassword.transform);
			BSMLUtils.FixStringSetting(loginUsername.transform);
			BSMLUtils.FixStringSetting(loginPassword.transform);
		}

		public override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			base.DidActivate(firstActivation, addedToHierarchy, screenSystemEnabling);
			moveOn = false;
			GoTo(LoadingType.CheckingForAccount);
			if (_refreshTokenStorageProvider.RefreshTokenExists())
			{
				var token = _refreshTokenStorageProvider.GetRefreshToken();
				checkingValid = false;
				moveOn = true;
				/*_authFlow._apiProvider.AccessToken(token, (x) =>
				{
					if (x == "Request Failed")
					{
						//Refresh Token Expired
						moveOn = true;
					}
					else
					{
						_authFlow._apiProvider.Identity(x, (x) =>
						{
							if (x.Username == "Identity Request Failed")
							{
								//Failed to get access token somehow
								moveOn = true;
							}
							else
							{
								//Success
								checkingValid = true;
								moveOn = true;
							}
						});
					}
				});*/
			}
			else
			{
				moveOn = true;
			}
		}

		[UIAction("signup")]
		private void SignUp()
		{
			_signup.SetActive(false);
			_idle.SetActive(true);
			_login.SetActive(false);
			GoTo(LoadingType.SigningUp);

			/*_authFlow._apiProvider.Signup(username, password, (x) =>
			{
				_refreshTokenStorageProvider.StoreRefreshToken(x.RefreshToken);
				_authFlow._apiProvider.AccessToken(x.RefreshToken, (x) =>
				{
					_authFlow._apiProvider.Identity(x, (x) =>
					{
						moveOn = true;
					});
				});
			});*/
			moveOn = true;
		}

		[UIAction("login")]
		private void Login()
		{
			_signup.SetActive(false);
			_idle.SetActive(true);
			_login.SetActive(false);
			GoTo(LoadingType.LoggingIn);

			/*_authFlow._apiProvider.Login(username, password, (x) =>
			{

				_refreshTokenStorageProvider.StoreRefreshToken(x.RefreshToken);
				_authFlow._apiProvider.AccessToken(x.RefreshToken, (x) =>
				{
					_authFlow._apiProvider.Identity(x, (x) =>
					{
						moveOn = true;
					});
				});
			});*/
			moveOn = true;
		}

		[UIAction("goToLogin")]
		private void GoToLogin()
		{
			_signup.SetActive(false);
			_idle.SetActive(false);
			_login.SetActive(true);
		}

		private void GoTo(LoadingType loadingToUse)
		{
			this.loadingToUse = loadingToUse;
			StartCoroutine(WaitCoroutine());
			text.text = loadingToUse switch
			{
				LoadingType.CheckingForAccount => "Checking For Account",
				LoadingType.LoggingIn => "Logging Into Account",
				LoadingType.SigningUp => "Signing Up For Account",
				_ => ""
			};
		}

		internal bool moveOn = false;
		internal bool checkingValid = false;
		IEnumerator WaitCoroutine()
		{
			moveOn = false;
			yield return new WaitUntil(() => moveOn);
			switch (loadingToUse)
			{
				case LoadingType.CheckingForAccount:
					if (checkingValid)
					{
						_authFlow.GoToRankingFlow();
					}
					else
					{
						_signup.SetActive(true);
						_login.SetActive(false);
						_idle.SetActive(false);
					}
					break;
				case LoadingType.LoggingIn:
				case LoadingType.SigningUp:
					_authFlow.GoToRankingFlow();
					break;
			}
		}
	}
}