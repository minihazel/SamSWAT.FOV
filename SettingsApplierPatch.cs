using SPT.Reflection.Patching;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace SamSWAT.FOV
{
    public class SettingsApplierPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GClass1053.Class1718), nameof(GClass1053.Class1718.method_0));
        }

        [PatchPostfix]
        public static void PatchPostfix(int x, ref int __result)
        {
            __result = Mathf.Clamp(x, FovPlugin.MinFov.Value, FovPlugin.MaxFov.Value);
        }
    }
}
