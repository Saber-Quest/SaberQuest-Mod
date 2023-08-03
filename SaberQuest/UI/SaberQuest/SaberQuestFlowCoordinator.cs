using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using SaberQuest.UI.SaberQuest.Views;
using Zenject;

namespace SaberQuest.UI.SaberQuest
{
	internal class SaberQuestFlowCoordinator : FlowCoordinator
	{
		private MainView _mainView;
		private DailyChallengesView _challengesView;

		[Inject]
		internal void Construct(MainView mainViewController, DailyChallengesView challengesView)
		{
			_mainView = mainViewController;
			_challengesView = challengesView;
		}

		protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			if (firstActivation)
			{
				SetTitle("SaberQuest");
				showBackButton = true;
				ProvideInitialViewControllers(_mainView, _challengesView);
			}
		}

		protected override void BackButtonWasPressed(ViewController topViewController)
		{
			BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
		}
	}
}