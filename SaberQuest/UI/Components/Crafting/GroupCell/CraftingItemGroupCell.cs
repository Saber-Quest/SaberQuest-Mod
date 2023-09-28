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

        public static CraftItemGroupListTableCell GetCell(int idx, VisualCellPool pool, TableView tableView, List<ItemModel> items, Transform itemParent, CellManager manager)
        {
            var tableCell = tableView.DequeueReusableCellForIdentifier(ReuseIdentifier);

            if (tableCell == null) //gross check I know
            {
                tableCell = new GameObject("CraftItemGroupListTableCell", typeof(Touchable)).AddComponent<CraftItemGroupListTableCell>();

                //We need to populate the list of sub cells for the foreach macro
                (tableCell as CraftItemGroupListTableCell).PopulateWithItems(pool, items, itemParent, manager);

                tableCell.reuseIdentifier = ReuseIdentifier;
                BSMLParser.instance.Parse(
                    Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "SaberQuest.UI.Components.Crafting.GroupCell.CraftItemGroupListTableData"),
                    tableCell.gameObject, tableCell
                );
            }
            else
            {
                (tableCell as CraftItemGroupListTableCell).PopulateWithItems(pool, items, itemParent, manager);
            }

            return (CraftItemGroupListTableCell)tableCell;
        }
    }

    class CraftItemGroupListTableCell : TableCell
    {
        private Transform _itemParent;
        private VisualCellPool _pool;
        private List<ItemModel> _items;

        [UIValue("cells")] internal List<CraftItemCell> cells = new List<CraftItemCell>(5);

        public CraftItemGroupListTableCell PopulateWithItems(VisualCellPool pool, List<ItemModel> items, Transform itemParent, CellManager manager)
        {
            _pool = pool;
			_itemParent = itemParent;
            _items = items;
            var targets = cells.ConvertAll(x => x.targetObject);
            cells = cells.ConvertAll<CraftItemCell>(x => {
                return null;
            });
			cells = items.Select((ItemModel x, int y) =>
			{
				var visuals = _pool.Get();
				visuals.cellManager = manager;
                var cell = new CraftItemCell().PopulateWithItemData(x, visuals);
                cell.targetObject = targets[y];
				visuals.SetCell(cell);
				return cell;
			}).ToList();
			return this;
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
        }

        private void OnDisable()
        {
            foreach (var cell in cells)
            {
                _pool.Dequeue(cell.linkedVisuals);
            }
        }
    }
}
