using CryptoCurrencies.CoinGecko.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

namespace CryptoCurrencies.WPF.Components
{
    /// <summary>
    /// Interaction logic for Loader.xaml
    /// </summary>
    public partial class Loader : UserControl
    {
        public Loader()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty IsLoadingProperty =
                   DependencyProperty.Register(
                         "IsLoading",
                          typeof(bool),
                          typeof(Loader));

        public bool IsLoading
        {
            get
            {
                return (bool)GetValue(IsLoadingProperty);
            }
            set
            {
                SetValue(IsLoadingProperty, value);
            }
        }

        public static readonly DependencyProperty HasErrorProperty =
                   DependencyProperty.Register(
                         "HasError",
                          typeof(bool),
                          typeof(Loader));

        public bool HasError
        {
            get
            {
                return (bool)GetValue(HasErrorProperty);
            }
            set
            {
                SetValue(HasErrorProperty, value);
            }
        }

        public static readonly DependencyProperty TitleProperty =
                   DependencyProperty.Register(
                         "Title",
                          typeof(string),
                          typeof(Loader));

        public string Title
        {
            get
            {
                return (string)GetValue(TitleProperty);
            }
            set
            {
                SetValue(TitleProperty, value);
            }
        }

        public static readonly DependencyProperty DescriptionProperty =
                   DependencyProperty.Register(
                         "Description",
                          typeof(string),
                          typeof(Loader));

        public string Description
        {
            get
            {
                return (string)GetValue(DescriptionProperty);
            }
            set
            {
                SetValue(DescriptionProperty, value);
            }
        }

        public static readonly DependencyProperty DismissErrorProperty =
                   DependencyProperty.Register(
                         "DismissError",
                          typeof(ICommand),
                          typeof(Loader));

        public ICommand DismissError
        {
            get
            {
                return (ICommand)GetValue(DismissErrorProperty);
            }
            set
            {
                SetValue(DismissErrorProperty, value);
            }
        }
    }
}
