using System;
using System.Collections.Generic;
using System.Text;

namespace Security.Common.Exception
{
    public enum EnumErrorCode
    {
        Generic = 0,
        UserNameMandatory = 1,
        UserNameLength = 2,
        PasswordMandatory = 3,
        PasswordLength = 4
    }
}
