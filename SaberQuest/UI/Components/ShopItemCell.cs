﻿using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.API.Data.Deals;
using SaberQuest.Utils;
using System;
using System.Linq;
using UnityEngine;

namespace SaberQuest.UI.Components
{
	internal class ShopItemCell
	{
		internal readonly DealModel _dealModel;

		[UIObject("cell")]
		private readonly GameObject cell;
		private ImageView bg;

		private string Name => _dealModel.Name;
		private string ImageUrl => _dealModel.ImageURL;
		internal ShopItemCell(DealModel deal)
		{
			_dealModel = deal;
		}

		[UIAction("#post-parse")]
		internal void PostParse()
		{
			CustomCellTableCell tableCell = cell.GetComponentInParent<CustomCellTableCell>();
			foreach (var x in cell.GetComponentsInChildren<Backgroundable>().Select(x => x.GetComponent<ImageView>()))
			{
				if (!x || x.color0 != Color.white || x.sprite.name != "RoundRect10")
					continue;

				x.SetField("_skew", 0f);
				x.overrideSprite = null;
				x.SetImage("#RoundRect10BorderFade");
				x.color = new Color(0.25f, 0.25f, 1f, 0.4f);
				bg = x;
			}

			if (!UIConsts.RarityColors.TryGetValue(_dealModel.Rarity, out Color defaultColor))
			{
				defaultColor = UIConsts.RarityColors["Common"];
			}
			if (cell.GetComponent<CellBehaviour>() == null)
			{
				var behaviour = cell.AddComponent<CellBehaviour>();
				behaviour.enabled = false;
				behaviour.Construct(tableCell, bg, new Color(0.8f, 0.3f, 1f), defaultColor * 1.2f, defaultColor, true, ImageView.GradientDirection.Vertical);
				behaviour.enabled = true;
			}
		}
	}
}