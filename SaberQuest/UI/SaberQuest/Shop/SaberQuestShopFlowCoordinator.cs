using BeatSaberMarkupLanguage;
using HMUI;
using SaberQuest.UI.SaberQuest.Shop.Views;
using Zenject;

namespace SaberQuest.UI.SaberQuest
{
	internal class SaberQuestShopFlowCoordinator : FlowCoordinator
	{
		private ShopView _shopView;
		private SaberQuestFlowCoordinator _mainFlow;

		[Inject]
		internal void Construct(ShopView shopViewController, SaberQuestFlowCoordinator saberQuestFlowCoordinator)
		{
			_shopView = shopViewController;
			_mainFlow = saberQuestFlowCoordinator;
		}

		protected override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
		{
			if (firstActivation)
			{
				SetTitle("SaberQuest");
				showBackButton = true;
				ProvideInitialViewControllers(_shopView);
			}
		}

		protected override void BackButtonWasPressed(ViewController topViewController)
		{
			_mainFlow.DismissFlowCoordinator(this);
		}
	}
}