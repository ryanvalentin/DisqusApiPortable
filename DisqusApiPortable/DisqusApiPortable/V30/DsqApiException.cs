using System;

namespace Disqus.Api.V30
{
    public class DsqApiException : Exception
    {
        public DsqApiException(string message, int? code, FaultType fault) 
            : base(message)
        {
            this.Code = code;
            this.Fault = fault;
        }

        public int? Code { get; set; }

        public FaultType Fault { get; set; } 
    }

    public enum FaultType
    {
        Undetermined,
        Disqus,
        ClientRequest,
        ClientNetworkConnection,
        InsufficientAccess,
        Timeout
    }
}
