using BeatSaberMarkupLanguage.Components;
using HMUI;
using UnityEngine;

namespace SaberQuest.UI.Components
{
	internal class CellBehaviour : MonoBehaviour
	{
		internal CustomCellTableCell cell;
		internal ImageView bg;
		internal Color selectedColor;
		internal Color highlightedColor;
		internal Color defaultColor;
		internal bool gradient;

		private void Start()
		{
			bg.gradient = gradient;
			if (gradient)
			{
				bg.color0 = defaultColor;
				bg.color = Color.white;
			}
		}
		private void Update()
		{
			Color targetColor0 = defaultColor;
			Color targetColor1;
			if (cell.selected)
			{
				targetColor1 = selectedColor;
				targetColor0 = highlightedColor;
			}
			else if (cell.highlighted)
				targetColor1 = highlightedColor;
			else
				targetColor1 = defaultColor;
			if (gradient)
			{
				bg.color1 = Color.Lerp(bg.color1, targetColor1, Time.deltaTime * 5f);
				bg.color0 = Color.Lerp(bg.color0, targetColor0, Time.deltaTime * 5f);
			}
			else
			{
				bg.color = Color.Lerp(bg.color, targetColor1, Time.deltaTime * 5f);
			}
		}
	}
}
