using BepInEx;
using BepInEx.Configuration;
using Comfort.Common;
using EFT;
using System;
using UnityEngine;

namespace SamSWAT.FOV
{
    [BepInPlugin("com.samswat.fov", "SamSWAT.FOV", "1.0.5")]
    public class FovPlugin : BaseUnityPlugin
    {
        internal static ConfigEntry<int> MinFov;
        internal static ConfigEntry<int> MaxFov;
        internal static ConfigEntry<float> HudFov;
        internal static ConfigEntry<float> HudVerticalFov;

        private void Awake()
        {
            new FovPatch().Enable();
            new PlayerSpringPatch().Enable();
            new SettingsApplierPatch().Enable();

            MinFov = Config.Bind(
                "Main Section",
                "Min FOV Value",
                20,
                new ConfigDescription("Your desired minimum FOV value. Default is 50",
                new AcceptableValueRange<int>(1, 149)));

            MaxFov = Config.Bind(
                "Main Section",
                "Max FOV Value",
                150,
                new ConfigDescription("Your desired maximum FOV value. Default is 75",
                new AcceptableValueRange<int>(1, 150)));

            HudFov = Config.Bind(
                "Main Section",
                "HUD FOV `Value`",
                0.05f,
                new ConfigDescription("Pseudo-value for HUD FOV, it will actually change your camera position HORIZONTALLY relative to your body. " +
                "The lower the value, the further backwards the camera is and will result in more hands and weapon being in view. Default is 0.05",
                new AcceptableValueRange<float>(-0.1f, 0.1f)));

            HudVerticalFov = Config.Bind(
                "Main Section",
                "HUD Vertical FOV `Value`",
                0.05f,
                new ConfigDescription("Pseudo-value for HUD FOV, it will actually change your camera position VERTICALLY relative to your body. " +
                "The lower the value, the further down the camera is and will result in arms and weapon moving upwards. Default is 0.05",
                new AcceptableValueRange<float>(-0.1f, 0.1f)));

            HudFov.SettingChanged += HudFov_SettingChanged;
            HudVerticalFov.SettingChanged -= HudVerticalFov_SettingChanged;
        }

        private void HudFov_SettingChanged(object sender, EventArgs e)
        {
            var gameWorld = Singleton<GameWorld>.Instance;

            if (gameWorld == null || gameWorld.RegisteredPlayers == null)
            {
                return;
            }

            gameWorld.MainPlayer.ProceduralWeaponAnimation.HandsContainer.CameraOffset = new Vector3(0.04f, HudVerticalFov.Value, HudFov.Value);
        }

        private void HudVerticalFov_SettingChanged(object sender, EventArgs e)
        {
            var gameWorld = Singleton<GameWorld>.Instance;

            if (gameWorld == null || gameWorld.RegisteredPlayers == null)
            {
                return;
            }

            gameWorld.MainPlayer.ProceduralWeaponAnimation.HandsContainer.CameraOffset = new Vector3(0.04f, HudVerticalFov.Value, HudFov.Value);
        }
    }
}
