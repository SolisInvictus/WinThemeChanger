namespace WinThemeChangerSvc.Types
{
    class ThemeTime
    {
        public string ThemeKey { get; }
        public string Time { get; }

        public ThemeTime(string themeKey, string time)
        {
            ThemeKey = themeKey;
            Time = time;
        }
    }
}
