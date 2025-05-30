using Microsoft.AspNetCore.Mvc;


namespace E_Commerce.Web
{

    [Route("api/[controller]")]
    [ApiController]
    public class WeatherForecast
    {
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
