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
		private readonly List<PoolItem> _cells = new List<PoolItem>();
		private Transform _parent;

		public void Initialize(Transform parent, int count = 12)
		{
			_parent = parent;
			for (int i = 0; i < count; i++)
			{
				NewCell();
			}
		}

		public CraftingCellSoftParentVisuals Get()
		{
			var cell = _cells.Find(x => !x.active);
			if(cell != null)
			{
				return cell.cell;
			}
			else
			{
				return NewCell().cell;
			}
		}

		public void Dequeue(CraftingCellSoftParentVisuals cell)
		{
			cell.gameObject.SetActive(false);
			var item = _cells.Find(x=>x.cell == cell);
			item.active = false;
		}

		private PoolItem NewCell()
		{
			var item = new PoolItem
			{
				cell = CraftingCellSoftParentVisuals.GetVisualCell(_parent),
				active = false
			};
			item.cell.gameObject.SetActive(false);

			_cells.Add(item);
			return item;
		}

		internal class PoolItem
		{
			internal CraftingCellSoftParentVisuals cell;
			internal bool active;
		}
	}
}
