using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

namespace SaberQuest.UI.Components.Crafting.IndividualCell
{
    internal class CraftingCellSoftParentVisuals : MonoBehaviour
    {
        private CraftItemCell _linkedCell;
        private GameObject _overrideObject;

        private ImageView bg;

        private Selectable selectable;

        internal static CraftingCellSoftParentVisuals GetVisualCell(Transform itemsParent)
        {
            var cell = new GameObject("CraftingCellSoftParentVisuals");
            cell.transform.SetParent(itemsParent, false);
            var visuals = cell.AddComponent<CraftingCellSoftParentVisuals>();

            var layout = cell.AddComponent<LayoutElement>();
            layout.preferredWidth = 19f;
            layout.preferredHeight = 30f;

            (cell.transform as RectTransform).sizeDelta = new Vector2(19f, 30f);

			BSMLParser.instance.Parse(
                    Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "SaberQuest.UI.Components.Crafting.IndividualCell.CraftingCell.bsml"),
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

            selectable = gameObject.AddComponent<Selectable>();
            selectable.targetGraphic = bg;
        }

        internal void SetCell(CraftItemCell cell)
        {
            _linkedCell = cell;
        }

        private void Update()
        {
            if (_linkedCell != null && _linkedCell.targetObject != null)
            {
                var target = _overrideObject ? _overrideObject : _linkedCell.targetObject;
                transform.position = _linkedCell.crafting ? Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * 7f) : new Vector3(Mathf.Lerp(transform.position.x, target.transform.position.x, Time.deltaTime * 7f), target.transform.position.y, target.transform.position.z);
            }
            if (bg != null && selectable != null)
            {
                bg.color = Color.Lerp(bg.color, selectable.isPointerInside ? Color.red : Color.white.ColorWithAlpha(0.4f), Time.deltaTime * 6f);
            }
        }

        private void OnEnable()
        {
            if (_linkedCell != null && _linkedCell.targetObject != null)
            {
                var target = _overrideObject ? _overrideObject : _linkedCell.targetObject;
                transform.position = target.transform.position;
            }
		}
    }
}
