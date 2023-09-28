using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace SaberQuest.UI.Components.Crafting.IndividualCell
{
	//TODO: Migrate to zenject factory/pool some day
	internal class VisualCellPool : MonoBehaviour
	{
		private readonly Dictionary<CraftingCellSoftParentVisuals, bool> _cells = new Dictionary<CraftingCellSoftParentVisuals, bool>();
		private Transform _parent;

		public void Initialize(Transform parent, int count = 16)
		{
			_parent = parent;
			for (int i = 0; i < count; i++)
			{
				NewCell();
			}
		}

		public CraftingCellSoftParentVisuals Get()
		{
			var cell = _cells.FirstOrDefault(x => !x.Value);
			if(cell.Key == null)
			{
				cell = NewCell();
			}
			cell.Key.gameObject.SetActive(true);
			_cells[cell.Key] = true;
			return cell.Key;
		}

		public void Dequeue(CraftingCellSoftParentVisuals cell)
		{
			cell.gameObject.SetActive(false);
			_cells[cell] = false;
		}

		private KeyValuePair<CraftingCellSoftParentVisuals, bool> NewCell()
		{
			var cell = CraftingCellSoftParentVisuals.GetVisualCell(_parent);
			_cells.Add(cell, false);
			return _cells.First(x=>x.Key==cell);
		}
	}
}
