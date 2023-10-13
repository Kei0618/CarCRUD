namespace Car.Models
{
    public class ResponseData<T>
    {
        public string Response { get; set; } = "";

        public T? Result { get; set; } 
    }
}
