using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Config.Data;
using IPA.Utilities;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Providers;
using SaberQuest.UI.Components;
using SiraUtil.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;
using static IPA.Logging.Logger;

namespace SaberQuest.UI.SaberQuest.Views
{
	[HotReload(RelativePathToLayout = @"DailyChallengesView.bsml")]
	[ViewDefinition("SaberQuest.UI.SaberQuest.Views.DailyChallengesView.bsml")]
	internal class DailyChallengesView : BSMLAutomaticViewController
	{
		//Dependencies
		private SaberQuestApiProvider _apiProvider;
		private SiraLog _logger;

		//Challenge List
		[UIComponent("challengesList")]
		private CustomCellListTableData list = null;
		[UIValue("challenges")]
		private List<object> challenges = new List<object>();

		[Inject]
		private void Construct(SaberQuestApiProvider apiProvider, SiraLog siraLog)
		{
			_apiProvider = apiProvider;
			_logger = siraLog;
		}

		[UIAction("#post-parse")]
		internal void PostParse()
		{
			if (gameObject.GetComponent<Touchable>() == null)
				gameObject.AddComponent<Touchable>();
			foreach (var x in GetComponentsInChildren<Backgroundable>().Select(x => x.GetComponent<ImageView>()))
			{
				if (!x || x.color0 != Color.white || x.sprite.name != "RoundRect10" || x.transform.childCount < 1)
					continue;
				var firstChild = x.transform.GetChild(0);
				Color targetColor = new Color(1f, 1f, 1f, 0.4f);
				if (firstChild != null)
				{

					var childText = firstChild.GetComponent<TextMeshProUGUI>();
					if (childText != null && childText.text.StartsWith("#color"))
						ColorUtility.TryParseHtmlString(childText.text.Replace("#color", ""), out targetColor);
				}

				ReflectionUtil.SetField(x, "_skew", 0f);
				x.overrideSprite = null;
				x.SetImage("#RoundRect10BorderFade");
				x.color = targetColor;
			}
			if (list != null)
			{
				_apiProvider.GetDailyChallenges((x) =>
				{
					ApplyChallengeSet(x);
				}, async (error) =>
				{
					//TODO: Show Error Modal here
					_logger.Error(await error.Message.Error());
				});
			}
		}

		[UIAction("select-challenge")]
		internal void SelectChallenge()
		{

		}

		internal void ApplyChallenge(ChallengeModel challenge)
		{

		}

		internal void ApplyChallengeSet(ChallengeSetModel challengeSet)
		{
			_logger.Info("hola");
			_logger.Info(challengeSet.Challenges.Count);
			if (!(challengeSet?.Challenges?.Count > 0))
			{
				_logger.Error("No Daily Challenges Found... Do you need to update?");
				return;
			}
			challenges = challengeSet.Challenges.ConvertAll(x => (object)new DailyChallengeCell(x));
			list.data = challenges;
			list.tableView.ReloadData();
			list.tableView.SelectCellWithIdx(0);
			ApplyChallenge(challengeSet.Challenges[0]);
		}
	}
}
