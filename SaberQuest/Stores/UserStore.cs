using SaberQuest.Models.SaberQuest.API.Data;
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
		public UserModel CurrentUser { get; private set; }

		public void SetUser(int user) => CurrentUser = ApiProvider.GetUser(user, (err) => Logger.Error($"Failed to get user: {user} with error: {err?.Message}"));
	}
}
