using System;
using System.Net;

namespace WebAppTratamentoExceptions.Utils
{
    public class ExceptionNotFound : Exception, IException
    {
        public ExceptionNotFound(string message) : base(message) { }

        public HttpStatusCode GetStatusCode() => HttpStatusCode.NotFound;
        public string GetMessageType() => "warning";
    }
}
