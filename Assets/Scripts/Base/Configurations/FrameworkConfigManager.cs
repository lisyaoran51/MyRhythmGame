
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Base.Configurations {
    public class FrameworkConfigManager : ConfigManager<FrameworkSetting> {
        protected override string Filename { get { return @"framework.ini"; } }

        public FrameworkConfigManager()
            : base() { }

        protected override void InitialiseDefaults() {
            Set(FrameworkSetting.ShowLogOverlay, true);

            Set(FrameworkSetting.Width, 1366);
            Set(FrameworkSetting.Height, 768);
            //Set(FrameworkSetting.ConfineMouseMode, ConfineMouseMode.Fullscreen);
            Set(FrameworkSetting.WindowedPositionX, 0.5);
            Set(FrameworkSetting.WindowedPositionY, 0.5);
            Set(FrameworkSetting.AudioDevice, string.Empty);
            Set(FrameworkSetting.VolumeUniversal, 1.0);
            Set(FrameworkSetting.VolumeMusic, 1.0);
            Set(FrameworkSetting.VolumeEffect, 1.0);
            Set(FrameworkSetting.WidthFullscreen, 9999);
            Set(FrameworkSetting.HeightFullscreen, 9999);
            Set(FrameworkSetting.Letterboxing, true);
            Set(FrameworkSetting.LetterboxPositionX, 0.0);
            Set(FrameworkSetting.LetterboxPositionY, 0.0);
            //Set(FrameworkSetting.FrameSync, FrameSync.Limit2x);
            //Set(FrameworkSetting.WindowMode, WindowMode.Windowed);
            Set(FrameworkSetting.ShowUnicode, false);
            Set(FrameworkSetting.ActiveInputHandlers, string.Empty);
            Set(FrameworkSetting.CursorSensitivity, 1.0);
            Set(FrameworkSetting.Locale, string.Empty);
        }

    }

    public enum FrameworkSetting {
        ShowLogOverlay,

        AudioDevice,
        VolumeUniversal,
        VolumeEffect,
        VolumeMusic,

        Width,
        Height,
        WindowedPositionX,
        WindowedPositionY,

        HeightFullscreen,
        WidthFullscreen,

        WindowMode,
        ConfineMouseMode,
        Letterboxing,
        LetterboxPositionX,
        LetterboxPositionY,
        FrameSync,

        ShowUnicode,
        Locale,
        ActiveInputHandlers,
        CursorSensitivity
    }
}