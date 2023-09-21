using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaberQuest.Models.SaberQuest.API.Data
{
    internal class ItemModel
    {
        public string Id { get; set; }
        public string Image { get; set; }
        public string Name { get; set; }
        public string Rarity { get; set; }
        public int Value { get; set; }
        public int Price { get; set; }

		internal bool usedInCrafting;
        internal int row = 0;
    }
}
