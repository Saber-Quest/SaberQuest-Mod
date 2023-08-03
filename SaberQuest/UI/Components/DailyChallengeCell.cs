using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Utils;
using System.Linq;
using UnityEngine;

namespace SaberQuest.UI.Components
{
	internal class DailyChallengeCell
	{
		private readonly ChallengeModel _challengeModel;

		[UIObject("cell")]
		private readonly GameObject cell;
		private ImageView bg;

		internal DailyChallengeCell(ChallengeModel challenge)
		{
			_challengeModel = challenge;
		}

		[UIAction("#post-parse")]
		internal void PostParse()
		{
			/*CustomCellTableCell tableCell = cell.GetComponentInParent<CustomCellTableCell>();
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

			if (!UIConsts.DailyChallengeColorSet.TryGetValue(_challengeModel.Difficulty, out Color[] colorSet))
			{
				colorSet = UIConsts.DailyChallengeColorSet["Easy"];
			}

			if (cell.GetComponent<CellBehaviour>() == null)
			{
				var behaviour = cell.AddComponent<CellBehaviour>();
				behaviour.enabled = false;
				behaviour.bg = bg;
				behaviour.cell = tableCell;
				behaviour.defaultColor = colorSet[0];
				behaviour.highlightedColor = colorSet[1];
				behaviour.selectedColor = colorSet[2];
				behaviour.enabled = true;
			}*/
		}
	}
}
