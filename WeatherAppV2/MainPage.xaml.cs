using Newtonsoft.Json;
using System.Windows.Input;
using WeatherAppV2.Models;
using WeatherAppV2.Services;


namespace WeatherAppV2
{
    public partial class MainPage : ContentPage
    {
        private int _temp;
        private double _wind;
        private int _humidity;
        private int _pressure;
        private string _country;
        private int _clouds;
        private string _weatherdescription;
        private int _dateAndTime;
        private int _sunrise;
        private int _sunset;
        private long dateModified;

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

        public int DateAndTime
        {
            get { return _dateAndTime; }
            set
            {
                _dateAndTime = value;
                OnPropertyChanged();
            }
        }

        public int Sunrise
        {
            get { return _sunrise; }
            set
            {
                _sunrise = value;
                OnPropertyChanged();
            }
        }

        public int Sunset
        {
            get { return _sunset; }
            set
            {
                _sunset = value;
                OnPropertyChanged();
            }
        }

        public long DateModified
        {
            get => dateModified;
            set
            {
                if (dateModified.Equals(value)) return;
                dateModified = value;
                OnPropertyChanged();
            }
        }

        private GeoClass _geoClass;

        private string _currentWeather;

        /* public string CurrentWeather
         {
             get { return _currentWeather; }
             set
             {
                 _currentWeather = value;
                 OnPropertyChanged();
             }
         }*/



        public MainPage()
        {
            InitializeComponent();
            ShowWeatherCommand = new Command(GetLatestWeather);
            _client = new HttpClient();
            _client.DefaultRequestHeaders.Add("Accept", "application/json");
            _geoClass = new GeoClass();
            BindingContext = this;

        }

        //private void InitializeComponent()
        //{
        //    throw new NotImplementedException();
        //}

        public ICommand ShowWeatherCommand { get; set; }

       
        private HttpClient _client;

        public async void GetLatestWeather(object parameters)
        {
            DateAndTime currenttime = new DateAndTime();

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
                Sunrise = (int)currentweather.Sys.Sunrise;
                Sunset = (int)currentweather.Sys.Sunset;
                DateModified = currentweather.Dt;
                Humidity = (int)currentweather.Main.Humidity;
                Pressure = (int)currentweather.Sys.Sunrise;
                Country = currentweather.Sys.Country;
                Clouds = (int)currentweather.Clouds.All;

                if (currentweather.Weather.Count() > 0)
                {
                    WeatherDescription = currentweather.Weather[0].Description;
                }

            }

        }


        //RestService _restService;

        //public MainPage()
        //{
        //    InitializeComponent();
        //    _restService = new RestService();
        //}

        //async void OnGetWeatherButtonClicked(object sender, EventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(_cityEntry.Text))
        //    {
        //        WeatherData weatherData = await
        //            _restService.
        //            GetWeatherData(GenerateRequestURL(WeatherAPI.OpenWeatherMapEndpoint));

        //        BindingContext = weatherData;
        //    }
        //}

        //string GenerateRequestURL(string endPoint)
        //{
        //    string requestUri = endPoint;
        //    requestUri += $"?q={_cityEntry.Text}";
        //    requestUri += "&units=metric";
        //    requestUri += $"&APPID={WeatherAPI.OpenWeatherMapAPIKey}";
        //    return requestUri;
        //}
    }

}
