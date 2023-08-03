using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SaberQuest.Utils
{
	internal static class UIConsts
	{
		//Normal
		//Highlight
		//Selected
		internal static Dictionary<string, Color[]> DailyChallengeColorSet { get; } = new Dictionary<string, Color[]>
		{
			{
				"Easy", new Color[] {
					new Color(0f, 1f, 0.4f).ColorWithAlpha(0.6f),
					new Color(0f, 1f, 0,4f),
					new Color(0.6f, 0.75f, 1f)
				}
			},
			{
				"Normal", new Color[] {
					new Color(1f, 0.8f, 0.25f).ColorWithAlpha(0.6f),
					new Color(1f, 0.8f, 0.25f),
					new Color(0.6f, 0.75f, 1f)
				}
			},
			{
				"Hard", new Color[] {
					new Color(1f, 0f, 0f).ColorWithAlpha(0.6f),
					new Color(1f, 0f, 0f),
					new Color(0.6f, 0.75f, 1f)
				}
			},
			{
				"Extreme", new Color[] {
					new Color(0.6f, 0f, 1f).ColorWithAlpha(0.6f),
					new Color(0.6f, 0f, 1f),
					new Color(0.6f, 0.75f, 1f)
				}
			}
		};
	}
}
