namespace Security.Common.Exception
{
    public enum EnumErrorCode
    {
        Generic = 0,
        UserNameMandatory = 1,
        UserNameLength = 2,
        PasswordMandatory = 3,
        PasswordLength = 4,
        InvalidToken = 5,
        TokenMandatory = 6,
        UnauthorizedUser = 7
    }
}
