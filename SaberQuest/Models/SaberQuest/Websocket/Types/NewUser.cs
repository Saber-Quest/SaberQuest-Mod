﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.Websocket.Types
{
	internal class NewUser
	{
		public string UserId { get; set; }
		public string Link { get; set; }
	}
}