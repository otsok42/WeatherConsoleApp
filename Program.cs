using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq; 

class Program
{
    static async Task Main(string[] args)
    {
        Console.Write("Enter a city name: ");
        string cityName = Console.ReadLine();

        string apiKey = "82ce28887fec436959e557e03a886161";
        string apiUrl = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid={apiKey}&units=metric";

        try
        {
            using (HttpClient client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    var weatherData = JObject.Parse(jsonResponse);

                    string weatherDescription = weatherData["weather"][0]["description"].ToString();
                    string temperature = weatherData["main"]["temp"].ToString();

                    Console.WriteLine($"Weather in {cityName}: {weatherDescription}");
                    Console.WriteLine($"Temperature: {temperature}°C");
                }
                else
                {
                    Console.WriteLine("Error: Could not retrieve weather data. Check the city name or API key.");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }
}
