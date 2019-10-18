using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private const string CalculationApiUrl = "http://localhost:50032/api/calculation/stddev";

        static readonly Dictionary<string, List<Tuple<DateTime, float>>> weatherData;

        static WeatherController()
        {
            weatherData = JsonConvert.DeserializeObject<Dictionary<string, List<Tuple<DateTime, float>>>>(System.IO.File.ReadAllText("weather_data.json"));
        }

        [HttpGet("stddev/{city}")]
        public async Task<ActionResult> StdDevForCity(string city)
        {
            if (!weatherData.ContainsKey(city.ToUpper()))
            {
                return BadRequest($"{city} not found!");
            }
            using (var client = new HttpClient())
            {
                var response = await client.PostAsJsonAsync(CalculationApiUrl, weatherData[city.ToUpper()].Select(x => x.Item2).ToArray());
                response.EnsureSuccessStatusCode();
                return Ok(await response.Content.ReadAsStringAsync());
            }
        }
    }
}
