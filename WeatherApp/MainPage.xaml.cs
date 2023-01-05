using Java.Sql;

namespace WeatherApp;

public partial class MainPage : ContentPage
{
	Service WeatherService;
	public MainPage()
	{
		InitializeComponent();
		WeatherService=new Service();
	}
	async void GetWeather(object sender,EventArgs e)
	{
        try
        {
            _isCheckingLocation = true;

            GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));

            _cancelTokenSource = new CancellationTokenSource();

            Location location = await Geolocation.Default.GetLocationAsync(request, _cancelTokenSource.Token);

            if (location != null)
            {
                    
                    WeatherData weatherData = await WeatherService.GetWeatherData(GenerateURL(Constants.WeatherAPI,location));
                    BindingContext = weatherData;
            }

        }
        catch (Exception ex)
        {
        }
        finally
        {
            _isCheckingLocation = false;
        }
	}
	string GenerateURL(string endPoint,Location location)
	{
		string requestUri = endPoint;
		requestUri += $"?lat={location.Latitude}";
        requestUri += $"&lon={location.Longitude}";
        requestUri += "&units=imperial";
		requestUri += $"&appid={Constants.WeatherAPIKEY}";
		return requestUri;
	}
    private CancellationTokenSource _cancelTokenSource;
    private bool _isCheckingLocation;
    public void CancelRequest()
    {
        if (_isCheckingLocation && _cancelTokenSource != null && _cancelTokenSource.IsCancellationRequested == false)
            _cancelTokenSource.Cancel();
    }
}

