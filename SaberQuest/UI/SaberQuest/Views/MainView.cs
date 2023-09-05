using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Loader;
using IPA.Utilities;
using SaberQuest.UI.SaberQuest.Crafting;
using SaberQuest.UI.SaberQuest.Shop;
using SiraUtil.Web.SiraSync;
using SiraUtil.Zenject;
using System.Linq;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SaberQuest.UI.SaberQuest.Views
{
    [HotReload(RelativePathToLayout = @"MainView.bsml")]
    [ViewDefinition("SaberQuest.UI.SaberQuest.Views.MainView")]
    internal class MainView : BSMLAutomaticViewController
    {
        //Dependencies
		private SaberQuestFlowCoordinator _mainFlow;
		private SaberQuestShopFlowCoordinator _shopFlow;
		private SaberQuestCraftFlowCoordinator _craftFlow;
		private ISiraSyncService _siraSyncService;
        private PluginMetadata _metadata;

		//UI Objects
		[UIObject("avatar-mask")] private GameObject avatarMask;
        [UIObject("container-background")] private GameObject containerBackground;
        [UIComponent("version-text")] private TextMeshProUGUI versionText;

        [Inject]
        private void Construct(SaberQuestFlowCoordinator mainFlowCoordinator, SaberQuestShopFlowCoordinator shopFlowCoordinator, SaberQuestCraftFlowCoordinator craftFlowCoordinator, ISiraSyncService siraSyncService, UBinder<Plugin, PluginMetadata> metadata)
        {
            _mainFlow = mainFlowCoordinator;
            _shopFlow = shopFlowCoordinator;
            _craftFlow = craftFlowCoordinator;
            _siraSyncService = siraSyncService;
            _metadata = metadata.Value;
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

            Task.Run(async () => {
                var latest = await _siraSyncService.LatestVersion();
				HMMainThreadDispatcher.instance.Enqueue(() => {
					if (latest > _metadata.HVersion || true)
					{
                        versionText.text = $"Your version of SaberQuest is out of date! Latest: v{latest}, Current: v{_metadata.HVersion}";
						versionText.color = Color.red;
					}
					else
                    {
						versionText.text = $"Your version of SaberQuest is up to date!";
						versionText.color = Color.green;
                    }
				});
            });
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