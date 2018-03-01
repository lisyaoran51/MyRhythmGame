using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Base.Configurations;


namespace Base.Rulesets.Straight.Configurations {
    public class StraightConfigManager : ConfigManager<StraightSetting> {

        public StraightConfigManager() : base() {
        }

        protected override void InitialiseDefaults() {
            // UI/selection defaults
            Set(StraightSetting.Ruleset, 0);
            //Set(StraightSetting.BeatmapDetailTab, BeatmapDetailTab.Details);

            Set(StraightSetting.DisplayStarsMinimum, 0.0);
            Set(StraightSetting.DisplayStarsMaximum, 10.0);

            //Set(StraightSetting.SelectionRandomType, SelectionRandomType.RandomPermutation);

            //Set(StraightSetting.ChatDisplayHeight, ChatOverlay.DEFAULT_HEIGHT, 0.2, 1);

            // Online settings
            Set(StraightSetting.Username, string.Empty);
            Set(StraightSetting.Token, string.Empty);

            /*
            Set(StraightSetting.SavePassword, false).ValueChanged += val => {
                if (val) Set(OsuSetting.SaveUsername, true);
            };

            Set(StraightSetting.SaveUsername, true).ValueChanged += val => {
                if (!val) Set(OsuSetting.SavePassword, false);
            };
            */

            // Audio
            Set(StraightSetting.MenuVoice, true);
            Set(StraightSetting.MenuMusic, true);

            Set(StraightSetting.AudioOffset, 0);

            // Input
            Set(StraightSetting.MenuCursorSize, 1.0);
            Set(StraightSetting.GameplayCursorSize, 1.0);
            Set(StraightSetting.AutoCursorSize, false);

            Set(StraightSetting.MouseDisableButtons, false);
            Set(StraightSetting.MouseDisableWheel, false);

            // Graphics
            Set(StraightSetting.ShowFpsDisplay, false);

            Set(StraightSetting.ShowStoryboard, true);
            Set(StraightSetting.CursorRotation, true);

            Set(StraightSetting.MenuParallax, true);

            Set(StraightSetting.SnakingInSliders, true);
            Set(StraightSetting.SnakingOutSliders, true);

            // Gameplay
            Set(StraightSetting.DimLevel, 0.3);

            Set(StraightSetting.ShowInterface, true);
            Set(StraightSetting.KeyOverlay, false);

            Set(StraightSetting.FloatingComments, false);
            Set(StraightSetting.PlaybackSpeed, 1.0);

            // Update
            //Set(StraightSetting.ReleaseStream, ReleaseStream.Lazer);

            Set(StraightSetting.Version, string.Empty);


        }

    }

    public enum StraightSetting {
        Ruleset,
        Token,
        MenuCursorSize,
        GameplayCursorSize,
        AutoCursorSize,
        DimLevel,
        ShowStoryboard,
        KeyOverlay,
        FloatingComments,
        PlaybackSpeed,
        ShowInterface,
        MouseDisableButtons,
        MouseDisableWheel,
        AudioOffset,
        MenuMusic,
        MenuVoice,
        CursorRotation,
        MenuParallax,
        BeatmapDetailTab,
        Username,
        ReleaseStream,
        SavePassword,
        SaveUsername,
        DisplayStarsMinimum,
        DisplayStarsMaximum,
        SelectionRandomType,
        SnakingInSliders,
        SnakingOutSliders,
        ShowFpsDisplay,
        ChatDisplayHeight,
        Version,

        StartPitch,
        availableColumns,
        TargetLineHeight,
        WhiteKeyLength,
        WhiteKeyTarget,
        BlackKeyLength,
        BlackKeyTarget,
        PixelToMillimeter
    }
}