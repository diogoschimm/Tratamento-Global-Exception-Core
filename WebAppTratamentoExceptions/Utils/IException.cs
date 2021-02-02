using System.Net;

namespace WebAppTratamentoExceptions.Utils
{
    public interface IException
    {
        public HttpStatusCode GetStatusCode();
        public string GetMessageType();
    }
}
