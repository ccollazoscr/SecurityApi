using System.Net;

namespace Security.Common.Exception
{
    public class ErrorCode
    {
        public HttpStatusCode StatusCode { get; set; }
        public EnumErrorCode Code { get; set; }
        public string Description { get; set; }
    }
}
