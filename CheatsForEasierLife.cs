using HarmonyLib;
using System.Collections.Generic;

namespace BaldiDevContentAPI
{
	[HarmonyPatch(typeof(Baldi_Chase))]
	[HarmonyPatch("OnStateTriggerStay")]
	internal class Baldistop
	{
		static bool Prefix() // No ded
		{
			return false;
		}
	}

	[HarmonyPatch(typeof(HappyBaldi), "SpawnWait", MethodType.Enumerator)]
	internal class ChangeCount
	{
		private static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions) // Make count from 1
		{
			bool changedCount = false;
			using var enumerator = instructions.GetEnumerator();
			while (enumerator.MoveNext())
			{
				var instruction = enumerator.Current;
				if (!changedCount && instruction.Is(System.Reflection.Emit.OpCodes.Ldc_I4_S, 9))
				{
					changedCount = true;
					instruction.operand = 0;
				}
				yield return instruction;
			}
		}
	}

	[HarmonyPatch(typeof(PlayerMovement))]
	internal class KeyCheats
	{
		[HarmonyPatch("Start")]
		[HarmonyPostfix]
		private static void Speed(PlayerMovement __instance)
		{
			__instance.runSpeed *= 3;
			__instance.walkSpeed *= 3;
		}
	}

	// Actual Patches Below
}
