using System;
using System.Collections;
using WinThemeChangerSvc.Types;

namespace WinThemeChangerSvc.Comparers
{
    class ThemeTimeComparer : IComparer
    {
        public int Compare(object x, object y)
        {
            return String.Compare(((ThemeTime)x).Time, ((ThemeTime)y).Time);
        }
    }
}
