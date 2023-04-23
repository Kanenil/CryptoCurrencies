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

namespace CryptoCurrencies.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public IntPtr Handle { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            SourceInitialized += (s, ea) =>
            {
                try
                {
                    Handle = new WindowInteropHelper(this).EnsureHandle();
                    SetDarkMode();
                }
                catch { }
            };
        }

        private int SetDarkMode()
        {
            var darkMode = true;
            return DwmSetWindowAttribute(
                Handle,
                DwmWindomAttributeType.DWMWA_USE_IMMERSIVE_DARK_MODE,
                ref darkMode,
                Marshal.SizeOf<bool>()
            );
        }

        [DllImport("dwmapi")]
        private static extern int DwmSetWindowAttribute(
            IntPtr hwnd,
            DwmWindomAttributeType attribute,
            [In] ref bool pvAttribute,
            int cbAttribute
        );

        private enum DwmWindomAttributeType
        {
            DWMWA_USE_IMMERSIVE_DARK_MODE = 20,
        }
    }
}
