﻿using HMUI;
using UnityEngine;

namespace SaberQuest.UI.Components.DailyChallenges
{
    internal class DailyChallengesColorController : MonoBehaviour
    {
        internal Color targetColor;
        private ImageView image;

        private void Start()
        {
            image = GetComponent<ImageView>();
        }

        private void Update()
        {
            image.color = Color.Lerp(image.color, targetColor, Time.deltaTime * 8f);
        }
    }
}
