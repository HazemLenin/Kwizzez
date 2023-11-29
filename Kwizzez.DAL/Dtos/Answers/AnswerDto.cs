﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Common;
using Kwizzez.DAL.Dtos.Questions;
using Kwizzez.DAL.Dtos.StudentScoreAnswers;

namespace Kwizzez.DAL.Dtos.Answers
{
    public class AnswerDto : BaseDto
    {
        public string QuestionId { get; set; }
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
        public int Order { get; set; }

        // Navigation Props

        public QuestionDto? Question { get; set; }
        public List<StudentScoreAnswerDto>? ScoreAnswers { get; set; }
    }
}
