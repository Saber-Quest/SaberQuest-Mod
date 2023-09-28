using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HMUI;
using SaberQuest.UI.Auth.Views;
using SaberQuest.UI.SaberQuest;
using SaberQuest.UI.SaberQuest.Views;
using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace SaberQuest.UI.Auth
{
    internal class SaberQuestAuthenticationFlowCoordinator : FlowCoordinator
    {
        private AuthView _authView;
        private SaberQuestFlowCoordinator _mainFlow;

		[Inject]
        internal void Construct(AuthView authView, SaberQuestFlowCoordinator mainFlow)
        {
            _authView = authView;
            _mainFlow = mainFlow;
            MenuButtons.instance.RegisterButton(
                new MenuButton("Saber Quest", () =>
                {
                    BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(this);
                }
            ));
        }

        public override void DidActivate(bool firstActivation, bool addedToHierarchy, bool screenSystemEnabling)
        {
            if (firstActivation)
            {
                SetTitle("Saber Quest");
                showBackButton = true;
                ProvideInitialViewControllers(_authView);
            }
        }

        public void GoToMainFlow()
        {
            HMMainThreadDispatcher.instance.Enqueue(() =>
            {
                StartCoroutine(Goto());
            });
		}

        private IEnumerator Goto()
        {
			BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
            yield return new WaitForEndOfFrame();
			BeatSaberUI.MainFlowCoordinator.PresentFlowCoordinator(_mainFlow);
		}

		public override void BackButtonWasPressed(ViewController topViewController)
        {
            BeatSaberUI.MainFlowCoordinator.DismissFlowCoordinator(this);
        }
    }
}