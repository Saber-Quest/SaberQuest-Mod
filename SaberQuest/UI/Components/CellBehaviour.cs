using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using UnityEngine;

namespace SaberQuest.UI.Components
{
	internal class CellBehaviour : MonoBehaviour
	{
		private CustomCellTableCell cell;
		private ImageView bg;
		private Color selectedColor;
		private Color highlightedColor;
		private Color defaultColor;
		private bool gradient;
		private ImageView.GradientDirection gradientDirection;
		private RectTransform bgRect;
		private Vector2 startingMin;
		private Vector2 startingMax;

		internal void Construct(CustomCellTableCell cell, ImageView bg, Color selectedColor, Color highlightedColor, Color defaultColor, bool gradient, ImageView.GradientDirection gradientDirection)
		{
			this.cell = cell;
			this.bg = bg;
			this.selectedColor = selectedColor;
			this.highlightedColor = highlightedColor;
			this.defaultColor = defaultColor;
			this.gradient = gradient;
			this.gradientDirection = gradientDirection;
			bgRect = bg.transform as RectTransform;
			startingMin = bgRect.anchorMin;
			startingMax = bgRect.anchorMax;
		}

		private void Start()
		{
			bg.gradient = gradient;
			if (gradient)
			{
				bg.color0 = defaultColor;
				bg.color = Color.white;
				bg._gradientDirection = gradientDirection;
			}
		}
		private void Update()
		{
			Color targetColor0 = defaultColor;
			Color targetColor1;
			Vector2 targetAnchor = Vector2.zero;
			if (cell.selected)
			{
				targetColor1 = selectedColor;
				targetColor0 = highlightedColor;
				targetAnchor = new Vector2(0f, 0.1f);
			}
			else if (cell.highlighted)
				targetColor1 = highlightedColor;
			else
				targetColor1 = defaultColor;
			if (gradient)
			{
				bg.color1 = Color.Lerp(bg.color1, targetColor1, Time.deltaTime * 6f);
				bg.color0 = Color.Lerp(bg.color0, targetColor0, Time.deltaTime * 6f);
			}
			else
			{
				bg.color = Color.Lerp(bg.color, targetColor1, Time.deltaTime * 6f);
			}

			if(gradientDirection == ImageView.GradientDirection.Vertical)
			{
				bgRect.anchorMin = Vector2.Lerp(bgRect.anchorMin, startingMin + targetAnchor, Time.deltaTime * 6f);
				bgRect.anchorMax = Vector2.Lerp(bgRect.anchorMax, startingMax + targetAnchor, Time.deltaTime * 6f);
			}
		}
	}
}
