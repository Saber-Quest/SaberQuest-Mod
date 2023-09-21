using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.Attributes;
using BeatSaberMarkupLanguage.Components;
using HMUI;
using IPA.Utilities;
using SaberQuest.Configuration;
using SaberQuest.Models.SaberQuest.API.Data.Challenges;
using SaberQuest.Models.SaberQuest.API.Data.Deals;
using SaberQuest.Stores;
using SaberQuest.Utils;
using System.Linq;
using System.Reflection;
using TMPro;
using UnityEngine;

namespace SaberQuest.UI.Components.Shop
{
    static class ShopItemListTableData
    {
        const string ReuseIdentifier = "REUSEShopItemListTableCell";

        public static ShopItemListTableCell GetCell(TableView tableView)
        {
            var tableCell = tableView.DequeueReusableCellForIdentifier(ReuseIdentifier);

            if (tableCell == null)
            {
                tableCell = new GameObject("ShopItemListTableCell", typeof(Touchable)).AddComponent<ShopItemListTableCell>();
                tableCell.interactable = true;

                tableCell.reuseIdentifier = ReuseIdentifier;
                BSMLParser.instance.Parse(
                    Utilities.GetResourceContent(Assembly.GetExecutingAssembly(), "SaberQuest.UI.Components.Shop.ShopItemCell.bsml"),
                    tableCell.gameObject, tableCell
                );
            }

            return (ShopItemListTableCell)tableCell;
        }
    }

    class ShopItemListTableCell : TableCell
    {
        internal DealModel _dealModel;

        [UIObject("cell")] private readonly GameObject cell;
        [UIComponent("image")] readonly ImageView image = null;
        [UIComponent("name-text")] readonly TextMeshProUGUI nameText = null;

        private ImageView bg;

        [UIAction("#post-parse")]
        void Parsed()
        {
            foreach (var x in cell.GetComponentsInChildren<Backgroundable>().Select(x => x.GetComponent<ImageView>()))
            {
                if (!x || x.color0 != Color.white || x.sprite.name != "RoundRect10")
                    continue;

                x.SetField("_skew", 0f);
                x.overrideSprite = null;
                x.SetImage("#RoundRect10BorderFade");
                x.color = new Color(0.25f, 0.25f, 1f, 0.4f);
                bg = x;
            }
        }

        public ShopItemListTableCell PopulateWithShopItemData(DealModel deal)
        {
            _dealModel = deal;
            var item = ItemStore.Get().GetItem(_dealModel.Id);

            if (!UIConsts.RarityColors.TryGetValue(item.Rarity, out Color defaultColor))
            {
                defaultColor = UIConsts.RarityColors["Common"];
            }

            if (cell.GetComponent<CellBehaviour>() == null)
            {
                var behaviour = cell.AddComponent<CellBehaviour>();
                behaviour.enabled = false;
                behaviour.Construct(this, bg, new Color(0.8f, 0.3f, 1f), defaultColor * 1.2f, defaultColor, true, ImageView.GradientDirection.Vertical);
                behaviour.enabled = true;
            }

            image.SetImage(item.Image);
            nameText.text = item.Name;

            return this;
        }
    }
}
