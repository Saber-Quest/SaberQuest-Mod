using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using BeatSaberMarkupLanguage.ViewControllers;
using HMUI;
using IPA.Utilities;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Providers.ApiProvider;
using SaberQuest.UI.Components.DailyChallenges;
using SaberQuest.Utils;
using SiraUtil.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using Zenject;

namespace SaberQuest.UI.SaberQuest.Views
{
    [HotReload(RelativePathToLayout = @"DailyChallengesView.bsml")]
    [ViewDefinition("SaberQuest.UI.SaberQuest.Views.DailyChallengesView.bsml")]
    internal class DailyChallengesView : BSMLAutomaticViewController, TableView.IDataSource
    {
        //Dependencies
        private ISaberQuestApiProvider _apiProvider;
        private SiraLog _logger;

        //Challenge List
        [UIComponent("challengesList")] private CustomListTableData list = null;
        private TableView challengesList => list?.tableView;

        //Data
        private List<ChallengeModel> challenges = new List<ChallengeModel>();
        private ChallengeSetModel _selectedChallengeSet;
        private ChallengeModel _selectedChallenge;

        //Right Side
        private DailyChallengesColorController _colorController;
        [UIObject("difficultyText")] private GameObject _difficultyText;
        [UIObject("challengeContainer")] private GameObject _challengeContainer;
        [UIObject("descText")] private GameObject _descText;
        [UIObject("valueText")] private GameObject _valueText;

        [UIValue("date")]
        private string CurrentDate => DateTime.Now.ConvertWithSuffix("MMMM dnn, yyyy", true);

        [Inject]
        private void Construct(ISaberQuestApiProvider apiProvider, SiraLog siraLog)
        {
            _apiProvider = apiProvider;
            _logger = siraLog;
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            if (gameObject.GetComponent<Touchable>() == null)
                gameObject.AddComponent<Touchable>();

            challengesList.SetDataSource(this, false);

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

            _colorController = _challengeContainer.AddComponent<DailyChallengesColorController>();
        }

        [UIAction("select-challenge")]
        internal void SelectChallenge(TableView view, int row)
        {
            ApplyChallenge(challenges[row]);
        }

        internal void ApplyChallenge(ChallengeModel challenge)
        {
            _selectedChallenge = challenge;
            _difficultyText.GetComponent<TextMeshProUGUI>().text = challenge.Name;
            if (UIConsts.DailyChallengeColorSet.ContainsKey(challenge.Name))
            {
                _colorController.targetColor = UIConsts.DailyChallengeColorSet[challenge.Name][1];
            }
            _descText.GetComponent<TextMeshProUGUI>().text = _selectedChallengeSet.Description;
            _valueText.GetComponent<TextMeshProUGUI>().text = challenge.Challenge;

        }

        internal void ApplyChallengeSet(ChallengeSetModel challengeSet)
        {
            _selectedChallengeSet = challengeSet;
            HMMainThreadDispatcher.instance.Enqueue(() =>
            {
                _logger.Info("hola");
                _logger.Info(challengeSet.Difficulties.Count);
                if (!(challengeSet?.Difficulties?.Count > 0))
                {
                    _logger.Error("No Daily Challenges Found... Do you need to update?");
                    return;
                }
                challenges = challengeSet.Difficulties;
                challengesList.ReloadData();
                challengesList.SelectCellWithIdx(0);
                ApplyChallenge(challengeSet.Difficulties[0]);
            });
        }

        public float CellSize() => 14.4f;

        public int NumberOfCells() => challenges.Count;

        public TableCell CellForIdx(TableView tableView, int idx) => DailyChallengesListTableData.GetCell(tableView).PopulateWithChallengeData(challenges[idx], _selectedChallengeSet);
    }
}
