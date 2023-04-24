using CryptoCurrencies.WPF.Interfaces;
using LiveChartsCore.Defaults;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CryptoCurrencies.WPF.Stores;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CryptoCurrencies.CoinGecko.Models;
using CryptoCurrencies.CoinGecko.Interfaces;
using System.Globalization;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Geo;
using LiveChartsCore.SkiaSharpView.WPF;
using System.Diagnostics;
using System.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;

namespace CryptoCurrencies.WPF.ViewModels
{
    public partial class DetailViewModel : BaseViewModel
    {
        private readonly CoinStore _coinStore;
        private readonly ICoinsService _coinsService;

        public DetailViewModel(INavigationService navigationService, CoinStore coinStore, ICoinsService coinsService) : base(navigationService)
        {
            _coinStore = coinStore;
            _coinsService = coinsService;

            LoadCoinCommand = new AsyncRelayCommand(LoadCoinAsync);
            DismissErrorCommand = new AsyncRelayCommand(LoadCoinAsync);
            LoadChartCommand = new AsyncRelayCommand(LoadChartAsync);
            DismissChartErrorCommand = new AsyncRelayCommand(LoadChartAsync);
            LoadMarketsCommand = new AsyncRelayCommand(LoadMarketsAsync);
            DismissMarketsErrorCommand = new AsyncRelayCommand(LoadMarketsAsync);
        }

        [ObservableProperty]
        private string _culture = "en-US";

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private bool _hasError;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private string _description;

        [ObservableProperty]
        private bool _isLoadingChart;

        [ObservableProperty]
        private bool _hasErrorChart;

        [ObservableProperty]
        private string _titleChart;

        [ObservableProperty]
        private string _descriptionChart;

        [ObservableProperty]
        private bool _isLoadingMarkets;

        [ObservableProperty]
        private bool _hasErrorMarkets;

        [ObservableProperty]
        private string _titleMarkets;

        [ObservableProperty]
        private string _descriptionMarkets;

        [ObservableProperty]
        private DetailCoin _coin = new();

        [ObservableProperty]
        private ObservableCollection<TickerInfo> _markets;

        public Axis[] XAxes { get; set; } =
        {
            new Axis
            {
                LabelsRotation = 90,
                TextBrush = new SolidColorPaint(SkiaSharp.SKColor.Parse("#b3b9c5")),
                Labeler = value => value > 0? new DateTime((long)value).ToString("t"):"",
                UnitWidth = TimeSpan.FromMinutes(4).Ticks,
            }
        };

        public Axis[] YAxes { get; set; } =
        {
            new Axis
            {
                TextBrush = new SolidColorPaint(SkiaSharp.SKColor.Parse("#b3b9c5")),
                Labeler = (price) =>
                {
                    int precision;
                    if (price >= 1 || price < 0) precision = 2;
                    else if (price > 0.099) precision = 4;
                    else precision = (int)Math.Log10((double)(1 / price)) * 2;
                    return string.Format(new CultureInfo("en-US"), "{0:C" + precision + "}", price);
                }
            }
        };

        [ObservableProperty]
        private ISeries[] _series = new ISeries[]
            {
                new CandlesticksSeries<FinancialPoint>
                {
                    Values = new ObservableCollection<FinancialPoint>()
                    {
                        new(new DateTime(2021, 1, 1), 523, 500, 450, 400),
                    },
                    TooltipLabelFormatter = (val) =>
                    {
                        var price = (double)val.PrimaryValue;

                        int precision;
                        if (price >= 1 || price < 0) precision = 2;
                        else if (price > 0.099) precision = 4;
                        else precision = (int)Math.Log10((double)(1 / price)) * 2;
                        return string.Format(new CultureInfo("en-US"), val.Model.Date.ToString("dd/MM/yyyy H:mm:ss")+" {0:C" + precision + "}", price);
                    }
                }
            };

