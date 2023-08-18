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

		public CraftItemCell firstCraftingCell;
		public CraftItemCell secondCraftingCell;

		public void CellClicked(CraftItemCell cell)
		{
			if (cell == firstCraftingCell)
			{
				cell.crafting = false;
				firstCraftingCell = null;
			}
			else if (cell == secondCraftingCell)
			{
				cell.crafting = false;
				secondCraftingCell = null;
			}
			else
			{
				if (firstCraftingCell == null)
				{
					cell.crafting = true;
					firstCraftingCell = cell;
				}
				else if (secondCraftingCell == null)
				{
					cell.crafting = true;
					secondCraftingCell = cell;
				}
			}
		}
		public void Update()
		{
			if (firstSlot == null || secondSlot == null)
				return;
			if (firstCraftingCell != null)
			{
				firstCraftingCell.crafting = true;
				firstCraftingCell.linkedVisuals.overrideObject = firstSlot;
			}

			if (secondCraftingCell != null)
			{
				secondCraftingCell.crafting = true;
				secondCraftingCell.linkedVisuals.overrideObject = secondSlot;
			}
		}
	}
}
