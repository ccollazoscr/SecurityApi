using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Security.Common.Configuration
{
    public interface IGeneralSettings
    {
        public string GetKeyToken();
        public int GetTimeToken();
        public string GetKeyPassword();
        
    }
}
