using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using SaberQuest.UI.Auth.Views;
using SaberQuest.UI.SaberQuest;
using Zenject;

namespace SaberQuest.UI.Auth
{
	internal class AuthFlowCoordinator : FlowCoordinator
	{
		private SaberQuestFlowCoordinator _saberQuestFlow;
		private AuthView _authView;

		[Inject]
		internal void Construct(AuthView authViewController, SaberQuestFlowCoordinator saberQuestFlowCoordinator)
		{
			_authView = authViewController;
			_saberQuestFlow = saberQuestFlowCoordinator;
			MenuButtons.instance.RegisterButton(
				new MenuButton("Saber Quest", () =>
				{
					BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this);
				}
			));
		}

		protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			if (firstActivation)
			{
				SetTitle("Authorizing...");
				showBackButton = true;
				ProvideInitialViewControllers(_authView);
			}
		}

		internal void GoToRankingFlow()
		{
			BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this, null, ViewController.AnimationDirection.Horizontal, true);
			BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(_saberQuestFlow, null, ViewController.AnimationDirection.Horizontal, true);
		}

		protected override void BackButtonWasPressed(ViewController topViewController)
		{
			BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
		}
	}
}