using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace SaberQuest.UI.Components.Crafting
{
    internal class CraftingCellSoftParentVisuals : MonoBehaviour
    {
        private CraftItemListTableCell _linkedCell;
        private GameObject _overrideObject;

        private ImageView bg;

        private Selectable selectable;

        internal static CraftingCellSoftParentVisuals GetVisualCell(Transform itemsParent)
        {
            var cell = new GameObject("CraftingCellSoftParentVisuals");
            cell.transform.SetParent(itemsParent, false);
            var visuals = cell.AddComponent<CraftingCellSoftParentVisuals>();

            BSMLParser.instance.Parse(
                    Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "SaberQuest.UI.Components.Crafting.CraftingCell.bsml"),
                    cell, visuals);

            return visuals;
        }

        [UIAction("#post-parse")]
        internal void PostParse()
        {
            if (gameObject.GetComponent<Touchable>() == null)
                gameObject.AddComponent<Touchable>();

            foreach (var x in GetComponentsInChildren<Backgroundable>().Select(x => x.GetComponent<ImageView>()))
            {
                if (!x || x.color0 != Color.white || x.sprite.name != "RoundRect10")
                    continue;

                x._skew = 0f;
                x.overrideSprite = null;
                x.SetImage("#RoundRect10BorderFade");
                x.color = new Color(1f, 1f, 1f, 0.4f);
                bg = x;
            }

            (transform as RectTransform).sizeDelta = new Vector2(15f, 15f);

            selectable = gameObject.AddComponent<Selectable>();
            selectable.targetGraphic = bg;
        }

        internal void SetCell(CraftItemListTableCell cell)
        {
            _linkedCell = cell;
        }

        private void Update()
        {
            if (_linkedCell != null)
            {
                var target = _overrideObject ? _overrideObject : _linkedCell.gameObject;
                transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * 7f);
            }
            if (bg != null && selectable != null)
            {
                bg.color = Color.Lerp(bg.color, selectable.isPointerInside ? Color.red : Color.white.ColorWithAlpha(0.4f), Time.deltaTime * 6f);
            }
        }
    }
}
