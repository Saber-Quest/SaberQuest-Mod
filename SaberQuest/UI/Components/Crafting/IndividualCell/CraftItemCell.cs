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
using System;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace SaberQuest.UI.Components.Crafting.IndividualCell
{
    class CraftItemCell
    {
        internal ItemModel itemModel;
        internal CraftingCellSoftParentVisuals linkedVisuals;

        [UIObject("cell")] internal GameObject targetObject;

        public CraftItemCell PopulateWithItemData(ItemModel item, CraftingCellSoftParentVisuals visuals)
        {
            itemModel = item;
            linkedVisuals = visuals;
            return this;
        }

        [UIAction("#post-parse")]
        void Parsed()
        {
            var updater = targetObject.AddComponent<CraftItemCellUpdater>();
            updater.LinkedVisuals = linkedVisuals;
        }
    }
}
