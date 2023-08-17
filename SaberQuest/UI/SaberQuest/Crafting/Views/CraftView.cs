using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Config.Data;
using IPA.Utilities;
using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.API.Data.Deals;
using SaberQuest.Providers.ApiProvider;
using SaberQuest.UI.Components;
using SaberQuest.UI.Components.Crafting.GroupCell;
using SaberQuest.UI.Components.Crafting.IndividualCell;
using SaberQuest.Utils;
using SiraUtil.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SaberQuest.UI.SaberQuest.Crafting.Views
{
    [HotReload(RelativePathToLayout = @"CraftView.bsml")]
    [ViewDefinition("SaberQuest.UI.SaberQuest.Crafting.Views.CraftView.bsml")]
    internal class CraftView : BSMLAutomaticViewController, TableView.IDataSource
    {
        //Dependencies
        private ISaberQuestApiProvider _apiProvider;
        private SiraLog _logger;

        //Soft Parent Visuals
        [UIObject("item-parent")] private GameObject _itemParent;

        //Shop List
        [UIComponent("craft-list")] private CustomListTableData list = null;
        private TableView craftList => list?.tableView;

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

            craftList.SetDataSource(this, false);

            foreach (var x in GetComponentsInChildren<Backgroundable>().Select(x => x.GetComponent<ImageView>()))
            {
                if (!x || x.color0 != Color.white || x.sprite.name != "RoundRect10")
                    continue;

                ReflectionUtil.SetField(x, "_skew", 0f);
                x.overrideSprite = null;
                x.SetImage("#RoundRect10BorderFade");
                x.color = new Color(1f, 1f, 1f, 0.4f);
            }
			Destroy(_itemParent.GetComponent<HorizontalLayoutGroup>());

			craftList.ReloadData();
            craftList.SelectCellWithIdx(0);

            var mask = _itemParent.AddComponent<RectMask2D>();

            mask.padding = new Vector4(0f, 2f, 0f, 2f);

			IVRPlatformHelper platformHelper = null;
            foreach (var x in Resources.FindObjectsOfTypeAll<ScrollView>())
            {
                platformHelper = ReflectionUtil.GetField<IVRPlatformHelper, ScrollView>(x, "_platformHelper");
                if (platformHelper != null)
                    break;
            }
            foreach (var x in GetComponentsInChildren<ScrollView>()) ReflectionUtil.SetField(x, "_platformHelper", platformHelper);
        }

        public float CellSize() => 32f;

        public int NumberOfCells() => 10;

        public TableCell CellForIdx(TableView tableView, int idx) => CraftItemGroupListTableData.GetCell(tableView, new List<ItemModel>(4) { null, null, null, null }, _itemParent.transform);
	}
}