        public IAsyncRelayCommand LoadCoinCommand { get; }
        public IAsyncRelayCommand DismissErrorCommand { get; }
        private async Task LoadCoinAsync()
        {
            IsLoading = true;
            HasError = false;

            try
            {
                Title = "Loading Data";
                Description = "Please wait, we are loading coin data";

                Culture = _coinStore.Culture;

                var resp = await _coinsService.GetCoinMarkets("usd", 1, new string[] { _coinStore.SelectedCoin });

                Coin = resp.FirstOrDefault();

                IsLoading = false;
            }
            catch (Exception)
            {
                Title = "Too many requests";
                Description = "Please wait for the API to be available again";

                HasError = true;
            }
        }
        public IAsyncRelayCommand LoadChartCommand { get; }
        public IAsyncRelayCommand DismissChartErrorCommand { get; }
        private async Task LoadChartAsync()
        {
            IsLoadingChart = true;
            HasErrorChart = false;

            try
            {
                TitleChart = "Loading Chart";
                DescriptionChart = "Please wait, we are loading information";

                var resp = await _coinsService.GetMarketChartsByCoinId(_coinStore.SelectedCoin, "usd", "1");

                var values = new ObservableCollection<FinancialPoint>();

                for (var i = 0; i < resp.Prices.Length; i++)
                {
                    var timestamp = DateTimeOffset.FromUnixTimeMilliseconds((long)resp.Prices[i][0]);

                    FinancialPoint candleStickPoint;

                    if (i + 1 < resp.Prices.Length)
                    {
                        candleStickPoint = new FinancialPoint()
                        {
                            Date = timestamp.DateTime,
                            Open = (double)resp.Prices[i][1],
                            Close = (double)resp.Prices[i + 1][1],
                            High = resp.Prices[i + 1][1] > resp.Prices[i][1] ? (double)resp.Prices[i + 1][1] : (double)resp.Prices[i][1],
                            Low = resp.Prices[i + 1][1] < resp.Prices[i][1] ? (double)resp.Prices[i + 1][1] : (double)resp.Prices[i][1]
                        };
                    }
                    else
                    {
                        candleStickPoint = new()
                        {
                            Date = timestamp.DateTime,
                            Open = (double)resp.Prices[i][1],
                            Close = (double)resp.Prices[i][1],
                            High = (double)resp.Prices[i][1],
                            Low = (double)resp.Prices[i][1]
                        };

                    }

                    values.Add(candleStickPoint);
                }

                Series[0].Values = values;
                Series[0].RestartAnimations();

                await Task.Delay(500);

                IsLoadingChart = false;
            }
            catch (Exception)
            {
                TitleChart = "Too many requests";
                DescriptionChart = "Please wait for the API to be available again";

                HasErrorChart = true;
            }
        }
        public IAsyncRelayCommand LoadMarketsCommand { get; }
        public IAsyncRelayCommand DismissMarketsErrorCommand { get; }
        private async Task LoadMarketsAsync()
        {
            IsLoadingMarkets = true;
            HasErrorMarkets = false;

            try
            {
                TitleMarkets = "Loading Markets";
                DescriptionMarkets = "Please wait, we are loading information";

                var resp = await _coinsService.GetTickerByCoinId(_coinStore.SelectedCoin, 1);


                Markets = new(resp.Tickers.Take(5));

                IsLoadingMarkets = false;
            }
            catch (Exception)
            {
                TitleMarkets = "Too many requests";
                DescriptionMarkets = "Please wait for the API to be available again";

                HasErrorMarkets = true;
            }
        }

        [RelayCommand]
        private void MoveToHome()
        {
            Navigation.NavigateTo<HomeViewModel>();
        }
        [RelayCommand]
        private void OpenUrl(string? url)
        {
            Process.Start(
                new ProcessStartInfo(
                    "cmd", $"/c start {url}"
                )
                { CreateNoWindow = true }
            );
        }
    }
}
