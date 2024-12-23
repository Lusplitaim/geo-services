namespace ArcProxy.Core.Exceptions
{
    public class RestCoreException : Exception
    {
        public RestCoreException(): base()
        {
        }

        public RestCoreException(string? message, Exception? inner): base(message, inner)
        {
        }
    }
}
