using HarmonyLib;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;

namespace BaldiDevContentAPI.Debug
{
	[HarmonyPatch(typeof(Baldi))]
	[HarmonyPatch("OnTriggerEnter")]
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

	// Actual Patches Below
}
