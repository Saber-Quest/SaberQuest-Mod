using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using SaberQuest.Configuration;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Utils;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace SaberQuest.UI.Components.DailyChallenges
{
    static class DailyChallengesListTableData
    {
        const string ReuseIdentifier = "REUSEDailyChallengesListTableCell";

        public static DailyChallengesListTableCell GetCell(TableView tableView)
        {
            var tableCell = tableView.DequeueReusableCellForIdentifier(ReuseIdentifier);

            if (tableCell == null)
            {
                tableCell = new GameObject("DailyChallengesListTableCell", typeof(Touchable)).AddComponent<DailyChallengesListTableCell>();
                tableCell.interactable = true;

                tableCell.reuseIdentifier = ReuseIdentifier;
                BSMLParser.instance.Parse(
                    Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "SaberQuest.UI.Components.DailyChallenges.DailyChallengeCell.bsml"),
                    tableCell.gameObject, tableCell
                );
            }

            return (DailyChallengesListTableCell)tableCell;
        }
    }

    class DailyChallengesListTableCell : TableCell
    {
        internal ChallengeModel _challengeModel;
        internal ChallengeSetModel _challengeSet;

        [UIObject("cell")] private readonly GameObject cell;
        [UIComponent("difficulty-text")] readonly TextMeshProUGUI difficultyText = null;
        [UIComponent("short-name-text")] readonly TextMeshProUGUI shortNameText = null;

        private ImageView bg;

        [UIAction("#post-parse")]
        void Parsed()
        {
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
        }

        public DailyChallengesListTableCell PopulateWithChallengeData(ChallengeModel challenge, ChallengeSetModel challengeSet)
        {
            _challengeModel = challenge;
            _challengeSet = challengeSet;

            if (!UIConsts.DailyChallengeColorSet.TryGetValue(_challengeModel.Name, out Color[] colorSet))
            {
                colorSet = UIConsts.DailyChallengeColorSet["Easy"];
            }

            if (cell.GetComponent<CellBehaviour>() == null)
            {
                var behaviour = cell.AddComponent<CellBehaviour>();
                behaviour.enabled = false;
                behaviour.Construct(this, bg, colorSet[2], colorSet[1], colorSet[0], true, 0);
                behaviour.enabled = true;
            }

            difficultyText.text = _challengeModel.Name;
            shortNameText.text = _challengeSet.ShortName;

            return this;
        }
    }
}
