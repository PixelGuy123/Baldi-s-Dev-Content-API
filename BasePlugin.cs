using UnityEngine;
using BepInEx;
using HarmonyLib;
using MTM101BaldAPI.Registers;
using BaldiDevContentAPI.NPCs;
using MTM101BaldAPI.AssetManager;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace BaldiDevContentAPI
{
	[BepInPlugin(ModInfo.ModGUID, ModInfo.ModName, ModInfo.ModVersion)]
	public class BasePlugin : BaseUnityPlugin
	{
		void Awake()
		{

			Harmony harmony = new Harmony(ModInfo.ModGUID);

			Logger.LogInfo($"{ModInfo.ModName} {ModInfo.ModVersion} has been initialized! Made by PixelGuy");

			ModPath = AssetManager.GetModPath(this);

			harmony.PatchAll();

			LoadingEvents.RegisterOnAssetsLoaded(() =>
			{
				var pickMode = FindObjectsOfType<CursorInitiator>(true).First(c => c.name == "PickMode");
				pickMode.transform.Find("MainNew").GetComponent<TextLocalizer>().key = "MyName69"; // Here's the key, I put MyName69 since invalid keys just return themselves by default
				pickMode.transform.Find("MainContinue").GetComponent<TextLocalizer>().key = "MyName69";

			}, false);

		}

		readonly static Dictionary<string, NPC> hihiha = new Dictionary<string, NPC>();

		readonly static List<CustomNPCAttributes> AttributesMade = new List<CustomNPCAttributes>(); // This is to make npcs during the game, so I can just re-use the info inside here (Only for UE)

		public static string ModPath { get; private set; }
	}

	internal static class ModInfo
	{
		public const string ModGUID = "pixelguy.pixelmodding.baldiplus.bdconapi";

		public const string ModName = "Baldi Dev Content API";

		public const string ModVersion = "0.0.0.1";
	}
}

