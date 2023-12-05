using System.Collections.Generic;
using UnityEngine;
using MTM101BaldAPI;
using MonoMod.Utils;

namespace BaldiDevContentAPI.NPCs
{
	/// <summary>
	/// Holds all the essential data from your NPC to be used in CreateNPC()
	/// <para>Note: Highly recommended to use CreateAttribute() to set all fields</para>
	/// </summary>
	public struct CustomNPCAttributes
	{
		/// <summary>
		/// Holds all the essential data from your NPC to be used in CreateNPC();
		///<para>Read each field to know their info.</para>
		/// </summary>
		public CustomNPCAttributes(string name, List<RoomCategory> spawnableRooms, Dictionary<string, Sprite[]> availableSprites, PosterObject poster, bool usesHeatMap = false, float colliderRadius = 3f, bool canEnterRooms = false, bool ignoreBelts = false, bool ignorePlayerOnSpawn = false, bool needsLooker = false)
		{
			Name = name;
			Character = EnumExtensions.ExtendEnum<Character>(name);
			SpawnableRooms = spawnableRooms;
			AvailableSprites = availableSprites;
			UsesHeatMap = usesHeatMap;
			ColliderRadius = colliderRadius;
			CanEnterRooms = canEnterRooms;
			IgnoreBelts = ignoreBelts;
			IgnorePlayerOnSpawn = ignorePlayerOnSpawn;
			NeedsLooker = needsLooker;

			Poster = poster;
		}

		/// <summary>
		/// Name of the NPC
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The Character Enum of the NPC
		/// </summary>
		public Character Character { get; set; }

		/// <summary>
		/// All the rooms the NPC is able of spawning at
		/// </summary>
		public List<RoomCategory> SpawnableRooms { get; set; }
		/// <summary>
		/// The collider radius of the NPC (default is 3)
		/// </summary>
		public float ColliderRadius { get; set; }
		/// <summary>
		/// All the sprites the NPC will use along side their keys (animations)
		/// </summary>
		public Dictionary<string, Sprite[]> AvailableSprites { get; set; }
		/// <summary>
		/// If the NPC is able to enter inside rooms
		/// </summary>
		public bool CanEnterRooms { get; set; }
		/// <summary>
		/// If the NPC uses the WanderRound() method, this must be set to true
		/// </summary>
		public bool UsesHeatMap { get; set; }
		/// <summary>
		/// If the NPC ignores being pushed by belts (Baldi for example)
		/// </summary>
		public bool IgnoreBelts { get; set; }
		/// <summary>
		/// If the NPC instantly spawns (like Gotta Sweep)
		/// </summary>
		public bool IgnorePlayerOnSpawn { get; set; }
		/// <summary>
		/// If the NPC needs the Looker component enabled (Chalkles don't need it, becoming disabled by default)
		/// </summary>
		public bool NeedsLooker { get; set; }
		/// <summary>
		/// The Office's Poster of the Character
		/// </summary>
		public PosterObject Poster { get; set; }
	}
	/// <summary>
	/// This will be automatically added to your NPC upon using CreateNPC()
	/// <para>Basically it'll hold some basic data that can't be normally held by the NPC class</para>
	/// </summary>
	public class CustomNPC_Animator : MonoBehaviour
	{
		public NPC Npc;

		public SpriteRenderer renderer;

		public string CurrentAnimation { get; private set; } = string.Empty;

		float animationTimer = 0f, animatorSpeed = 1f;

		Sprite[] animationSet = new Sprite[0];

		readonly Dictionary<string, Sprite[]> animations = new Dictionary<string, Sprite[]>();

		private void Update() // Animator basically
		{
			if (Npc == null || renderer == null || animationSet.Length == 0)
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
			if (animations.ContainsKey(animation))
			{
				CurrentAnimation = animation;
				animationSet = animations[animation];
				renderer.sprite = animationSet[0];
				animationTimer = 0f;
			}

		}

		public void SetupAnimations(Dictionary<string, Sprite[]> anims)
		{
			animations.Clear();
			animations.AddRange(anims);
		}
	}
}
