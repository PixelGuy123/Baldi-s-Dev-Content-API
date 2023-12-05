using System.Collections.Generic;
using UnityEngine;
using MTM101BaldAPI;
using BaldiDevContentAPI.Misc;

namespace BaldiDevContentAPI.NPCs
{
	/// <summary>
	/// Holds all the essential data from your NPC to be used in CreateNPC()
	/// <para>Note: All the fields are required by the constructor</para>
	/// </summary>
	public struct CustomNPCAttributes
	{
		/// <summary>
		/// Holds all the essential data from your NPC to be used in CreateNPC()
		/// <para>Note: All the fields are required by the constructor</para>
		/// <para><paramref name="name"/> is the name of the NPC (that also includes to the Character enum of it), <paramref name="spawnableRooms"/> are the rooms it can spawn, <paramref name="colliderRadius"/> is how large are their collider</para>
		/// <para><paramref name="availableSprites"/> are all the sprites the npc will need, <paramref name="ignoreBelts"/> if it wants to ignore belts (like Baldi), <paramref name="ignorePlayerSpawn"/> if it spawns instantly (like Gotta Sweep), <paramref name="keepLookerEnabled"/> if you want the looker component to be enabled (is disabled by characters like Chalkles, since it is unused), <paramref name="poster"/> is the NPC's poster</para>
		/// <para><paramref name="canEnterRooms"/> if the NPC is able to get inside rooms, <paramref name="usesHeatMap"/> for NPCs who wandersRound (like Principal)</para>
		/// </summary>
		public CustomNPCAttributes(string name, List<RoomCategory> spawnableRooms, float colliderRadius, Sprite[] availableSprites, bool ignoreBelts, bool ignorePlayerSpawn, bool keepLookerEnabled, PosterObject poster, bool canEnterRooms, bool usesHeatMap)
		{
			Name = name;
			Character = EnumExtensions.ExtendEnum<Character>(name);
			SpawnableRooms = spawnableRooms;
			ColliderRadius = colliderRadius;
			AvailableSprites = availableSprites;
			IgnoreBelts = ignoreBelts;
			IgnorePlayerOnSpawn = ignorePlayerSpawn;
			NeedsLooker = keepLookerEnabled;
			CanEnterRooms = canEnterRooms;
			UsesHeatMap = usesHeatMap;

			Poster = poster;
		}

		/// <summary>
		/// Holds all the essential data from your NPC to be used in CreateNPC()
		/// <para>Note: All the fields are required by the constructor</para>
		/// <para><paramref name="name"/> is the name of the NPC (that also includes to the Character enum of it), <paramref name="spawnableRooms"/> are the rooms it can spawn, <paramref name="colliderRadius"/> is how large are their collider</para>
		/// <para><paramref name="availableSprites"/> are all the sprites the npc will need, <paramref name="ignoreBelts"/> if it wants to ignore belts (like Baldi), <paramref name="ignorePlayerSpawn"/> if it spawns instantly (like Gotta Sweep), <paramref name="keepLookerEnabled"/> if you want the looker component to be enabled (is disabled by characters like Chalkles, since it is unused)</para>
		/// <para><paramref name="canEnterRooms"/> if the NPC is able to get inside rooms, <paramref name="usesHeatMap"/> for NPCs who wandersRound (like Principal)</para>
		/// <para><paramref name="posterNameKey"/> is the NPC's name in the poster, <paramref name="posterDescKey"/> is the NPC's description in the poster, <paramref name="posterVisual"/> for the visual of the poster</para>
		/// </summary>
		public CustomNPCAttributes(string name, List<RoomCategory> spawnableRooms, float colliderRadius, Sprite[] availableSprites, bool ignoreBelts, bool ignorePlayerSpawn, bool keepLookerEnabled, string posterNameKey, string posterDescKey, Texture2D posterVisual, bool canEnterRooms, bool usesHeatMap)
		{
			Name = name;
			Character = EnumExtensions.ExtendEnum<Character>(name);
			SpawnableRooms = spawnableRooms;
			ColliderRadius = colliderRadius;
			AvailableSprites = availableSprites;
			IgnoreBelts = ignoreBelts;
			IgnorePlayerOnSpawn = ignorePlayerSpawn;
			NeedsLooker = keepLookerEnabled;
			CanEnterRooms = canEnterRooms;
			UsesHeatMap = usesHeatMap;

			var data = PosterUtilities.CopyPosterTextData(Character.Beans.GetFirstInstance().Poster.textData);
			data[0].textKey = posterNameKey;
			data[1].textKey = posterDescKey;

			Poster = ObjectCreatorHandlers.CreatePosterObject(posterVisual, data);
		}

		public string Name { get; set; }

		public Character Character { get; set; }

		public List<RoomCategory> SpawnableRooms { get; set; }

		public float ColliderRadius { get; set; }

		public Sprite[] AvailableSprites { get; set; }

		public bool CanEnterRooms { get; set; }

		public bool UsesHeatMap { get; set; }

		public bool IgnoreBelts { get; set; }

		public bool IgnorePlayerOnSpawn { get; set; }

		public bool NeedsLooker { get; set; }

		public PosterObject Poster { get; set; }
	}
	/// <summary>
	/// This will be automatically added to your NPC upon using CreateNPC()
	/// <para>Basically it'll hold some basic data that can't be normally held by the NPC class</para>
	/// </summary>
	public class CustomNPC_DataHolder : MonoBehaviour
	{
		public CustomNPCAttributes BaseAttributes = default;

		public NPC Npc;
	}
}
