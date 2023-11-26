﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Kwizzez.DAL.Common;
using Kwizzez.DAL.Dtos.Questions;

namespace Kwizzez.DAL.Dtos.Quizzes
{
    public class AddQuizDto
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }
        public List<QuestionFormDto>? Questions { get; set; }
    }
}
