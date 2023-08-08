using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.Websocket.Types
{
    internal class ItemBought
    {
        public string UserId { get; set; }
        public string Item { get; set; }
        public int QP { get; set; }
        public int Value { get; set; }
    }
}
