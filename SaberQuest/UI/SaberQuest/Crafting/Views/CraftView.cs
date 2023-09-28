using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using SaberQuest.Extensions;
using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.Providers.ApiProvider;
using SaberQuest.Stores;
using SaberQuest.UI.Components.Crafting;
using SaberQuest.UI.Components.Crafting.GroupCell;
using SaberQuest.UI.Components.Crafting.IndividualCell;
using SiraUtil.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace SaberQuest.UI.SaberQuest.Crafting.Views
{
	[HotReload(RelativePathToLayout = @"CraftView.bsml")]
	[ViewDefinition("SaberQuest.UI.SaberQuest.Crafting.Views.CraftView")]
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

		//Crafting
		[UIObject("first-slot")] private GameObject _firstSlot;
		[UIObject("second-slot")] private GameObject _secondSlot;
		private CellManager cellManager;

		private List<ItemModel> items;
		private List<List<ItemModel>> chunkedItems;

		private VisualCellPool _pool;

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

			_pool = _itemParent.gameObject.AddComponent<VisualCellPool>();
			_pool.Initialize(_itemParent.transform);

			cellManager = gameObject.AddComponent<CellManager>();
			cellManager.firstSlot = _firstSlot;
			cellManager.secondSlot = _secondSlot;

			craftList.SetDataSource(this, false);

			items = ItemStore.Get().Value.GetItems();
			chunkedItems = items.ChunkBy(4);
			for (int row = 0; row < chunkedItems.Count; row++)
			{
				chunkedItems[row].ForEach(x => x.row = row);
			}

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

			craftList.transform.parent.parent.gameObject.GetComponent<ImageView>().raycastTarget = false;

			_itemParent.transform.SetParent(craftList.transform, false);
		}

		public float CellSize() => 32f;

		public int NumberOfCells() => chunkedItems.Count;

		public TableCell CellForIdx(TableView tableView, int idx) => CraftItemGroupListTableData.GetCell(idx, _pool, tableView, chunkedItems[idx], _itemParent.transform, cellManager);
	}
}
