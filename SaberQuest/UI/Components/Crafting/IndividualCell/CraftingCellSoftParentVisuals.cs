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

        private Color normal = Color.white.ColorWithAlpha(0.4f);
        private Color highlight = new Color(0.25f, 0.4f, 0.5f).ColorWithAlpha(1.0f);

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

			var images = GetComponentsInChildren<Backgroundable>().Select(x => x.GetComponent<ImageView>());
			foreach (var x in images)
			{
				if (!x || x.color0 != Color.white || x.sprite.name != "RoundRect10")
                    continue;

                x._skew = 0f;
                x.overrideSprite = null;
                x.SetImage("#RoundRect10BorderFade");
                x.color = new Color(1f, 1f, 1f, 0.4f);
                bg = x;
            }
			foreach (var x in images)
			{
				if ((x != null) && x.sprite.name == "RoundRect10")
				{
					x.color0 = new Color(0.5f, 0.25f, 1f);
					x.color1 = new Color(0.23f, 0f, 0.58f);
				}
                x._skew = 0f;
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
				float lerpValue = Time.deltaTime * 7f;
				Vector3 targetPosition = target.transform.position;

                //Plane position
				if (_linkedCell.crafting)
				{
					transform.position = Vector3.Lerp(transform.position, targetPosition, lerpValue);
				}
				else
				{
					transform.position = new Vector3(
						Mathf.Lerp(transform.position.x, targetPosition.x, lerpValue),
						targetPosition.y,
						transform.position.z);
				}
                //Z offset
				float targetZ = selectable.isPointerInside && !_linkedCell.crafting ? targetPosition.z - 0.08f : targetPosition.z;
				var position = transform.position;
                position.z = Mathf.Lerp(position.z, targetZ, lerpValue);
                transform.position = position;

                //Look Rotation
                var camera = Camera.current;
                var x = Quaternion.LookRotation(transform.position - camera.transform.position);
                transform.rotation = Quaternion.Slerp(transform.rotation, selectable.isPointerInside ? x : Quaternion.identity, lerpValue);
            }
            if (bg != null && selectable != null)
            {
                bg.color = Color.Lerp(bg.color, selectable.isPointerInside ? highlight : normal, Time.deltaTime * 6f);
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
