using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Fina.Core.Responses
{
    public class PagedResponse<TData> : Response<TData>
    {
        [JsonConstructor]
        public PagedResponse(
            TData? data,
            int totalcount,
            int currentPage=1,
            int pageSize = Configuration.DefautPageSize)
            :base(data)
        {
            Data = data;
            TotalCount = totalcount;
            CurrentPage = currentPage;
            PageSize = pageSize;
        }

        public PagedResponse(
            TData? data,
            int code = Configuration.DefautStatusCode,
            string? message = null): base(data, code, message)
        {

        }
      
        public int CurrentPage { get; set; }

        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
        
        public int PageSize { get; set; } = Configuration.DefautPageSize;
        
        public int TotalCount { get; set; }
    }
}
