using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace SwaggerTestV3
{
    public class WeatherForecast
    {
        [Required]
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }
       
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        [NotNull]
        public string Summary { get; set; }
    }
}
