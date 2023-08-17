using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SaberQuest.UI.Components.Crafting.IndividualCell
{
	internal class CraftItemCellUpdater : MonoBehaviour
	{
		internal CraftingCellSoftParentVisuals LinkedVisuals { get; set; }

		private void OnDisable()
		{
			if (LinkedVisuals != null)
				LinkedVisuals.gameObject.SetActive(false);
		}

		private void OnEnable()
		{
			if (LinkedVisuals != null)
			{
				LinkedVisuals.gameObject.SetActive(true);
				LinkedVisuals.transform.position = transform.position;
			}
		}
	}
}
