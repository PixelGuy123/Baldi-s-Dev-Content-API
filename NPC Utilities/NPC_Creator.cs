using System.Collections.Generic;
using UnityEngine;
using MTM101BaldAPI.Reflection;
using MTM101BaldAPI;
using System;
using System.Linq;

namespace BaldiDevContentAPI.NPCs
{
	/// <summary>
	/// The helper is the main class to help you thorugh creating npcs and overall everything you need for them
	/// </summary>
	public static class NPCCreatorHelper
	{
		/// <summary>
		/// Creates the NPC given the <typeparamref name="C"/> as the NPC type for the mod's usage. 
		/// <para>Reminder: they are disabled by default to not return errors since they are meant to be initialized by the generator.</para>
		/// <para>It requires the <paramref name="attributes"/> to properly create the NPC</para>
		/// </summary>
		/// <typeparam name="C"></typeparam>
		/// <returns>Returns the new created NPC disabled and on DontDestroyOnLoad scene</returns>
		private static C Internal_CreateNPC<C>(CustomNPCAttributes attributes) where C : NPC
		{
			var cloneBeans = (Beans)UnityEngine.Object.Instantiate(Character.Beans.GetFirstInstance());

			UnityEngine.Object.Destroy(cloneBeans.GetComponent<Animator>()); // Removes ANIMATOR

			var npc = cloneBeans.gameObject.AddComponent<C>(); // Adds the actual necessary component

			UnityEngine.Object.Destroy(cloneBeans); // Removes the Beans component

			// Getting some variables held for usage

			var looker = npc.GetComponent<Looker>();
			var nav = npc.GetComponent<Navigator>();
			var col = npc.GetComponent<CapsuleCollider>();
			var control = npc.GetComponent<CharacterController>();

			var spriteBase = npc.transform.GetChild(0);
			var sprite = spriteBase.GetChild(0);

			// ------------- Setup NPC Fields -------------

			npc.baseTrigger = new Collider[] { col };
			npc.looker = looker;

			npc.spawnableRooms = attributes.SpawnableRooms;
			npc.spriteBase = spriteBase.gameObject;
			npc.spriteRenderer = new SpriteRenderer[] { sprite.GetComponent<SpriteRenderer>() };


			npc.ReflectionSetVariable("navigator", nav); // Refers the navigator

			// ------------ Setup Other Fields -----------

			// Setup Navigator
			nav.npc = npc;
			
			nav.ReflectionSetVariable("cc", control); // Character Controller Field
			nav.ReflectionSetVariable("collider", col); // Collider
			nav.ReflectionSetVariable("am", npc.GetComponent<ActivityModifier>());
			nav.ReflectionSetVariable("avoidRooms", !attributes.CanEnterRooms);
			nav.ReflectionSetVariable("useHeatMap", attributes.UsesHeatMap);

			// Setup Looker
			looker.ReflectionSetVariable("npc", npc);

			// ---------- Final Touch ----------

			col.radius = attributes.ColliderRadius; // Set Radius
			npc.ReflectionSetVariable("ignoreBelts", attributes.IgnoreBelts); // Sets some other private variables
			npc.ReflectionSetVariable("ignorePlayerOnSpawn", attributes.IgnorePlayerOnSpawn);
			npc.ReflectionSetVariable("poster", attributes.Poster);
			npc.ReflectionSetVariable("character", attributes.Character);

			looker.enabled = attributes.NeedsLooker;

			var holder = npc.gameObject.AddComponent<CustomNPC_Animator>();
			holder.Npc = npc;
			holder.renderer = sprite.GetComponent<SpriteRenderer>();
			holder.SetupAnimations(attributes.AvailableSprites);

			npc.name = attributes.Name;

			return npc;
		}

		/// <summary>
		/// Creates the NPC given the <typeparamref name="C"/> as the NPC type for the mod's usage. 
		/// <para>Reminder: they are enabled by default to not return errors since they are meant to be initialized by the generator.</para>
		/// <para>It requires the <paramref name="attributes"/> to properly create the NPC</para>
		/// <para></para>
		/// </summary>
		/// <typeparam name="C"></typeparam>
		/// <returns>Returns the new created NPC disabled and on DontDestroyOnLoad scene</returns>
		public static C CreateNPC<C>(CustomNPCAttributes attributes) where C : NPC
		{
			var npc = Internal_CreateNPC<C>(attributes);
			npc.gameObject.SetActive(false); // Basic stuff
			UnityEngine.Object.DontDestroyOnLoad(npc.gameObject);
			return npc;
		}

		/// <summary>
		/// Creates the NPC given the <typeparamref name="C"/> as the NPC type for the mod's usage. 
		/// <para>Reminder: they are enabled by default</para>
		/// <para>It requires the <paramref name="attributes"/> to properly create the NPC, with a <paramref name="ec"/> to not return errors and requires a <paramref name="spawnPoint"/></para>
		/// <para></para>
		/// </summary>
		/// <typeparam name="C"></typeparam>
		/// <returns>Returns the new created NPC on the current game with a set spawnPoint</returns>

		public static C CreateNPC<C>(CustomNPCAttributes attributes, EnvironmentController ec, Vector3 spawnPoint) where C : NPC
		{
			if (ec == null)
				throw new ArgumentNullException($"The EnvironmentController given had a null reference or wasn\'t set to an instance of an object");

			var npc = Internal_CreateNPC<C>(attributes);

			npc.ec = ec; // Sets Ec
			ec.Npcs.Add(npc); // Adds npc to the Ec
			npc.players = new List<PlayerManager>(UnityEngine.Object.FindObjectsOfType<PlayerManager>()); // Since it is during runtime, it also need the players
			npc.GetComponent<Navigator>().ec = ec;
			npc.transform.SetParent(ec.transform);

			npc.transform.position = spawnPoint;

			return npc;

		}


	}
}
