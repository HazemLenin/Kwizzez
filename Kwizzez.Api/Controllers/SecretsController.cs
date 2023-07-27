using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kwizzez.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class SecretsController : Controller
    {
        [HttpGet("Secret")]
        public ApiResponse<string> Secret()
        {
            return new()
            {
                Data = "Horray!"
            };
        }
    }
}