using System;
using System.Net;

namespace WebAppTratamentoExceptions.Utils
{
    public class ExceptionWarning : Exception, IException
    {
        public ExceptionWarning(string message) : base(message) { }

        public HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
        public string GetMessageType() => "warning";
    }
}
