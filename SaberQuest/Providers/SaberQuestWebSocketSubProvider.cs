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
            Console.WriteLine("x");
            //PacketHandler packetHandler = new PacketHandler();
            //PacketModel packet = packetHandler.OpenPacket(e.Data);
            //Console.WriteLine(packet.EventName);
            //Console.WriteLine(packet.JsonPayLoad);

            using (var ws = new WebSocket("ws://localhost:8080"))
            {
                ws.OnMessage += (sender, e) =>
                                  Console.WriteLine("Laputa says: " + e.Data);

                ws.Connect();
            }
        }
    }
}
