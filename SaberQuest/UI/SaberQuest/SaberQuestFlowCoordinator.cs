using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using SaberQuest.UI.Main.Views;
using Zenject;

namespace SaberQuest.UI.SaberQuest
{
	internal class SaberQuestFlowCoordinator : FlowCoordinator
	{
		private MainView _mainView;

		[Inject]
		internal void Construct(MainView mainViewController)
		{
			_mainView = mainViewController;
		}

		protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			if (firstActivation)
			{
				SetTitle("SaberQuest");
				showBackButton = true;
				ProvideInitialViewControllers(_mainView);
			}
		}

		protected override void BackButtonWasPressed(ViewController topViewController)
		{
			BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
		}
	}
}