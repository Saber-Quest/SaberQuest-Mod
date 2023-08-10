using SaberQuest.Providers;
using SaberQuest.Providers.ApiProvider;
using SaberQuest.UI.SaberQuest;
using SaberQuest.UI.SaberQuest.Crafting.Views;
using SaberQuest.UI.SaberQuest.Crafting;
using SaberQuest.UI.SaberQuest.Shop.Views;
using SaberQuest.UI.SaberQuest.Shop;
using SaberQuest.UI.SaberQuest.Views;
using UnityEngine;
using Zenject;

namespace SaberQuest.Installers
{
    internal class SaberQuestMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
#if DEBUG
            Container.BindInterfacesTo<MockSaberQuestApiProvider>().AsSingle().NonLazy();
#else
			Container.BindInterfacesTo<SaberQuestApiProvider>().AsSingle().NonLazy();
#endif
            Container.Bind<CraftView>().FromNewComponentAsViewController().AsSingle().NonLazy();
            Container.Bind<SaberQuestCraftFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

            Container.Bind<ShopView>().FromNewComponentAsViewController().AsSingle().NonLazy();
            Container.Bind<SaberQuestShopFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

            Container.Bind<DailyChallengesView>().FromNewComponentAsViewController().AsSingle().NonLazy();
            Container.Bind<MainView>().FromNewComponentAsViewController().AsSingle().NonLazy();
            Container.Bind<SaberQuestFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

            Container.Bind<SaberQuestWebSocketSubProvider>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}
