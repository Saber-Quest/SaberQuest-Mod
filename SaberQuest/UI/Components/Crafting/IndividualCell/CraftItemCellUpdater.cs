using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SaberQuest.UI.Components.Crafting.IndividualCell
{
	internal class CraftItemCellUpdater : MonoBehaviour, IPointerEnterHandler
	{
		internal CraftingCellSoftParentVisuals LinkedVisuals { get; set; }

		public void OnPointerEnter(PointerEventData eventData)
		{
			Console.WriteLine("Breh");
		}

		private void OnDisable()
		{
		}

		private void OnEnable()
		{
			if (LinkedVisuals != null)
			{
				LinkedVisuals.transform.position = transform.position;
			}
		}
	}
}
