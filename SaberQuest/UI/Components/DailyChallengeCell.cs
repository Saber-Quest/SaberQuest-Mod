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
		internal readonly ChallengeModel _challengeModel;
		private readonly ChallengeSetModel _challengeSet;

		[UIObject("cell")]
		private readonly GameObject cell;
		private ImageView bg;

		private string Difficulty => _challengeModel.Name;
		private string ShortName => _challengeSet.ShortName;
		internal DailyChallengeCell(ChallengeModel challenge, ChallengeSetModel challengeSet)
		{
			_challengeModel = challenge;
			_challengeSet = challengeSet;
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

			if (!UIConsts.DailyChallengeColorSet.TryGetValue(_challengeModel.Name, out Color[] colorSet))
			{
				colorSet = UIConsts.DailyChallengeColorSet["Easy"];
			}

			if (cell.GetComponent<CellBehaviour>() == null)
			{
				var behaviour = cell.AddComponent<CellBehaviour>();
				behaviour.enabled = false;
				behaviour.Construct(tableCell, bg, colorSet[2], colorSet[1], colorSet[0], true, 0);
				behaviour.enabled = true;
			}
		}
	}
}
