using BepInEx;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using System.Reflection;
using HarmonyLib;
using Unity.Mathematics;
using Newtonsoft.Json;
using GlobalEnums;
using HutongGames.PlayMaker.Actions;
using GenericVariableExtension;
using HutongGames.PlayMaker;
using Unity.Burst.Intrinsics;
using UnityEngine.ResourceManagement.AsyncOperations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace FastGameLoad
{
    [BepInPlugin("bepinex.plugin.FastGameLoad", "FastGameLoad", "0.1.0")]
    public class FastGameLoad : BaseUnityPlugin
    {
        public static FastGameLoad instance;
		public static bool isAnimFast = false;
		public static BepInEx.Logging.ManualLogSource Log;
        private void Awake()
        {
            instance = this;
			Log = this.Logger;

            var harmony = new Harmony("com.fastgamestartup");
            Harmony.CreateAndPatchAll(typeof(FastGameLoad));

            Logger.LogInfo($"Plugin is loaded!");
        }
		
        [HarmonyPostfix]
        [HarmonyPatch(typeof(StartManager), "Start")]
        public static IEnumerator Start_Manager_Start_Postfix(IEnumerator __result, StartManager __instance)
        {
			if(!FastGameLoad.isAnimFast){
				__instance.startManagerAnimator.speed = float.MaxValue;
				FastGameLoad.Log.LogInfo("START ANIM IS FAST NOW");
				FastGameLoad.isAnimFast = true;
			}
			while(__result.MoveNext()){
				yield return __result.Current;
			}
        }
    }
}
