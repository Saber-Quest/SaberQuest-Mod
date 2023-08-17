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

namespace SaberQuest.UI.Components.Crafting.IndividualCell
{
    class CraftItemCell
    {
        //For the sole purpose of creating a new visual object when a reused cell is enabled
        internal Transform _itemParent;
        internal ItemModel _itemModel;
        internal CraftingCellSoftParentVisuals _linkedVisuals;

        internal bool crafting;

        [UIObject("cell")] internal GameObject targetObject;

        public CraftItemCell PopulateWithItemData(ItemModel item, CraftingCellSoftParentVisuals visuals, Transform itemParent)
        {
            _itemModel = item;
            _linkedVisuals = visuals;
            _itemParent = itemParent;
            return this;
        }

        [UIAction("#post-parse")]
        void Parsed()
        {
            var updater = targetObject.AddComponent<CraftItemCellUpdater>();
			updater.LinkedVisuals = _linkedVisuals;
		}
    }
}
