using BeatSaberMarkupLanguage;
using HMUI;
using SaberQuest.UI.SaberQuest.Crafting.Views;
using Zenject;

namespace SaberQuest.UI.SaberQuest.Crafting
{
    internal class SaberQuestCraftFlowCoordinator : FlowCoordinator
    {
        private CraftView _shopView;
        private SaberQuestFlowCoordinator _mainFlow;

        [Inject]
        internal void Construct(CraftView shopViewController, SaberQuestFlowCoordinator saberQuestFlowCoordinator)
        {
            _shopView = shopViewController;
            _mainFlow = saberQuestFlowCoordinator;
        }

        public override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("Shop");
                showBackButton = true;
                ProvideInitialViewControllers(_shopView);
            }
        }

        public override void BackButtonWasPressed(ViewController topViewController)
        {
            _mainFlow.DismissFlowCoordinator(this);
        }
    }
}