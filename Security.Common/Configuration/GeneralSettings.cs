using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Common.Configuration
{
    public class GeneralSettings : IGeneralSettings
    {

        private string KeyPassword;
        private string KeyToken;
        private int TimeToken;
        public GeneralSettings SetKeyPassword(string KeyPassword)
        {
            this.KeyPassword = KeyPassword;
            return this;
        }
        public GeneralSettings SetKeyToken(string KeyToken)
        {
            this.KeyToken = KeyToken;
            return this;
        }
        public GeneralSettings SetTimeToken(int TimeToken)
        {
            this.TimeToken = TimeToken;
            return this;
        }
        public string GetKeyPassword()
        {
            return KeyPassword;
        }

        public string GetKeyToken()
        {
            return KeyToken;
        }
        public int GetTimeToken()
        {
            return TimeToken;
        }
    }
}
