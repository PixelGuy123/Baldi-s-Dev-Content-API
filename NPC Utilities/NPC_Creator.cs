using System.Collections.Generic;
using UnityEngine;
using MTM101BaldAPI.Reflection;
using MTM101BaldAPI;

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
		public static C CreateNPC<C>(CustomNPCAttributes attributes) where C : NPC // Credits to the OldSport's mod menu code, half of it defines specifically what I need to make these npcs from scratch
		{
			var npc = new GameObject(attributes.Name,
				typeof(C),
				typeof(Navigator),
				typeof(CharacterController),
				typeof(ActivityModifier),
				typeof(NavigatorDebugger),
				typeof(Looker),
				typeof(AudioManager),
				typeof(AudioSource),
				typeof(CapsuleCollider),
				typeof(Rigidbody),
				typeof(CustomNPC_DataHolder)
			).GetComponent<C>();

			npc.gameObject.SetActive(false); // Basic stuff
			Object.DontDestroyOnLoad(npc.gameObject);

			// Getting some variables held for usage

			var looker = npc.GetComponent<Looker>();
			var nav = npc.GetComponent<Navigator>();
			var rigidBody = npc.GetComponent<Rigidbody>();
			var col = npc.GetComponent<CapsuleCollider>();
			var control = npc.GetComponent<CharacterController>();

			// ------------- GameObject Setup Basically --------------
			// Most of it is from OldSport's code, so here is the credit!


			col.isTrigger = true; // Sets as a trigger duh
			// Reminder: set capsule collider from attributes here

			// Setup Character Control
			control.slopeLimit = 0f;
			control.stepOffset = 0f;
			control.skinWidth = 0.0001f;
			control.minMoveDistance = 0.001f;


			// Setup Rigid Body
			rigidBody.useGravity = false;
			rigidBody.isKinematic = true;
			rigidBody.constraints = RigidbodyConstraints.FreezeAll;
			rigidBody.angularDrag = 0f;

			npc.gameObject.tag = "NPC";
			npc.gameObject.layer = LayerMask.NameToLayer("NPCs"); // DID THIS METHOD ALWAYS EXISTED?? OMG

			// ----------- Setup Visual ----------

			var spriteBase = new GameObject("SpriteBase");

			var sprite = new GameObject("Sprite",
				typeof(SpriteRenderer),
				typeof(Billboard)
			)
			{
				layer = LayerMask.NameToLayer("Billboard")
			};

			spriteBase.transform.SetParent(npc.transform);
			sprite.transform.SetParent(spriteBase.transform); // As always, sprite inside a "spriteBase"

			// ------------- Setup NPC Fields -------------

			npc.baseTrigger = new Collider[] { col };
			npc.looker = looker;

			npc.spawnableRooms = attributes.SpawnableRooms;
			npc.spriteBase = spriteBase;
			npc.spriteRenderer = new SpriteRenderer[] { sprite.GetComponent<SpriteRenderer>() };
			npc.spriteRenderer[0].material = Character.Beans.GetFirstInstance().spriteRenderer[0].material; // Let's all trust this works

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
			looker.ReflectionSetVariable("layerMask", new LayerMask() 
			{
				value = LayerMask.GetMask("Default", "Player", "Windows", "Block Raycast") // Forgot an integer can't be a layer mask
			}); // Another method I didn't know it existed
			looker.ReflectionSetVariable("npc", npc);

			// ---------- Final Touch ----------

			col.radius = attributes.ColliderRadius; // Set Radius
			npc.ReflectionSetVariable("ignoreBelts", attributes.IgnoreBelts); // Sets some other private variables
			npc.ReflectionSetVariable("ignorePlayerOnSpawn", attributes.IgnorePlayerOnSpawn);
			npc.ReflectionSetVariable("poster", attributes.Poster);
			npc.ReflectionSetVariable("character", attributes.Character);

			looker.enabled = attributes.NeedsLooker;
			npc.GetComponent<AudioManager>().audioDevice = npc.GetComponent<AudioSource>();

			if (attributes.AvailableSprites.Length > 0)
				sprite.GetComponent<SpriteRenderer>().sprite = attributes.AvailableSprites[0];

			var holder = npc.GetComponent<CustomNPC_DataHolder>();
			holder.BaseAttributes = attributes;
			holder.Npc = npc;

			

			return npc;
		}




		/* This code is for later
			 if (ec != null)
			{
				npc.ec = ec; // Sets Ec
				ec.Npcs.Add(npc); // Adds npc to the Ec
				npc.players = new List<PlayerManager>(Object.FindObjectsOfType<PlayerManager>()); // Since it is during runtime, it also need the players
			}
			if (ec != null) // If it isn't null, give the ec already
						nav.ec = ec;
		*/
	}
}
