using Newtonsoft.Json;
using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.Utils;
using System.Collections.Generic;
using System.Linq;
using Zenject;

namespace SaberQuest.Stores
{
	internal class ItemStore : Store<ItemStore>
	{
		private List<ItemModel> Items { get; } = new List<ItemModel>();
		[Inject]
		private void Construct()
		{
			ApiProvider.GetAllItems((items) =>
			{
				Items.Clear();
				Items.AddRange(items);
			}, (err) =>
			{
				Logger.Error($"Failed to populate item store due to error: {err.Message}");
			});
		}

		public Result<ItemModel> GetItem(string id)
		{
			var item = Items.Find(x => x.Id == id);
			if(item != null)
			{
				return Result.Ok(item);
			}
			else
			{
				return Result.Fail<ItemModel>("Item does not exist!");
			}
		}

		public List<ItemModel> GetItems() => Items;
	}
}
