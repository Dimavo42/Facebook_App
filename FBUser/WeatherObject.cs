
namespace MyUser
{
    public class WeatherMainObject
    {
        public double temp { get; set; }
        public int pressure { get; set; }
        public int humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_max { get; set; }

    }
    public class WeatherObject
    {
        public WeatherMainObject main { get; set; }
    }
}
