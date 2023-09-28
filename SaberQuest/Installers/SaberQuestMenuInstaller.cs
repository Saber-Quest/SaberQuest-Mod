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
using SaberQuest.Stores;
using SaberQuest.UI.Auth;
using SaberQuest.UI.Auth.Views;
using SaberQuest.Providers.BSChallenger.Providers;

namespace SaberQuest.Installers
{
    internal class SaberQuestMenuInstaller : Installer
    {
        public override void InstallBindings()
        {
			Container.BindInterfacesTo<SaberQuestApiProvider>().AsSingle().NonLazy();

			Container.Bind<TokenStorageProvider>().AsSingle().NonLazy();

			Container.Bind<UserStore>().AsSingle().NonLazy();
			Container.Bind<ItemStore>().AsSingle().NonLazy();

			Container.Bind<CraftView>().FromNewComponentAsViewController().AsSingle().NonLazy();
			Container.Bind<SaberQuestCraftFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

            Container.Bind<ShopView>().FromNewComponentAsViewController().AsSingle().NonLazy();
            Container.Bind<SaberQuestShopFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

			Container.Bind<AuthView>().FromNewComponentAsViewController().AsSingle().NonLazy();
			Container.Bind<SaberQuestAuthenticationFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

			Container.Bind<DailyChallengesView>().FromNewComponentAsViewController().AsSingle().NonLazy();
            Container.Bind<MainView>().FromNewComponentAsViewController().AsSingle().NonLazy();
            Container.Bind<SaberQuestFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

            Container.Bind<SaberQuestWebSocketSubProvider>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();
        }
    }
}
