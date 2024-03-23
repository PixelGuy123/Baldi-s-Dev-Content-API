using BepInEx;
using HarmonyLib;
using MTM101BaldAPI.AssetTools;
using MTM101BaldAPI.Registers;
using PlusLevelLoader;
using PlusLevelFormat;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using System.Net;

namespace BaldiDevContentAPI
{
	[BepInPlugin(ModInfo.ModGUID, ModInfo.ModName, ModInfo.ModVersion)]
	public class BasePlugin : BaseUnityPlugin
	{
		void Awake()
		{

			Harmony harmony = new Harmony(ModInfo.ModGUID);

			Logger.LogInfo($"{ModInfo.ModName} {ModInfo.ModVersion} has been initialized! Made by PixelGuy");

			ModPath = AssetLoader.GetModPath(this);

			harmony.PatchAll();

			

			string path = Path.Combine(ModPath, "room.cbld");
			if (!File.Exists(path))
			{
				Debug.LogWarning("BALDIDEVAPI: Failed to grab room data as it doesn't exist");
				return;
			}

			

			LoadingEvents.RegisterOnAssetsLoaded(() =>
			{
				using var binaryStream = new BinaryReader(File.OpenRead(path));
				{
					level = binaryStream.ReadLevel();
					var obj = CustomLevelLoader.LoadLevel(level);

					asset = obj.levelAsset;

					for (int i = 0; i < obj.levelAsset.rooms.Count; i++)
					{
						var room = obj.levelAsset.rooms[i];
						if (room.category != RoomCategory.Class)
							continue;
						
						var asset = ScriptableObject.CreateInstance<RoomAsset>();
						if (room.activity == null)
						{
							room.activity = new ActivityData();
							room.hasActivity = false;
						}
						room.ConvertToAsset(asset, new IntVector2(0, 0));
						foreach (var c in obj.levelAsset.tile)
						{
							if (c.roomId == i)
								asset.cells.Add(c);
						}
						rooms.Add(asset);
					}
					
				}
			}, false);

			GeneratorManagement.Register(this, GenerationModType.Base, (name, num, ld) =>
			{
				if (name == "F1")
				{
					ld.potentialClassRooms = ld.potentialClassRooms.AddRangeToArray(rooms.ConvertAll(x => new WeightedRoomAsset() { selection = x, weight = 999 }).ToArray());
				}
			});

			
		}

		static Level level;

		readonly List<RoomAsset> rooms = new List<RoomAsset>();

		static LevelAsset asset;

		public static string ModPath { get; private set; }
	}

	internal static class ModInfo
	{
		public const string ModGUID = "pixelguy.pixelmodding.baldiplus.bdconapi";

		public const string ModName = "Baldi Dev Content API";

		public const string ModVersion = "0.0.0.1";
	}
}

