using SaberQuest.Providers;
using SaberQuest.UI.Auth;
using SaberQuest.UI.Auth.Views;
using SaberQuest.UI.SaberQuest;
using SaberQuest.UI.SaberQuest.Views;
using UnityEngine;
using Zenject;

namespace SaberQuest.Installers
{
	internal class SaberQuestMenuInstaller : Installer
	{
		public override void InstallBindings()
		{
			Container.Bind<RefreshTokenStorageProvider>().AsSingle().NonLazy();
			Container.Bind<SaberQuestApiProvider>().AsSingle().NonLazy();

			Container.Bind<DailyChallengesView>().FromNewComponentAsViewController().AsSingle().NonLazy();
			Container.Bind<MainView>().FromNewComponentAsViewController().AsSingle().NonLazy();
			Container.Bind<SaberQuestFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

			Container.Bind<AuthView>().FromNewComponentAsViewController().AsSingle().NonLazy();
			Container.Bind<AuthFlowCoordinator>().FromNewComponentOnNewGameObject().AsSingle().NonLazy();

			new GameObject().AddComponent<SaberQuestWebSocketSubProvider>();
		}
	}
}
