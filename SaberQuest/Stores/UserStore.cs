using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace SaberQuest.Stores
{
	internal class UserStore : Store<UserStore>
	{
		private UserModel CurrentUser { get; set; }

		public Result<UserModel> GetCurentUser()
		{
			if(CurrentUser != null)
			{
				return Result.Ok(CurrentUser);
			}
			else
			{
				return Result.Fail<UserModel>("User is not authenticated!");
			}
		}

		public void SetUser() => CurrentUser = ApiProvider.GetUser(76561198343533017, (err) => Logger.Error($"Failed to get user with token because of error: {err?.Message}"));
	}
}
