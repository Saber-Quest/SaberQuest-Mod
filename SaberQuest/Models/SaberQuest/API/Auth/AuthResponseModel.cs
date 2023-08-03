using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.API.Auth
{
	internal class AuthResponseModel
	{
		public string AccessToken { get; set; }
		public string NewRefreshToken { get; set; }
	}
}
