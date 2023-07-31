using SaberQuest.Models.SaberQuest.Websocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp;
using Zenject;

namespace SaberQuest.Providers
{
	internal class SaberQuestWebSocketSubProvider
	{
		private WebSocket socket;

		[Inject]
		internal void Construct()
		{
			Console.WriteLine("x");
			socket = new WebSocket("ws://localhost:8080");
			socket.OnMessage += (sender, e) =>
			{
				PacketHandler packetHandler = new PacketHandler();
				PacketModel packet = packetHandler.OpenPacket(e.Data);
				Console.WriteLine(packet.EventName);
				Console.WriteLine(packet.JsonPayLoad);
			};
		}
	}
}
