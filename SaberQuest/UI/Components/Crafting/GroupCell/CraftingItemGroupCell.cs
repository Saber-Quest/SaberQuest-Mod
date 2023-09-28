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

            if (tableCell == null) //gross check I know
            {
                tableCell = new GameObject("CraftItemGroupListTableCell", typeof(Touchable)).AddComponent<CraftItemGroupListTableCell>();

                //We need to populate the list of sub cells for the foreach macro
                (tableCell as CraftItemGroupListTableCell).PopulateWithItems(items, itemParent, manager);

                tableCell.reuseIdentifier = ReuseIdentifier;
                BSMLParser.instance.Parse(
                    Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "SaberQuest.UI.Components.Crafting.GroupCell.CraftItemGroupListTableData"),
                    tableCell.gameObject, tableCell
                );
            }
            else
            {
                (tableCell as CraftItemGroupListTableCell).PopulateWithItems(items, itemParent, manager);
            }

            return (CraftItemGroupListTableCell)tableCell;
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
                            var cellVisuals = cells[i].linkedVisuals;
                            var row = items[i].row;
                            if (manager.firstCraftingCellRow != row && manager.secondCraftingCellRow != row)
                            {
                                Console.WriteLine("test1");
                                if (manager.firstCraftingCellVisuals == cellVisuals || manager.secondCraftingCellVisuals == cellVisuals)
                                {
                                    Console.WriteLine("test2");
                                    cellVisuals = CraftingCellSoftParentVisuals.GetVisualCell(itemParent);
                                    cellVisuals.cellManager = manager;
                                }
                            }
                            else
                            {
                                Console.WriteLine("test3");
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
