﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using WebSocketSharp.Server;
using Zenject;

namespace SaberQuest.Providers
{
	internal class AuthWebsocketProvider : IDisposable
	{
		private WebSocketServer server;
		private static AuthWebsocketProvider _instance;
		internal bool started = false;
		private Action<string> _recieveToken;
		public void Dispose()
		{
			started = false;
			server.Stop();
		}

		public void Initialize(Action<string> onTokenRecieved)
		{
			_instance = this;
			_recieveToken = onTokenRecieved;

			server = new WebSocketServer("ws://0.0.0.0:8080");
			server.AddWebSocketService<SocketBehavior>("/");
			server.Start();
			started = true;
		}

		private class SocketBehavior : WebSocketBehavior
		{
			protected override void OnMessage(MessageEventArgs e)
			{
				_instance._recieveToken(JsonConvert.DeserializeObject<TokenResult>(e.Data).ToString());
			}
			private class TokenResult
			{
				public string Token { get; set; }
			}
		}
	}
}
