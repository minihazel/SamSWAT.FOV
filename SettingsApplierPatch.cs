using SPT.Reflection.Patching;
using SPT.Reflection.Utils;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace SamSWAT.FOV
{
    public class SettingsApplierPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            Type gclass911 = PatchConstants.EftTypes.Single(x => x.GetMethod("Clone") != null && x.GetField("NotificationTransportType") != null);
            return gclass911.GetNestedTypes(PatchConstants.PrivateFlags)[0].GetMethod("method_0", PatchConstants.PrivateFlags);
        }

        [PatchPostfix]
        public static void PatchPostfix(int x, ref int __result)
        {
            __result = Mathf.Clamp(x, FovPlugin.MinFov.Value, FovPlugin.MaxFov.Value);
        }
    }
}
