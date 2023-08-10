using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Config.Data;
using IPA.Utilities;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.API.Data.Deals;
using SaberQuest.Providers.ApiProvider;
using SaberQuest.UI.Components;
using SaberQuest.Utils;
using SiraUtil.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace SaberQuest.UI.SaberQuest.Crafting.Views
{
    [HotReload(RelativePathToLayout = @"CraftView.bsml")]
    [ViewDefinition("SaberQuest.UI.SaberQuest.Crafting.Views.CraftView.bsml")]
    internal class CraftView : BSMLAutomaticViewController
    {
        //Dependencies
        private ISaberQuestApiProvider _apiProvider;
        private SiraLog _logger;

        [Inject]
        private void Construct(ISaberQuestApiProvider apiProvider, SiraLog siraLog)
        {
            _apiProvider = apiProvider;
            _logger = siraLog;
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            if (gameObject.GetComponent<Touchable>() == null)
                gameObject.AddComponent<Touchable>();
            foreach (var x in GetComponentsInChildren<Backgroundable>().Select(x => x.GetComponent<ImageView>()))
            {
                if (!x || x.color0 != Color.white || x.sprite.name != "RoundRect10" || x.transform.childCount < 1)
                    continue;

                var firstChild = x.transform.GetChild(0);

                Color targetColor = new Color(1f, 1f, 1f, 0.4f);
                if (firstChild != null)
                {

                    var childText = firstChild.GetComponent<TextMeshProUGUI>();
                    if (childText != null && childText.text.StartsWith("#color"))
                        ColorUtility.TryParseHtmlString(childText.text.Replace("#color", ""), out targetColor);
                }

                ReflectionUtil.SetField(x, "_skew", 0f);
                x.overrideSprite = null;
                x.SetImage("#RoundRect10BorderFade");
                x.color = targetColor;
            }
        }
    }
}
