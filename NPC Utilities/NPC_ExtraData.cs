using System.Collections.Generic;
using UnityEngine;
using MTM101BaldAPI;

namespace BaldiDevContentAPI.NPCs
{
	/// <summary>
	/// Holds all the essential data from your NPC to be used in CreateNPC()
	/// <para>Note: Highly recommended to use CreateAttribute() to set all fields</para>
	/// </summary>
	public class CustomNPCAttributes : ScriptableObject
	{
		/// <summary>
		/// Holds all the essential data from your NPC to be used in CreateNPC()
		/// </summary>
		public static CustomNPCAttributes CreateAttribute(string name, List<RoomCategory> spawnableRooms, Dictionary<string, Sprite[]> availableSprites, PosterObject poster, bool usesHeatMap)
		{
			var attribute = CreateInstance<CustomNPCAttributes>();

			attribute.Name = name;
			attribute.Character = EnumExtensions.ExtendEnum<Character>(name);
			attribute.SpawnableRooms = spawnableRooms;
			attribute.AvailableSprites = availableSprites;
			attribute.UsesHeatMap = usesHeatMap;

			attribute.Poster = poster;

			attribute.name = $"{name}_Attributes";

			return attribute;
		}

		/// <summary>
		/// Name of the NPC
		/// </summary>
		public string Name { get; set; } = "CustomNPC";

		/// <summary>
		/// The Character Enum of the NPC
		/// </summary>
		public Character Character { get; set; } = Character.Null;

		/// <summary>
		/// All the rooms the NPC is able of spawning at
		/// </summary>
		public List<RoomCategory> SpawnableRooms { get; set; } = new List<RoomCategory>();
		/// <summary>
		/// The collider radius of the NPC (default is 3)
		/// </summary>
		public float ColliderRadius { get; set; } = 3f;
		/// <summary>
		/// All the sprites the NPC will use along side their keys (animations)
		/// </summary>
		public Dictionary<string, Sprite[]> AvailableSprites { get; set; } = new Dictionary<string, Sprite[]>();
		/// <summary>
		/// If the NPC is able to enter inside rooms
		/// </summary>
		public bool CanEnterRooms { get; set; } = false;
		/// <summary>
		/// If the NPC uses the WanderRound() method, this must be set to true
		/// </summary>
		public bool UsesHeatMap { get; set; } = false;
		/// <summary>
		/// If the NPC ignores being pushed by belts (Baldi for example)
		/// </summary>
		public bool IgnoreBelts { get; set; } = false;
		/// <summary>
		/// If the NPC instantly spawns (like Gotta Sweep)
		/// </summary>
		public bool IgnorePlayerOnSpawn { get; set; } = false;
		/// <summary>
		/// If the NPC needs the Looker component enabled (Chalkles don't need it, becoming disabled by default)
		/// </summary>
		public bool NeedsLooker { get; set; } = true;
		/// <summary>
		/// The Office's Poster of the Character
		/// </summary>
		public PosterObject Poster { get; set; } = default;
	}
	/// <summary>
	/// This will be automatically added to your NPC upon using CreateNPC()
	/// <para>Basically it'll hold some basic data that can't be normally held by the NPC class</para>
	/// </summary>
	public class CustomNPC_Animator : MonoBehaviour
	{
		public CustomNPCAttributes BaseAttributes;

		public NPC Npc;

		public SpriteRenderer renderer;

		public string CurrentAnimation { get; private set; } = string.Empty;

		float animationTimer = 0f, animatorSpeed = 1f;

		Sprite[] animationSet = new Sprite[0];

		private void Update() // Animator basically
		{
			if (Npc == null || renderer == null || BaseAttributes == null || animationSet.Length == 0)
				return;

			animationTimer += Npc.ec.NpcTimeScale * Time.deltaTime * animatorSpeed;
			int num = Mathf.FloorToInt(animationTimer);
			if (num >= animationSet.Length)
			{
				num = 0;
				animationTimer = 0f;
			}

			
			renderer.sprite = animationSet[num];

			

		}

		public float AnimationSpeed { get => animatorSpeed; set => animatorSpeed = Mathf.Max(0f, value); } // Sets the animation speed here

		public void SetAnimation(string animation)
		{
			if (BaseAttributes.AvailableSprites.ContainsKey(animation))
			{
				CurrentAnimation = animation;
				animationSet = BaseAttributes.AvailableSprites[animation];
				renderer.sprite = animationSet[0];
				animationTimer = 0f;
			}

		}
	}
}
