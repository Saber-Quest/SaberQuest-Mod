using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using SaberQuest.Configuration;
using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.API.Data.Deals;
using SaberQuest.Utils;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace SaberQuest.UI.Components.Crafting
{
	//purely for list usage
	//NO VISUALS
	static class CraftItemListTableData
	{
		const string ReuseIdentifier = "REUSECraftItemListTableCell";

		public static CraftItemListTableCell GetCell(TableView tableView)
		{
			var tableCell = tableView.DequeueReusableCellForIdentifier(ReuseIdentifier);

			if (tableCell == null)
			{
				tableCell = new GameObject("CraftItemListTableCell", typeof(Touchable)).AddComponent<CraftItemListTableCell>();
				tableCell.interactable = true;

				tableCell.reuseIdentifier = ReuseIdentifier;
			}

			return (CraftItemListTableCell)tableCell;
		}
	}

	class CraftItemListTableCell : TableCell
	{
		//For the sole purpose of creating a new visual object when a reused cell is enabled
		internal Transform _itemParent;
		internal ItemModel _itemModel;
		internal CraftingCellSoftParentVisuals _linkedVisuals;

		public CraftItemListTableCell PopulateWithItemData(ItemModel item, CraftingCellSoftParentVisuals visuals, Transform itemParent)
		{
			_itemModel = item;
			_linkedVisuals = visuals;
			_itemParent = itemParent;
			return this;
		}

		public void OnDisable()
		{
			//TODO: Reuse visual cells
			GameObject.Destroy(_linkedVisuals.gameObject);
		}

		public void OnEnable()
		{
			if (!_linkedVisuals)
			{
				_linkedVisuals = CraftingCellSoftParentVisuals.GetVisualCell(_itemParent);
				_linkedVisuals.SetCell(this);
			}
		}
	}
}
