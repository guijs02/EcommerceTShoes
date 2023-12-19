namespace EcommerceWeb.Services.Handle
{
    public class ApiException : Exception
    {
        public ApiException(string? message) : base(message)
        {
        }
    }
}
