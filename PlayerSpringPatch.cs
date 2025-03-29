using SPT.Reflection.Patching;
using EFT.Animations;
using System.Reflection;
using UnityEngine;
using HarmonyLib;

namespace SamSWAT.FOV
{
    public class PlayerSpringPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(PlayerSpring), nameof(PlayerSpring.Start));
        }

        [PatchPostfix]
        private static void PatchPostfix(ref Vector3 ___CameraOffset)
        {
            ___CameraOffset = new Vector3(0.04f, FovPlugin.VerticalHudFov.Value, FovPlugin.HorizontalHudFov.Value);
        }
    }
}
