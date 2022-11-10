using HarmonyLib;
using System;

namespace NGUnidle
{
    public class BossPatches
    {
        /// Increase GRB respawn speed
        [HarmonyPatch(typeof(AdventureController), "boss1SpawnTime")]
        [HarmonyPostfix]
        static void PatchBoss1(ref float __result)
        {
            __result = __result / Settings.bossMulti;
        }

        /// Increase GCT respawn speed
        [HarmonyPatch(typeof(AdventureController), "boss2SpawnTime")]
        [HarmonyPostfix]
        static void PatchBoss2(ref float __result)
        {
            __result = __result / Settings.bossMulti;
        }

        /// Increase boss 3 respawn speed
        [HarmonyPatch(typeof(AdventureController), "boss3SpawnTime")]
        [HarmonyPostfix]
        static void PatchBoss3(ref float __result)
        {
            __result = __result / Settings.bossMulti;
        }

        /// Increase boss 4 respawn speed
        [HarmonyPatch(typeof(AdventureController), "boss4SpawnTime")]
        [HarmonyPostfix]
        static void PatchBoss4(ref float __result)
        {
            __result = __result / Settings.bossMulti;
        }

        /// Increase boss 5 respawn speed
        [HarmonyPatch(typeof(AdventureController), "boss5SpawnTime")]
        [HarmonyPostfix]
        static void PatchBoss5(ref float __result)
        {
            __result = __result / Settings.bossMulti;
        }

        /// Increase boss 6 respawn speed
        [HarmonyPatch(typeof(AdventureController), "boss6SpawnTime")]
        [HarmonyPostfix]
        static void PatchBoss6(ref float __result)
        {
            __result = __result / Settings.bossMulti;
        }

        /// Decrease GRB exp
        [HarmonyPatch(typeof(AdventureController), "boss1Exp")]
        [HarmonyPostfix]
        static void PatchBoss1Exp(ref long __result)
        {
            __result = Convert.ToInt64(__result / Settings.bossExpDiv);
        }

        /// Decrease GCT exp
        [HarmonyPatch(typeof(AdventureController), "boss2Exp")]
        [HarmonyPostfix]
        static void PatchBoss2Exp(ref long __result)
        {
            System.Console.WriteLine("Before:" + __result);
            __result = Convert.ToInt64(__result / Settings.bossExpDiv);
            System.Console.WriteLine("After:" + __result);
        }

        /// Decrease boss 3 exp
        [HarmonyPatch(typeof(AdventureController), "boss3Exp")]
        [HarmonyPostfix]
        static void PatchBoss3Exp(ref long __result)
        {
            __result = Convert.ToInt64(__result / Settings.bossExpDiv);
        }

        /// Decrease boss 4 exp
        [HarmonyPatch(typeof(AdventureController), "boss4Exp")]
        [HarmonyPostfix]
        static void PatchBoss4Exp(ref long __result)
        {
            __result = Convert.ToInt64(__result / Settings.bossExpDiv);
        }

        /// Decrease boss 5 exp
        [HarmonyPatch(typeof(AdventureController), "boss5Exp")]
        [HarmonyPostfix]
        static void PatchBoss5Exp(ref long __result)
        {
            __result = Convert.ToInt64(__result / Settings.bossExpDiv);
        }

        /// Decrease boss 6 exp
        [HarmonyPatch(typeof(AdventureController), "boss6Exp")]
        [HarmonyPostfix]
        static void PatchBoss6Exp(ref long __result)
        {
            __result = Convert.ToInt64(__result / Settings.bossExpDiv);
        }
    }
}
