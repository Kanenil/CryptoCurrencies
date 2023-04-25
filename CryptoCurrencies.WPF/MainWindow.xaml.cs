using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.Windows.Interop;
using CryptoCurrencies.WPF.Core;
using FontAwesome.WPF;

namespace CryptoCurrencies.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetupWindow.SetupSettings(this);
            Icon.Icon = Settings.Default.DarkTheme ? FontAwesomeIcon.SunOutline : FontAwesomeIcon.MoonOutline;
        }

        private void SwitchTheme(object sender, RoutedEventArgs e) 
        {
            Icon.Icon = Settings.Default.DarkTheme ? FontAwesomeIcon.MoonOutline : FontAwesomeIcon.SunOutline;
            ThemeSwitcher.Switch(this);
        }
    }
}
