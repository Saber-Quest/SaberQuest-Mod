using Newtonsoft.Json;
using SaberQuest.Models.SaberQuest.API.Data;
using System.Collections.Generic;
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
				Logger.Info(JsonConvert.SerializeObject(items));
			}, (err) =>
			{
				Logger.Error($"Failed to populate item store due to error: {err.Message}");
			});
		}

		public ItemModel GetItem(string id)
		{
			Logger.Info(id);
			var item = Items.Find(x => x.Id == id);
			Logger.Info(item.Id);
			return item;
		}
	}
}
