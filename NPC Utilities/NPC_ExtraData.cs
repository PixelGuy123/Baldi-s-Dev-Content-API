using System.Collections.Generic;
using UnityEngine;
using MTM101BaldAPI;
using MonoMod.Utils;
using System.Linq;

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
	/// <para>Basically it'll replace the usual Animator with an usable one for newly created NPCs</para>
	/// </summary>
	public class CustomNPC_Animator : MonoBehaviour
	{
		/// <summary>
		/// The NPC Field (DON'T touch it, you won't need it)
		/// </summary>
		public NPC Npc;
		/// <summary>
		/// The Renderer (DON'T touch it, you won't need it)
		/// </summary>
		public SpriteRenderer renderer;
		/// <summary>
		/// The current animation being played in the animator
		/// </summary>
		public string CurrentAnimation { get; private set; } = string.Empty;

		/// <summary>
		/// The animation speed
		/// </summary>
		public float AnimationSpeed { get => animatorSpeed; set => animatorSpeed = Mathf.Max(0f, value); } // Sets the animation speed here

		float animationTimer = 0f, animatorSpeed = 1f;

		Sprite[] animationSet = new Sprite[0];
		/// <summary>
		/// Here are the animations stored in a ScriptableObject (DON'T touch it, you won't need it)
		/// </summary>
		public AnimationsHolder Animations; // Omg, I had to make it public to not forget the reference, wtf. I hate this workaround with ScriptableObjects
		/// <summary>
		/// Gets all animations available in the Animator
		/// </summary>
		public string[] AllAnimations => Animations.Animations.Keys.ToArray();

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

		/// <summary>
		/// Sets the <paramref name="animation"/> by the name
		/// </summary>
		/// <param name="animation"></param>
		/// <returns>If it finds the animation, it returns True, otherwise False.</returns>
		public bool SetAnimation(string animation) // Safely sets the animation
		{
			if (Animations.Animations.ContainsKey(animation))
			{
				CurrentAnimation = animation;
				animationSet = Animations.Animations[animation];
				renderer.sprite = animationSet[0];
				animationTimer = 0f;
				return true;
			}
			return false;

		}

		public class AnimationsHolder : ScriptableObject // Yeah, to store this single field, the others aren't necessary since the npcs can do themselves in initialization
		{
			public Dictionary<string, Sprite[]> Animations = new Dictionary<string, Sprite[]>();
		}

	}
}
