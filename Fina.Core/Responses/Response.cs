using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fina.Core.Responses
{
    public class Response<TData>
    {
        private int Code = Configuration.DefautStatusCode;

        [JsonConstructor]
        public Response()
            => Code = Configuration.DefautStatusCode;

        public Response(
            TData? data,
            int code = Configuration.DefautStatusCode,
            string? message = null)
        {
            Data = data;
            Code = code;
            Message = message;
        }

        public TData? Data { get; set; }

        public string? Message { get; set; }

        [JsonIgnore]
        public bool IsSuccess => Code is >= 200 and  <= 299;
    }
}
