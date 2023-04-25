using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CryptoCurrencies.WPF.Core
{
    public class ThemeSwitcher
    {
        private static readonly ResourceDictionary _lightTheme = new ResourceDictionary() { Source = new Uri("Themes/LightTheme.xaml", UriKind.Relative) };
        private static readonly ResourceDictionary _darkTheme = new ResourceDictionary() { Source = new Uri("Themes/DarkTheme.xaml", UriKind.Relative) };
        public static ResourceDictionary LightTheme { get { return _lightTheme; } }
        public static ResourceDictionary DarkTheme { get { return _darkTheme; } }

        public static void SetDefault(Window window) => window.Resources.MergedDictionaries.Add(Settings.Default.DarkTheme ? _darkTheme : _lightTheme);
        public static void Switch(Window window)
        {
            if (Settings.Default.DarkTheme == false)
            {
                Settings.Default.DarkTheme = true;
                window.Resources.MergedDictionaries[0] = _darkTheme;
            }
            else
            {
                Settings.Default.DarkTheme = false;
                window.Resources.MergedDictionaries[0] = _lightTheme;
            }
            Settings.Default.Save();
        }
    }
}
