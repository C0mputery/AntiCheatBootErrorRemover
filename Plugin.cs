using System;
using System.Reflection;
using BepInEx;
using Epic.OnlineServices.AntiCheatClient;
using HarmonyLib;
using Landfall;

namespace AntiCheatBootErrorRemover
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class Plugin : BaseUnityPlugin
    {
        private void Awake()
        {
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
            Harmony harmony = new Harmony(PluginInfo.PLUGIN_GUID);
            harmony.PatchAll(typeof(SetACInterface_Patch));
            Logger.LogInfo($"SetACInterface Patched, Anti-Cheat Boot Error Will No Longer Appear");
        }
    }


    public class SetACInterface_Patch
    {
        [HarmonyPatch(typeof(Easy_AC_Client), "SetACInterface")]
        [HarmonyPrefix]
        public static bool Prefix(AntiCheatClientInterface ACInterface)
        {
            Traverse.Create(Easy_AC_Client._instance).Field("m_Interface").SetValue(ACInterface);
            return false;
        }
    }
}
