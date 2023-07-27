using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using SaberQuest.Providers;
using SaberQuest.Utils;
using System.Collections;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace SaberQuest.UI.Main.Views
{
	[HotReload(RelativePathToLayout = @"MainView.bsml")]
	[ViewDefinition("SaberQuest.UI.Main.Views.MainView")]
	internal class MainView : BSMLAutomaticViewController
	{
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
		}
	}
}