namespace WebApiTest.Exceptions
{
    public class ExternalLoginProviderException(string provider, string message) : Exception("Exeternal login provider: " + provider + " error ocurred: " + message)
    {

    }
}
