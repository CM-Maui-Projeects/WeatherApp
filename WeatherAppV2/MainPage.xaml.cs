using Newtonsoft.Json;
using System.Windows.Input;
using WeatherAppV2.Models;
using WeatherAppV2.Services;


namespace WeatherAppV2
{
    public partial class MainPage : ContentPage
    {
        //Variables
        private int _temp;
        private double _wind;
        private int _humidity;
        private int _pressure;
        private string _country;
        private int _clouds;
        private string _weatherdescription;
        private int _dateAndTime;
        private string _sunrise;
        private string _sunset;
        private string dateModified;
        private double _tempmin;
        private double _tempmax;
        private double _feelslike;
        private GeoClass _geoClass;

        public int Temp
        {
            get { return _temp; }
            set
            {
                _temp = value;
                OnPropertyChanged();
            }
        }

        public double Wind
        {
            get { return _wind; }
            set
            {
                _wind = value;
                OnPropertyChanged();
            }
        }

        public int Humidity
        {
            get { return _humidity; }
            set
            {
                _humidity = value;
                OnPropertyChanged();
            }
        }

        public int Pressure
        {
            get { return _pressure; }
            set
            {
                _pressure = value;
                OnPropertyChanged();
            }
        }

        public string Country
        {
            get { return _country; }
            set
            {
                _country = value;
                OnPropertyChanged();
            }
        }

        public int Clouds
        {
            get { return _clouds; }
            set
            {
                _clouds = value;
                OnPropertyChanged();
            }
        }

        public string WeatherDescription
        {
            get { return _weatherdescription; }
            set
            {
                _weatherdescription = value;
                OnPropertyChanged();
            }
        }

        public string Sunrise
        {
            get { return _sunrise; }
            set
            {
                _sunrise = value;
                OnPropertyChanged();
            }
        }

        public string Sunset
        {
            get { return _sunset; }
            set
            {
                _sunset = value;
                OnPropertyChanged();
            }
        }

        public string DateModified
        {
            get => dateModified;
            set
            {                
                dateModified = value;
                OnPropertyChanged();
            }
        }
        
        public double TempMin
        {
            get { return _tempmin; }
            set { _tempmin = value; OnPropertyChanged(); }
        }
        
        public double TempMax
        {
            get { return _tempmax; }
            set { _tempmax = value; OnPropertyChanged(); }
        }

        public double FeelsLike
        {
            get { return _feelslike; }
            set { _feelslike = value; OnPropertyChanged(); }
        }

        public MainPage()
        {
            InitializeComponent();
            ShowWeatherCommand = new Command(GetLatestWeather);
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _geoClass = new GeoClass();
            BindingContext = this;
        }
        
        public ICommand ShowWeatherCommand { get; set; }
        private HttpClient _client;

        public async void GetLatestWeather(object parameters)
        {
            await DisplayAlert("Notice", "Weather Is Updating...", "Okay");

            Location location = await _geoClass.GetCurrentLocation();
            double lat = location.Latitude;
            double lng = location.Longitude;

            string appid = WeatherAPI.OpenWeatherMapAPIKey;
            string append = WeatherAPI.OpenWeatherMapEndpoint;
            string response = await _client.GetStringAsync(new Uri($"{append}?lat={lat}&lon={lng}&appid={appid}&units=metric"));

            WeatherData currentweather = JsonConvert.DeserializeObject<WeatherData>(response);

            if (currentweather != null)
            {
                Temp = (int)Math.Round(currentweather.Main.Temperature);
                Wind = currentweather.Wind.Speed;

                DateTimeOffset dtOffset = DateTimeOffset.FromUnixTimeSeconds(currentweather.Sys.Sunrise);
                Sunrise = dtOffset.UtcDateTime.ToString();

                dtOffset = DateTimeOffset.FromUnixTimeSeconds(currentweather.Sys.Sunset);
                Sunset = dtOffset.UtcDateTime.ToString();

                dtOffset = DateTimeOffset.FromUnixTimeSeconds(currentweather.Dt);
                DateModified = dtOffset.UtcDateTime.ToString();

                TempMax = Math.Round(currentweather.Main.TempMax);
                TempMin = Math.Round(currentweather.Main.TempMin);
                FeelsLike = Math.Round(currentweather.Main.FeelsLike);
                Humidity = currentweather.Main.Humidity;
                Pressure = currentweather.Sys.Sunrise;
                Country = currentweather.Sys.Country;
                Clouds = currentweather.Clouds.All;

                if (currentweather.Weather.Count() > 0)
                {
                    WeatherDescription = currentweather.Weather[0].Description.ToUpper();
                }

            }

        }

    }

}
