using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Dtos.Auth
{
    public class AuthDto
    {
        [JsonIgnore]
        public bool IsSucceed => Errors == null;
        [JsonIgnore]
        public Dictionary<string, List<string>>? Errors { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<string>? Roles { get; set; }
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }
    }
}
