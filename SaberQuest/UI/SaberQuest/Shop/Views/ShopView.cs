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
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace SaberQuest.UI.SaberQuest.Shop.Views
{
	[HotReload(RelativePathToLayout = @"ShopView.bsml")]
	[ViewDefinition("SaberQuest.UI.SaberQuest.Shop.Views.ShopView.bsml")]
	internal class ShopView : BSMLAutomaticViewController
	{
		//Dependencies
		private ISaberQuestApiProvider _apiProvider;
		private SiraLog _logger;

		//Challenge List
		[UIComponent("shopList")]
		private CustomCellListTableData list = null;
		private List<object> shopItems = new List<object>();

		[Inject]
		private void Construct(ISaberQuestApiProvider apiProvider, SiraLog siraLog)
		{
			_apiProvider = apiProvider;
			_logger = siraLog;
		}

		[UIAction("#post-parse")]
		internal void PostParse()
		{
			_logger.Info("breh1");
			if (gameObject.GetComponent<Touchable>() == null)
				gameObject.AddComponent<Touchable>();
			foreach (var x in GetComponentsInChildren<Backgroundable>().Select(x => x.GetComponent<ImageView>()))
			{
				_logger.Info("breh");
				if (!x || x.color0 != Color.white || x.sprite.name != "RoundRect10" || x.transform.childCount < 1)
					continue;
				_logger.Info("breh 2");

				var firstChild = x.transform.GetChild(0);
				_logger.Info("breh 2.5");

				Color targetColor = new Color(1f, 1f, 1f, 0.4f);
				if (firstChild != null)
				{

					var childText = firstChild.GetComponent<TextMeshProUGUI>();
					if (childText != null && childText.text.StartsWith("#color"))
						ColorUtility.TryParseHtmlString(childText.text.Replace("#color", ""), out targetColor);
				}
				_logger.Info("breh 3");

				ReflectionUtil.SetField(x, "_skew", 0f);
				x.overrideSprite = null;
				x.SetImage("#RoundRect10BorderFade");
				x.color = targetColor;
			}
			_logger.Info("breh 4");

			if (list != null)
			{
				_logger.Info("sdljfasdasdasdadjsdf");
				_apiProvider.GetCurrentDeals((x) =>
				{
					_logger.Info("blud");
					ApplyShopItems(x);
				}, async (error) =>
				{
					_logger.Info("balls");

					//TODO: Show Error Modal here
					_logger.Error(await error.Message.Error());
				});
			}
		}

		private void ApplyShopItems(DealSetModel deals)
		{
			HMMainThreadDispatcher.instance.Enqueue(() =>
			{
				_logger.Info("XD");
				if (!(deals?.Deals?.Count > 0))
				{
					_logger.Error("No Daily Challenges Found... Do you need to update?");
					return;
				}
				_logger.Info("XD 2");
				shopItems = deals.Deals.ConvertAll(x => (object)new ShopItemCell(x));
				list.data = shopItems;
				list.tableView.ReloadData();
				_logger.Info("XD 3");
				list.tableView.SelectCellWithIdx(0);
				_logger.Info("XD 4");
			});
		}
	}
}
