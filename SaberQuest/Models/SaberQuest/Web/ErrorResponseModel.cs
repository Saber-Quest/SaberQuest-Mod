using SiraUtil.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.Web
{
	internal class ErrorResponseModel
	{
		public ErrorResponseModel(IHttpResponse message)
		{
			Message = message;
		}

		internal IHttpResponse Message { get; set; }
	}
}
