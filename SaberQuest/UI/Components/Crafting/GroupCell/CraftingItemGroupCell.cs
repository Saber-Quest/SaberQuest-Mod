using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.UI.Components.Crafting.IndividualCell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SaberQuest.UI.Components.Crafting.GroupCell
{
	static class CraftItemGroupListTableData
	{
		const string ReuseIdentifier = "REUSECraftItemGroupListTableCell";

		public static CraftItemGroupListTableCell GetCell(int idx, TableView tableView, List<ItemModel> items, Transform itemParent, CellManager manager)
		{
			var tableCell = tableView.DequeueReusableCellForIdentifier(ReuseIdentifier);

			bool pickReuse;
			if(tableCell != null)
			{
				Console.WriteLine(idx + " - " + tableCell.idx);

			}
			if (tableCell == null || ((tableCell as CraftItemGroupListTableCell).cells.Any(x=>x.crafting) && tableCell.idx != idx)) //gross check I know
			{
				tableCell = new GameObject("CraftItemGroupListTableCell", typeof(Touchable)).AddComponent<CraftItemGroupListTableCell>();

				//We need to populate the list of sub cells for the foreach macro
				(tableCell as CraftItemGroupListTableCell).PopulateWithItems(items, itemParent, manager);

				tableCell.reuseIdentifier = ReuseIdentifier;
				BSMLParser.instance.Parse(
					Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "SaberQuest.UI.Components.Crafting.GroupCell.CraftingGroupCell.bsml"),
					tableCell.gameObject, tableCell
				);
			}

			return (CraftItemGroupListTableCell)tableCell;
		}
	}

	class CraftItemGroupListTableCell : TableCell
	{
		private Transform _itemParent;
		private List<ItemModel> _items;

		[UIValue("cells")] internal List<CraftItemCell> cells = new List<CraftItemCell>();

		[UIObject("cell")] private readonly GameObject cell;

		public CraftItemGroupListTableCell PopulateWithItems(List<ItemModel> items, Transform itemParent, CellManager manager)
		{
			_itemParent = itemParent;
			_items = items;
			Console.WriteLine(items.Count);
			cells = items.ConvertAll(x =>
			{
				var visuals = CraftingCellSoftParentVisuals.GetVisualCell(itemParent);
				visuals.cellManager = manager;
				var cell = new CraftItemCell().PopulateWithItemData(x, visuals, itemParent);
				visuals.SetCell(cell);
				return cell;
			});
			return this;
		}

		[UIAction("#post-parse")]
		internal void PostParse()
		{
		}
	}
}
