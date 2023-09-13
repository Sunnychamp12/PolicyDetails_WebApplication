using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PolicyDetails_WebApplication.Enums
{
    public class Enums
    {
        [Flags]
        public enum statusCode
        {
            Success = 1,
            Error = 0
        }
        public enum headerParams
        {
            headerToken,
            APPName
        }
    }
}