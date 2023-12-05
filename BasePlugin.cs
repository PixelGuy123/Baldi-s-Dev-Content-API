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
				string npcTemplatePath = Path.Combine(ModPath, "npc", "template"); // Templates

				var attributes = CustomNPCAttributes.CreateAttribute("WheelChair", RoomUtilities.AllRooms.ToList(), 
					NPCUtilities.CreateAnimationSprites("normal", 50, Path.Combine(npcTemplatePath, "stronado_normal1.png"), Path.Combine(npcTemplatePath, "stronado_normal2.png")),
					PosterUtilities.CreateDefaultNPCPosterObject("PST_OFC_Name", "PST_OFC_Desc", AssetManager.TextureFromFile(Path.Combine(npcTemplatePath, "pri_ofc.png"))),
					false
					);

				hihiha.Add("F1", NPCCreatorHelper.CreateNPC<WheelChair>(attributes));
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

		readonly static Dictionary<string, NPC> hihiha = new Dictionary<string, NPC>();

		public static string ModPath { get; private set; }
	}

	internal static class ModInfo
	{
		public const string ModGUID = "pixelguy.pixelmodding.baldiplus.bdconapi";

		public const string ModName = "Baldi Dev Content API";

		public const string ModVersion = "0.0.0.1";
	}
}

