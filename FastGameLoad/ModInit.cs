using BepInEx;
using HarmonyLib;
using System.Collections;

namespace FastGameLoad
{
    [BepInPlugin("bepinex.plugin.fastgameload", "FastGameLoad", "0.1.0")]
    public class FastGameLoad : BaseUnityPlugin
    {
        public static FastGameLoad instance;
		public static BepInEx.Logging.ManualLogSource Log;
        private void Awake()
        {
            instance = this;
			Log = this.Logger;

            var harmony = new Harmony("com.fastgameload");
            Harmony.CreateAndPatchAll(typeof(FastGameLoad));

            Logger.LogInfo($"Plugin is loaded!");
        }
		
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StartManager), "Start")]
        public static IEnumerator Start_Manager_Start_Postfix(IEnumerator __result, StartManager __instance)
        {
			__instance.startManagerAnimator.speed = float.MaxValue;
			FastGameLoad.Log.LogInfo("START ANIM IS FAST NOW, finally...");

			while(__result.MoveNext()) yield return __result.Current;
        }
    }
}
