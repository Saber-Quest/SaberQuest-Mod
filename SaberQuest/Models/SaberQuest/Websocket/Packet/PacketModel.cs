using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.Websocket.Packet
{
    internal class PacketModel
    {
        public string EventName { get; set; }

        public string JsonPayLoad { get; set; }
    }
}
