using BeatSaberMarkupLanguage;
using BeatSaberMarkupLanguage.MenuButtons;
using HarmonyLib;
using HMUI;
using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace SaberQuest.HarmonyPatches
{
    public static class MenuButtonRedecoratePatch
    {
        [HarmonyPatch(typeof(MenuButtonsViewController), nameof(MenuButtonsViewController.RefreshView))]
        [HarmonyPostfix]
        public static void Postfix(MenuButtonsViewController __instance)
        {
            Console.WriteLine('x');
            var button = __instance?.GetComponentsInChildren<TextMeshProUGUI>()?.Where(x => { Console.WriteLine(x.text); return x.text == "Saber Quest"; })?.FirstOrDefault().transform.parent;
            if (button != null)
            {
                var text = button.GetComponentInChildren<TextMeshProUGUI>();
                text.enableVertexGradient = true;
                text.colorGradient = new VertexGradient()
                {
                    topLeft = new Color(1f, 0.65f, 0.2f),
                    topRight = new Color(1f, 0.65f, 0.2f),
                    bottomLeft = new Color(0.8f, 0.63f, 0.5f),
                    bottomRight = new Color(0.8f, 0.63f, 0.5f)
                };

                CreateImage(button, "SaberQuest.UI.Resources.favicon.png");
            }
        }

        public static void CreateImage(Transform parent, string img)
        {
            var obj = new GameObject("SaberQuestIcon");
            obj.transform.SetParent(parent, false);
            var image = obj.AddComponent<ImageView>();
            image.SetImage(img);
        }
    }
}