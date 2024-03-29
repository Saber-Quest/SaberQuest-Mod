﻿using HarmonyLib;
using IPA;
using IPA.Config;
using SaberQuest.HarmonyPatches;
using SaberQuest.Installers;
using SiraUtil.Zenject;
using System.Reflection;
using IPALogger = IPA.Logging.Logger;

namespace SaberQuest
{
	[Plugin(RuntimeOptions.DynamicInit), NoEnableDisable]
	public class Plugin
	{
		private static Harmony harmony = new Harmony("com.FutureMapper.SaberQuest");
		[Init]
		public void Init(IPALogger logger, Config config, Zenjector zenjector)
		{
			harmony.PatchAll(typeof(MenuButtonRedecoratePatch));
			zenjector.UseSiraSync();
			zenjector.Install<SaberQuestMenuInstaller>(Location.Menu);
			zenjector.UseLogger(logger);
			zenjector.UseMetadataBinder<Plugin>();
		}

		[OnStart]
		public void OnApplicationStart()
		{
		}

		[OnExit]
		public void OnApplicationExit()
		{
			harmony.UnpatchSelf();
		}
	}
}