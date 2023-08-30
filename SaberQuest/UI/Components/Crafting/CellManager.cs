using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.UI.Components.Crafting.IndividualCell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SaberQuest.UI.Components.Crafting
{
    internal class CellManager : MonoBehaviour
    {
		public GameObject firstSlot;
		public GameObject secondSlot;

		//Clean this up at some point
		public CraftItemCell firstCraftingCell;
		public CraftingCellSoftParentVisuals firstCraftingCellVisuals;
		public ItemModel firstCraftingCellItem;
		public int firstCraftingCellRow;

		public CraftItemCell secondCraftingCell;
		public CraftingCellSoftParentVisuals secondCraftingCellVisuals;
		public ItemModel secondCraftingCellItem;
		public int secondCraftingCellRow;

		public void CellClicked(CraftItemCell cell)
		{
			if (cell == firstCraftingCell)
			{
				cell.itemModel.usedInCrafting = false;
				firstCraftingCell = null;
				firstCraftingCellVisuals = null;
				firstCraftingCellItem = null;
				firstCraftingCellRow = -1;
			}
			else if (cell == secondCraftingCell)
			{
				cell.itemModel.usedInCrafting = false;
				secondCraftingCell = null;
				secondCraftingCellVisuals = null;
				secondCraftingCellItem = null;
				secondCraftingCellRow = -1;
			}
			else
			{
				if (firstCraftingCell == null)
				{
					cell.itemModel.usedInCrafting = true;
					firstCraftingCell = cell;
					firstCraftingCellVisuals = cell.linkedVisuals;
					firstCraftingCellItem = cell.itemModel;
					firstCraftingCellRow = cell.itemModel.row;
				}
				else if (secondCraftingCell == null)
				{
					cell.itemModel.usedInCrafting = true;
					secondCraftingCell = cell;
					secondCraftingCellVisuals = cell.linkedVisuals;
					secondCraftingCellItem = cell.itemModel;
					secondCraftingCellRow = cell.itemModel.row;
				}
			}
		}
		public void Update()
		{
			if (firstSlot == null || secondSlot == null)
				return;
			if (firstCraftingCell != null && firstCraftingCell.itemModel.row == firstCraftingCellRow)
			{
				firstCraftingCell.itemModel.usedInCrafting = true;
				firstCraftingCell.linkedVisuals.overrideObject = firstSlot;
			}

			if (secondCraftingCell != null && secondCraftingCell.itemModel.row == secondCraftingCellRow)
			{
				secondCraftingCell.itemModel.usedInCrafting = true;
				secondCraftingCell.linkedVisuals.overrideObject = secondSlot;
			}
		}
	}
}
