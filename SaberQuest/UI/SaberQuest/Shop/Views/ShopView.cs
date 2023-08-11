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
using SaberQuest.UI.Components.Shop;
using SaberQuest.Utils;
using SiraUtil.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace SaberQuest.UI.SaberQuest.Shop.Views
{
    [HotReload(RelativePathToLayout = @"ShopView.bsml")]
    [ViewDefinition("SaberQuest.UI.SaberQuest.Shop.Views.ShopView.bsml")]
    internal class ShopView : BSMLAutomaticViewController, TableView.IDataSource
    {
        //Dependencies
        private ISaberQuestApiProvider _apiProvider;
        private SiraLog _logger;

        //Shop List
        [UIComponent("shopList")] private CustomListTableData list = null;
        private TableView shopList => list?.tableView;

        //Data
        private List<DealModel> CurrentDeals = new List<DealModel>();
        private DealModel _selectedDeal;

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

            shopList.SetDataSource(this, false);

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

            if (list != null)
            {
                _apiProvider.GetCurrentDeals((x) =>
                {
                    ApplyShopItems(x);
                }, async (error) =>
                {
                    //TODO: Show Error Modal here
                    _logger.Error(await error.Message.Error());
                });
            }

            StartCoroutine(ContentCoroutine());
        }

        private IEnumerator ContentCoroutine()
        {
            yield return new WaitForEndOfFrame();
            var viewport = list.transform.GetChild(0).GetChild(0) as RectTransform;
            var content = viewport.GetChild(0) as RectTransform;

            viewport.sizeDelta = new Vector2(0f, 10f);
            yield return new WaitForEndOfFrame();
            content.anchorMin = new Vector2(0f, 0.15f);
            content.anchorMax = new Vector2(0f, 0.85f);
        }

        private void ApplyShopItems(DealSetModel deals)
        {
            HMMainThreadDispatcher.instance.Enqueue(() =>
            {
                if (!(deals?.Deals?.Count > 0))
                {
                    _logger.Error("No Shop Items Found... Do you need to update?");
                    return;
                }
                CurrentDeals = deals.Deals;
                shopList.ReloadData();
                shopList.SelectCellWithIdx(0);
            });
        }

        public float CellSize() => 18f;

        public int NumberOfCells() => CurrentDeals.Count;

        public TableCell CellForIdx(TableView tableView, int idx) => ShopItemListTableData.GetCell(tableView).PopulateWithShopItemData(CurrentDeals[idx]);
    }
}
