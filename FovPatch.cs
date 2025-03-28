using SPT.Reflection.Patching;
using EFT.UI;
using EFT.UI.Settings;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using HarmonyLib;

namespace SamSWAT.FOV
{
    public class FovPatch : ModulePatch
    {
        protected override MethodBase GetTargetMethod()
        {
            return AccessTools.Method(typeof(GameSettingsTab), nameof(GameSettingsTab.Show));
        }

        [PatchPostfix]
        private static void PatchPostfix(ref NumberSlider ____fov, GClass1053 ___gclass1053_0)
        {
            if (FovPlugin.MaxFov.Value < FovPlugin.MinFov.Value)
            {
                FovPlugin.MinFov.Value = 50;
                FovPlugin.MaxFov.Value = 75;
            }

            int rangeCount = FovPlugin.MaxFov.Value - FovPlugin.MinFov.Value + 1;
            SettingsTab.BindNumberSliderToSetting(____fov, ___gclass1053_0.FieldOfView, FovPlugin.MinFov.Value, FovPlugin.MaxFov.Value);
        }
    }
}
