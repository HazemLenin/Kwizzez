﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Dtos.Auth
{
    public class RefreshTokenDto
    {
        public string RefreshToken { get; set; }
    }
}
