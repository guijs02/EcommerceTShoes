namespace EcommerceCartAPI.Domain.Exception
{
    public class ProductNotFoundInCartException : System.Exception
    {
        public ProductNotFoundInCartException(string message = NotFoundErrorMessages.ProdutoNull) : base(message) { }

    }
}
