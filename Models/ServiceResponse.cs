namespace WeatherAPI.Models
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public T? Data { get; set; }
    }
}
