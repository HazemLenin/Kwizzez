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
}
