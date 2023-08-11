using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Dtos.Responses
{
    public class ApiResponse<T>
    {
        public bool IsSucceed => Errors == null;
        public T? Data { get; set; }
        public Dictionary<string, List<string>>? Errors { get; set; }
    }

    public class ApiPaginatedResponse<T> : ApiResponse<T>
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious { get; set; }
        public bool HasNext { get; set; }
    }

    public class ApiResponse
    {
        public bool IsSucceed => Errors == null;
        public Dictionary<string, List<string>>? Errors { get; set; }
    }
}
