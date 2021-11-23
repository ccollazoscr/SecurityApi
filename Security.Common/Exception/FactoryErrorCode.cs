using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Security.Common.Exception
{
    public static class FactoryErrorCode
    {
        public static ErrorCode GetErrorCode(EnumErrorCode errorCode)
        {
            ErrorCode oErrorCode = new ErrorCode { Code = errorCode };
            oErrorCode.StatusCode = HttpStatusCode.BadRequest;
            return oErrorCode;
        }
    }
}
