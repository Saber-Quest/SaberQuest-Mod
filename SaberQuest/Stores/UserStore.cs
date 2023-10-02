using SaberQuest.Models.SaberQuest.API.Data;
using SaberQuest.Utils;
using System;
using System.Collections.Generic;
using System.Drawing.Text;
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

		public void SetUser(Action<UserModel> OnResult, Action OnError)
		{
			ApiProvider.GetUser(-1, (x) =>
			{
				CurrentUser = x;
				OnResult(x);
			}, (err) =>
			{
				OnError();
				Logger.Error($"Failed to get user with token because of error: {err?.ToString()}");
			});
		}
	}
}
