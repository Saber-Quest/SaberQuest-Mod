using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.Websocket.Types
{
    internal class UserUpdate
    {
        public string UserId { get; set; }
        public string Type { get; set; }
        public List<string> Collectibles { get; set; }
        public int Value { get; set; }
    }
}
