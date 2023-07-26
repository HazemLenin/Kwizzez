using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Dtos.Responses
{
    public class ApiResponse<T>
    {
        public bool IsSucceed { get; set; } = true;
        public T? Data { get; set; }
        public Dictionary<string, string>? Errors { get; set; }
    }
}
