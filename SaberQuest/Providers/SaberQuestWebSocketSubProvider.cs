using SaberQuest.Models.SaberQuest.Websocket.Packet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using WebSocketSharp;
using Zenject;

namespace SaberQuest.Providers
{
    internal class SaberQuestWebSocketSubProvider : MonoBehaviour
    {
        private WebSocket socket;

        public void Start()
        {
            Console.WriteLine("Starting Websicjet");
			//PacketHandler packetHandler = new PacketHandler();
			//PacketModel packet = packetHandler.OpenPacket(e.Data);
			//Console.WriteLine(packet.EventName);
			//Console.WriteLine(packet.JsonPayLoad);

			var socket = new WebSocket("ws://saberquest.xyz:8080");


			socket.OnMessage += (sender, e) =>
			{
				Console.WriteLine(e.Data);
				PacketHandler packetHandler = new PacketHandler();
				PacketModel packet = packetHandler.OpenPacket(e.Data);
				Console.WriteLine(packet.EventName);
				Console.WriteLine(packet.JsonPayLoad);
			};

			socket.Connect();
		}
    }
}
