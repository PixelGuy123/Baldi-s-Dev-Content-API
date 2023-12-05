using UnityEngine;
using BepInEx;
using HarmonyLib;
using MTM101BaldAPI.Registers;
using BaldiDevContentAPI.NPCs;
using MTM101BaldAPI.AssetManager;
using BaldiDevContentAPI.NPCs.Templates;
using BaldiDevContentAPI.Misc;
using System.Linq;
using System.IO;
using System.Collections.Generic;

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
				hihiha.Add("F1", NPCCreatorHelper.CreateNPC<WheelChair>(new CustomNPCAttributes("WheelChair",
					RoomUtilities.AllRooms.ToList(),
					3.5f,
					new Sprite[] { AssetManager.SpriteFromTexture2D(AssetManager.TextureFromMod(this, Path.Combine("npc", "template", "officechair.png")), new Vector2(0.5f, 0.5f), 50) }, false, true, true,
					"PST_OFC_Name", "PST_OFC_Desc",
					AssetManager.TextureFromMod(this, Path.Combine("npc", "template", "pri_ofc.png")), true, false)));
			}, false);

			GeneratorManagement.Register(this, GenerationModType.Addend, (string floorName, int levelNo, LevelObject ld) =>
			{
				foreach (var pair in hihiha)
				{
					if (pair.Key == floorName)
					{
						ld.potentialNPCs.Add(new WeightedNPC()
						{
							weight = 1000,
							selection = pair.Value
						});
						
					}
				}
			});

		}

		static Dictionary<string, NPC> hihiha = new Dictionary<string, NPC>();

		public static string ModPath { get; private set; }
	}

	internal static class ModInfo
	{
		public const string ModGUID = "pixelguy.pixelmodding.baldiplus.bdconapi";

		public const string ModName = "Baldi Dev Content API";

		public const string ModVersion = "0.0.0.1";
	}
}

