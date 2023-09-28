using Newtonsoft.Json;
using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.Utils;
using System.Collections.Generic;
using Zenject;

namespace SaberQuest.Stores
{
	internal class RecipeStore : Store<RecipeStore>
	{
		private Dictionary<string, List<RecipeLookupResult>> Recipes { get; } = new Dictionary<string, List<RecipeLookupResult>>();
		[Inject]
		private void Construct()
		{
			ApiProvider.GetAllRecipes((recipes) =>
			{
				Recipes.Clear();
				foreach (var recipe in recipes)
				{
					var item1LookupResult = new RecipeLookupResult()
					{
						ComplimentaryCraftItem = recipe.Item2Id,
						ResultingItem = recipe.CraftedId
					};
					var item2LookupResult = new RecipeLookupResult()
					{
						ComplimentaryCraftItem = recipe.Item1Id,
						ResultingItem = recipe.CraftedId
					};

					if (Recipes.ContainsKey(recipe.Item1Id))
						Recipes[recipe.Item1Id].Add(item1LookupResult);
					else
						Recipes.Add(recipe.Item1Id, new List<RecipeLookupResult>() { item1LookupResult });

					if (Recipes.ContainsKey(recipe.Item2Id))
						Recipes[recipe.Item2Id].Add(item2LookupResult);
					else
						Recipes.Add(recipe.Item2Id, new List<RecipeLookupResult>() { item2LookupResult });
				}
			}, (err) => Logger.Error($"Failed to populate item store due to error: {err.Message}"));
		}

		public Result<List<RecipeLookupResult>> GetRecipes(string item)
		{
			Logger.Info(item);
			if(Recipes.TryGetValue(item, out var result))
			{
				return new Result<List<RecipeLookupResult>>(result, true);
			}
			else
			{
				return Result.Fail<List<RecipeLookupResult>>("Item is not able to be used for crafting");
			}
		}

		public struct RecipeLookupResult
		{
			public string ComplimentaryCraftItem { get; set; }
			public string ResultingItem { get; set; }
		}
	}
}
