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
			var tableCell = DequeueReusableCellForIdentifierWCheck(tableView, ReuseIdentifier);

			if (tableCell == null) //gross check I know
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
			else
			{
				(tableCell as CraftItemGroupListTableCell).PopulateWithItems(items, itemParent, manager);
			}

			return (CraftItemGroupListTableCell)tableCell;
		}

		public static TableCell DequeueReusableCellForIdentifierWCheck(TableView view, string identifier)
		{
			TableCell tableCell = null;
			List<TableCell> list;
			if (view._reusableCells.TryGetValue(identifier, out list) && list.Count > 0)
			{
				var cells = list.Where(x => !(x as CraftItemGroupListTableCell).cells.Any(x=>x.itemModel.usedInCrafting));
				tableCell = list[0];
				list.RemoveAt(0);
			}
			return tableCell;
		}
	}

	class CraftItemGroupListTableCell : TableCell
	{
		private Transform _itemParent;
		private List<ItemModel> _items;

		[UIValue("cells")] internal List<CraftItemCell> cells;

		public CraftItemGroupListTableCell PopulateWithItems(List<ItemModel> items, Transform itemParent, CellManager manager)
		{
			_itemParent = itemParent;
			_items = items;
			if (cells == null)
			{
				cells = items.ConvertAll(x =>
				{
					var visuals = CraftingCellSoftParentVisuals.GetVisualCell(itemParent);
					visuals.cellManager = manager;
					var cell = new CraftItemCell().PopulateWithItemData(x, visuals);
					visuals.SetCell(cell);
					return cell;
				});
			}
			else
			{
				for (int i = 0; i < items.Count; i++)
				{
					if (i < cells.Count && cells[i] != null)
					{
						if (!cells[i].itemModel.usedInCrafting && cells[i].linkedVisuals != null)
						{
							Console.WriteLine("test");
							var cellVisuals = cells[i].linkedVisuals;
							var row = cells[i].itemModel.row;
							if (manager.firstCraftingCellRow != row && manager.secondCraftingCellRow != row)
							{
								if (manager.firstCraftingCellVisuals == cellVisuals || manager.secondCraftingCellVisuals == cellVisuals)
								{
									cellVisuals = CraftingCellSoftParentVisuals.GetVisualCell(itemParent);
									cellVisuals.cellManager = manager;
								}
							}
							else
							{
								cellVisuals = manager.firstCraftingCellItem == items[i] ? manager.firstCraftingCellVisuals : manager.secondCraftingCellVisuals;
							}
							var cell = cells[i].PopulateWithItemData(items[i], cellVisuals);
							cellVisuals.SetCell(cell);
							cells[i] = cell;
						}
						else
						{
							Console.WriteLine("ghj");
							var visuals = manager.firstCraftingCellItem == items[i] ? manager.firstCraftingCellVisuals : manager.secondCraftingCellVisuals;
							var cell = cells[i].PopulateWithItemData(items[i], visuals);
							visuals.SetCell(cell);
							cells[i] = cell;
						}
					}
				}
			}
			return this;
		}

		[UIAction("#post-parse")]
		internal void PostParse()
		{
		}
	}
}
