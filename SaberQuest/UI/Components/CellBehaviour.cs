using BeatSaberMarkupLanguage.Components;
using HMUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
		private void Update()
		{
			Color targetColor;
			if (cell.selected)
				targetColor = selectedColor;
			else if (cell.highlighted)
				targetColor = highlightedColor;
			else
				targetColor = defaultColor;
			bg.color = Color.Lerp(bg.color, targetColor, Time.deltaTime * 5f);
		}
	}
}
