using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using SaberQuest.UI.SaberQuest.Crafting;
using SaberQuest.UI.SaberQuest.Shop;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SaberQuest.UI.SaberQuest.Views
{
    [HotReload(RelativePathToLayout = @"MainView.bsml")]
    [ViewDefinition("SaberQuest.UI.SaberQuest.Views.MainView")]
    internal class MainView : BSMLAutomaticViewController
    {
        [UIObject("avatarMask")]
        private GameObject avatarMask;
        [UIObject("containerBackground")]
        private GameObject containerBackground;

        private SaberQuestFlowCoordinator _mainFlow;
        private SaberQuestShopFlowCoordinator _shopFlow;
        private SaberQuestCraftFlowCoordinator _craftFlow;

        [Inject]
        private void Construct(SaberQuestFlowCoordinator mainFlowCoordinator, SaberQuestShopFlowCoordinator shopFlowCoordinator, SaberQuestCraftFlowCoordinator craftFlowCoordinator)
        {
            _mainFlow = mainFlowCoordinator;
            _shopFlow = shopFlowCoordinator;
            _craftFlow = craftFlowCoordinator;
        }

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
            avatarMask.AddComponent<Mask>().showMaskGraphic = false;

            var containerImage = containerBackground.AddComponent<ImageView>();
            containerImage.material = Utilities.ImageResources.NoGlowMat;
            containerImage.SetImage("SaberQuest.UI.Resources.containerbg.png");
        }

        [UIAction("present-shop")]
        private void EnterShop()
        {
            _mainFlow.PresentFlowCoordinator(_shopFlow);
        }

        [UIAction("present-craft")]
        private void EnterCraft()
        {
            _mainFlow.PresentFlowCoordinator(_craftFlow);
        }
    }
}