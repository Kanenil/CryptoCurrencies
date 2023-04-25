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
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using CryptoCurrencies.WPF.Core;

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
            LoadChartCommand = new AsyncRelayCommand(LoadChartAsync);
            LoadMarketsCommand = new AsyncRelayCommand(LoadMarketsAsync);
        }

        private Dictionary<string, int> _rangesDictionary = new Dictionary<string, int>()
        {
            { "1d", 1 },
            { "7d", 7 },
            { "1m", 30 },
            { "3m", 90 },
            { "1y", 365 }
        };

        private ObservableCollection<FinancialPoint> _financialPoints = new();

        [ObservableProperty]
        private Loader _chartLoader = new();

        [ObservableProperty]
        private Loader _coinLoader = new();

        [ObservableProperty]
        private Loader _marketsLoader = new();

        [ObservableProperty]
        private string _days = "1d";

        [ObservableProperty]
        private DetailCoin _coin = new();

        [ObservableProperty]
        private ObservableCollection<TickerInfo> _markets;

        [ObservableProperty]
        public Axis[] _xAxes;

        [ObservableProperty]
        private ISeries[] _series;

        public IAsyncRelayCommand LoadCoinCommand { get; }
        private async Task LoadCoinAsync()
        {
            CoinLoader.IsLoading = true;
            CoinLoader.HasError = false;

            try
            {
                CoinLoader.Title = "Loading Data";
                CoinLoader.Description = "Please wait, we are loading coin data";

                var resp = await _coinsService.GetCoinMarkets("usd", 1, new string[] { _coinStore.SelectedCoin });

                Coin = resp.FirstOrDefault();

                CoinLoader.IsLoading = false;
            }
            catch (Exception)
            {
                CoinLoader.Title = "Too many requests";
                CoinLoader.Description = "Please wait for the API to be available again";

                CoinLoader.HasError = true;
            }
        }
        public IAsyncRelayCommand LoadChartCommand { get; }
        private async Task LoadChartAsync()
        {
            ChartLoader.IsLoading = true;
            ChartLoader.HasError = false;

            int daysRange = _rangesDictionary[Days];

            try
            {
                ChartLoader.Title = "Loading Chart";
                ChartLoader.Description = "Please wait, we are loading information";

                var resp = await _coinsService.GetOhlcByCoinId(_coinStore.SelectedCoin, "usd", daysRange.ToString());

                double[][] ohlcArray = JsonSerializer.Deserialize<double[][]>(resp.ToString());

                if (ohlcArray != null)
                {
                    _financialPoints.Clear();
                    ConvertToFinancialPoint(ohlcArray).ForEach(x=>_financialPoints.Add(x));
                }

                ChartLoader.IsLoading = false;
            }
            catch (Exception)
            {
                ChartLoader.Title = "Too many requests";
                ChartLoader.Description = "Please wait for the API to be available again";

                ChartLoader.HasError = true;
            }
            finally
            {
                if (!ChartLoader.HasError)
                {
                    long unitWidth = 0;
                    Func<double, string> labeler = new Func<double, string>(value => string.Empty);

                    if (daysRange <= 2)
                    {
                        unitWidth = TimeSpan.FromMinutes(30).Ticks;
                        labeler = value => new DateTime((long)value).ToString("HH:mm");
                    }
                    else if (daysRange > 2 && daysRange <= 30)
                    {
                        unitWidth = TimeSpan.FromHours(4).Ticks;
                        labeler = value => new DateTime((long)value).ToString($"HH:mm{Environment.NewLine}dd/MM");
                    }
                    else if (daysRange > 30)
                    {
                        unitWidth = TimeSpan.FromDays(4).Ticks;
                        labeler = value => new DateTime((long)value).ToString($"dd/MM{Environment.NewLine}yyyy");
                    }

                    XAxes = new Axis[1]
                    {
                        new Axis
                        {
                            LabelsRotation = 0,
                            UnitWidth = unitWidth,
                            Labeler = labeler,
                            MinLimit = null,
                            MaxLimit = null
                        }
                    };

                    Series = new ISeries[1]
                    {
                        new CandlesticksSeries<FinancialPoint>
                        {
                            MaxBarWidth = SystemParameters.WorkArea.Width / 15,
                            Values = _financialPoints,
                            TooltipLabelFormatter = (ChartPoint) =>
                                                    $"Date: {ChartPoint.Model.Date.ToString("dd/MM/yyyy  HH:mm")}{Environment.NewLine}" +
                                                    $"High: {ChartPoint.Model.High}${Environment.NewLine}" +
                                                    $"Open: {ChartPoint.Model.Open}${Environment.NewLine}" +
                                                    $"Close: {ChartPoint.Model.Close}${Environment.NewLine}" +
                                                    $"Low: {ChartPoint.Model.Low}$"
                        }
                    };

                }

            }
        }

        private List<FinancialPoint> ConvertToFinancialPoint(double[][] marketChartById)
        {
            List<FinancialPoint> financialPoints = new List<FinancialPoint>();
            foreach (var c in marketChartById)
            {
                if (c != null)
                {
                    var point = new FinancialPoint();

                    point.Date = DateTimeOffset.FromUnixTimeMilliseconds((long)c[0]).UtcDateTime;
                    point.Open = c[1];
                    point.High = c[2];
                    point.Low = c[3];
                    point.Close = c[4];

                    financialPoints.Add(point);
                }
            }
            return financialPoints;
        }
        public IAsyncRelayCommand LoadMarketsCommand { get; }
        private async Task LoadMarketsAsync()
        {
            MarketsLoader.IsLoading = true;
            MarketsLoader.HasError = false;

            try
            {
                MarketsLoader.Title = "Loading Markets";
                MarketsLoader.Description = "Please wait, we are loading information";

                var resp = await _coinsService.GetTickerByCoinId(_coinStore.SelectedCoin, 1);


                Markets = new(resp.Tickers.Take(5));

                MarketsLoader.IsLoading = false;
            }
            catch (Exception)
            {
                MarketsLoader.Title = "Too many requests";
                MarketsLoader.Description = "Please wait for the API to be available again";

                MarketsLoader.HasError = true;
            }
        }

        [RelayCommand]
        private void MoveToHome()
        {
            Navigation.NavigateTo<HomeViewModel>();
        }

        [RelayCommand]
        private void ChangeChartRange(string? day)
        {
            if (Days == day)
                return;

            Days = day ?? "1d";
            Task.Run(LoadChartAsync);
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
