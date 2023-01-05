using Newtonsoft.Json;

namespace WeatherApp
{
    public class Service
    {
        HttpClient client;
        public Service() 
        {
            client = new HttpClient();
        }
        public async Task<WeatherData> GetWeatherData (string tmp)
        {
            WeatherData data = null;
            try
            {
                var resposne = await client.GetAsync (tmp);
                if (resposne.IsSuccessStatusCode)
                {
                    var content = await resposne.Content.ReadAsStringAsync ();
                    data=JsonConvert.DeserializeObject<WeatherData>(content);
                }
            }
            catch(Exception ex) 
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            return data;
        }
    }
}
