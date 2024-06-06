using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Core
{
    public static class Configuration
    {
        public const int DefautStatusCode = 200;
        public const int DefautPageNumber = 1;
        public const int DefautPageSize = 25;

        
        public static string BackendUrl { get; set; } = "http://localhost:5184";
        public static string FrontendUrl { get; set; } = "http://localhost:5050";

    }
}
