using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;

namespace MyFirstPlugin
{
    public static class Settings
    {
        private static ConfigEntry<float> configTimeMulti;
        private static ConfigEntry<float> configBossMulti;
        private static ConfigEntry<float> configWandoosMulti;
        private static ConfigEntry<float> configYggdrasil;
        private static ConfigEntry<bool> configAdvTraining;

        public static float timeMulti => configTimeMulti.Value;
        public static float bossMulti => configBossMulti.Value;
        public static float wandoosMulti => configWandoosMulti.Value;
        public static float yggdrasil => configYggdrasil.Value;
        public static bool advTraining => configAdvTraining.Value;

        public static void InitSettings(ConfigFile Config)
        {
            Settings.configTimeMulti = Config.Bind(
                "Multipliers",
                "TimeMulti",
                2.0f,
                "Multiplies the rebirth time in NUMBER calculations");

            Settings.configBossMulti = Config.Bind(
                "Multipliers",
                "BossMulti",
                4.0f,
                "Multiplies the respawn speed of bosses");

            Settings.configWandoosMulti = Config.Bind(
                "Multipliers",
                "WandoosMulti",
                2.0f,
                "Multiplies the startup speed of Wandoos");

            Settings.configYggdrasil = Config.Bind(
                "Multipliers",
                "YggdrasilMulti",
                4.0f,
                "Multiplies the growth speed of fruits");

            Settings.configAdvTraining = Config.Bind(
                "Features",
                "AlwaysUnlockAdvTraining",
                true,
                "Unlocks advanced training at the start of rebirth");
        }
    }


    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Settings.InitSettings(Config);
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Harmony.CreateAndPatchAll(typeof(Plugin));
        }

        /// Increase time multiplier speed
        [HarmonyPatch(typeof(Rebirth), "calculateTimeMulti")]
        [HarmonyPrefix]
        static bool PatchTimeMultiPrefix(Rebirth __instance, ref double __state)
        {
            var character = __instance.character;
            __state = character.rebirthTime.totalseconds;
            var newVal = __state * Settings.timeMulti;

            if (newVal >= 3600)
            {
                var remainder = newVal - 3600;
                newVal = 3600 + remainder / Settings.timeMulti;
            }
            character.rebirthTime.setTime(
                newVal
            );
            return true;
        }

        [HarmonyPatch(typeof(Rebirth), "calculateTimeMulti")]
        [HarmonyPostfix]
        static void PatchTimeMultiPostfix(Rebirth __instance, double __state)
        {
            __instance.character.rebirthTime.setTime(__state);
        }

        /// Increase GRB respawn speed
        [HarmonyPatch(typeof(AdventureController), "boss1SpawnTime")]
        [HarmonyPrefix]
        static bool PatchBoss1(ref float __result)
        {
            __result = 3600f / Settings.bossMulti;
            return false;
        }

        /// Increase GCT respawn speed
        [HarmonyPatch(typeof(AdventureController), "boss2SpawnTime")]
        [HarmonyPrefix]
        static bool PatchBoss2(ref float __result)
        {
            __result = 3600f / Settings.bossMulti;
            return false;
        }

        /// Increase boss 3 respawn speed
        [HarmonyPatch(typeof(AdventureController), "boss3SpawnTime")]
        [HarmonyPostfix]
        static void PatchBoss3(ref float __result)
        {
            __result = __result / Settings.bossMulti;
        }

        /// Increase wandoos bootup speed
        [HarmonyPatch(typeof(Wandoos98Controller), "wandoosBootupTime")]
        [HarmonyPostfix]
        static void PatchWandoos(ref float __result)
        {
            __result = __result / Settings.wandoosMulti;
        }

        /// Unlock advanced training earlier
        [HarmonyPatch(typeof(AllAdvancedTraining), "advancedTrainingUnlocked")]
        [HarmonyPrefix]
        static bool PatchAdvTraining(ref bool __result)
        {
            if (Settings.advTraining)
            {
                __result = true;
                return false;
            }
            return true;
        }

        /// Unlock advanced training earlier
        [HarmonyPatch(typeof(ButtonShower), "updateButtons")]
        [HarmonyPostfix]
        static void PatchAdvTrainingMenu(ButtonShower __instance)
        {
            if (Settings.advTraining && !__instance.advancedTraining.interactable)
            {
                var text = Traverse.Create(__instance).Field("advancedTrainingText").GetValue<UnityEngine.UI.Text>();
                text.text = "Adv. Training";
                __instance.advancedTraining.interactable = true;
            }
        }

        /// Unlock advanced training earlier
        [HarmonyPatch(typeof(AdvancedTrainingController), "updateAdvancedTraining")]
        [HarmonyPrefix]
        static void PatchAdvTrainingUpdate(AdvancedTrainingController __instance)
        {
            if (__instance.getDivisor() == 0f)
            {
                return;
            }
            if (__instance.character.advancedTraining.energy[__instance.id] <= 0L && __instance.character.wishes.wishes[190].level <= 0)
            {
                return;
            }
            if (__instance.hitTarget())
            {
                __instance.character.advancedTrainingController.advanceEnergy(__instance.id);
                return;
            }
            __instance.character.advancedTraining.barProgress[__instance.id] += __instance.progressPerTick();
            __instance.updateBar();
            if (__instance.character.advancedTraining.barProgress[__instance.id] >= 1f)
            {
                __instance.character.advancedTraining.barProgress[__instance.id] = 0f;
                if (!__instance.character.canLevel())
                {
                    return;
                }
                __instance.character.advancedTraining.level[__instance.id] += 1L;
                __instance.character.settings.rebirthLevels += 1L;
                __instance.updateText();
            }
        }

        /// Make yggdrasil faster
        [HarmonyPatch(typeof(FruitController), "tierThreshold")]
        [HarmonyPostfix]
        static void PatchYggdrasil(ref float __result)
        {
            __result = __result / Settings.yggdrasil;
        }
    }
}
