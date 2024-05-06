using BepInEx;
using HarmonyLib;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI.Registers;
using PlusLevelLoader;
using System.IO;
using UnityEngine;
using System.Collections.Generic;

namespace BaldiDevContentAPI
{
	[BepInPlugin(ModInfo.ModGUID, ModInfo.ModName, ModInfo.ModVersion)]
	public class BasePlugin : BaseUnityPlugin
	{
		void Awake()
		{

			Harmony harmony = new Harmony(ModInfo.ModGUID);

			
		}

	
	}
	internal static class ModInfo
	{
		public const string ModGUID = "pixelguy.pixelmodding.baldiplus.bdconapi";

		public const string ModName = "Baldi Dev Content API";

		public const string ModVersion = "0.0.0.1";
	}
}

