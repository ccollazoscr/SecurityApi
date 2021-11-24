using System.Net;

namespace Security.Common.Exception
{
    public static class FactoryErrorCode
    {
        public static ErrorCode GetErrorCode(EnumErrorCode errorCode)
        {
            ErrorCode oErrorCode = new ErrorCode { Code = errorCode };
            oErrorCode.StatusCode = HttpStatusCode.BadRequest;

            switch (errorCode)
            {
                case EnumErrorCode.InvalidToken:
                    oErrorCode.Description = "Token Invalid";
                    oErrorCode.StatusCode = HttpStatusCode.Unauthorized;
                    break;
                case EnumErrorCode.UnauthorizedUser:
                    oErrorCode.Description = "Unauthorized user";
                    oErrorCode.StatusCode = HttpStatusCode.Unauthorized;
                    break;
            }

            return oErrorCode;
        }
    }
}
