using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Common.Exception
{
    public sealed class CustomErrorException : SystemException
    {
        private List<ErrorCode> lstErrorCode = new List<ErrorCode>();

        public CustomErrorException(EnumErrorCode errorCode)
        {
            lstErrorCode.Add(new ErrorCode { Code = errorCode });
        }

        public CustomErrorException(List<ErrorCode> lstErrorCode)
        {
            this.lstErrorCode = lstErrorCode;
        }

        public List<ErrorCode> GetListError()
        {
            return lstErrorCode;
        }
    }
}
