using Microsoft.Win32;
using System;

namespace WinThemeChangerLib
{
    public class Settings
    {
        private static Settings instance;

        public const string EVENT_LOG_SOURCE_NAME   = "Windows 10 Theme Changer Service";
        public const string EVENT_LOG_LOG_NAME      = "Windows 10 Theme Changer";

        private const string WTC_REG_KEY_NAME                           = @"Software\WinThemeChangerSvc";
        private const string WTC_REG_LIGHT_SCHEDULED_TIME               = "LightScheduledTime";
        private const string WTC_REG_LIGHT_CHANGE_WINDOW_MODE           = "LightChangeWindowMode";
        private const string WTC_REG_LIGHT_CHANGE_APPLICATION_MODE      = "LightChangeApplicationMode";
        private const string WTC_REG_DARK_SCHEDULED_TIME                = "DarkScheduledTime";
        private const string WTC_REG_DARK_CHANGE_WINDOW_MODE            = "DarkChangeWindowMode";
        private const string WTC_REG_DARK_CHANGE_APPLICATION_MODE       = "DarkChangeApplicationMode";

        public string LightScheduledTime { get; set; }

        public bool LightChangeWindowMode { get; set; }

        public bool LightChangeApplicationMode { get; set; }

        public string DarkScheduledTime { get; set; }

        public bool DarkChangeWindowMode { get; set; }

        public bool DarkChangeApplicationMode { get; set; }

        private Settings()
        {
        }

        public static Settings GetInstance()
        {
            if (instance == null)
                instance = new Settings();
            return instance;
        }

        public bool LoadSettings()
        {
            try
            {
                RegistryKey rgkAppKey = Registry.CurrentUser.OpenSubKey(WTC_REG_KEY_NAME);
                if (rgkAppKey == null)
                    return CreateDefaultSettings();

                LightScheduledTime = (string) rgkAppKey.GetValue(WTC_REG_LIGHT_SCHEDULED_TIME);
                LightChangeWindowMode = Convert.ToBoolean(rgkAppKey.GetValue(WTC_REG_LIGHT_CHANGE_WINDOW_MODE));
                LightChangeApplicationMode = Convert.ToBoolean(rgkAppKey.GetValue(WTC_REG_LIGHT_CHANGE_APPLICATION_MODE));
                DarkScheduledTime = (string) rgkAppKey.GetValue(WTC_REG_DARK_SCHEDULED_TIME);
                DarkChangeWindowMode = Convert.ToBoolean(rgkAppKey.GetValue(WTC_REG_DARK_CHANGE_WINDOW_MODE));
                DarkChangeApplicationMode = Convert.ToBoolean(rgkAppKey.GetValue(WTC_REG_DARK_CHANGE_APPLICATION_MODE));
            }
            catch (Exception e)
            {
#if (DEBUG)
                Console.WriteLine(e.StackTrace);
#endif
                return false;
            }

            return true;
        }

        public bool CreateDefaultSettings()
        {
            try
            {
                LightScheduledTime = "06:00";
                LightChangeWindowMode = true;
                LightChangeApplicationMode = true;
                DarkScheduledTime = "17:00";
                DarkChangeWindowMode = true;
                DarkChangeApplicationMode = true;

                RegistryKey rgkAppKey = Registry.CurrentUser.CreateSubKey(WTC_REG_KEY_NAME);
                rgkAppKey.SetValue(WTC_REG_LIGHT_SCHEDULED_TIME, LightScheduledTime);
                rgkAppKey.SetValue(WTC_REG_LIGHT_CHANGE_WINDOW_MODE, LightChangeWindowMode);
                rgkAppKey.SetValue(WTC_REG_LIGHT_CHANGE_APPLICATION_MODE, LightChangeApplicationMode);
                rgkAppKey.SetValue(WTC_REG_DARK_SCHEDULED_TIME, DarkScheduledTime);
                rgkAppKey.SetValue(WTC_REG_DARK_CHANGE_WINDOW_MODE, DarkChangeWindowMode);
                rgkAppKey.SetValue(WTC_REG_DARK_CHANGE_APPLICATION_MODE, DarkChangeApplicationMode);
            }
            catch (Exception e)
            {
#if (DEBUG)
                Console.WriteLine(e.StackTrace);
#endif
                return false;
            }

            return true;
        }

        public bool SaveSettings()
        {
            try
            {
                RegistryKey rgkAppKey = Registry.CurrentUser.CreateSubKey(WTC_REG_KEY_NAME);
                rgkAppKey.SetValue(WTC_REG_LIGHT_SCHEDULED_TIME, LightScheduledTime);
                rgkAppKey.SetValue(WTC_REG_LIGHT_CHANGE_WINDOW_MODE, LightChangeWindowMode);
                rgkAppKey.SetValue(WTC_REG_LIGHT_CHANGE_APPLICATION_MODE, LightChangeApplicationMode);
                rgkAppKey.SetValue(WTC_REG_DARK_SCHEDULED_TIME, DarkScheduledTime);
                rgkAppKey.SetValue(WTC_REG_DARK_CHANGE_WINDOW_MODE, DarkChangeWindowMode);
                rgkAppKey.SetValue(WTC_REG_DARK_CHANGE_APPLICATION_MODE, DarkChangeApplicationMode);

                return true;
            }
            catch (Exception e)
            {
#if (DEBUG)
                Console.WriteLine(e.StackTrace);
#endif
                return false;
            }
        }
    }
}
