using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Common;
using Kwizzez.DAL.Dtos.Questions;
using Kwizzez.DAL.Dtos.StudentScores;
using Kwizzez.DAL.Dtos.Users;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Dtos.Quizzes
{
    public class QuizDetailedDto : BaseDto
    {
        public string Title { get; set; }
        public int Score { get; set; }
        public string TeacherId { get; set; }
        public int QuestionsNumber { get; set; }
        public string Description { get; set; }
        public int Code { get; set; }
        public bool IsPublic { get; set; }

        // Navigation Props

        public UserDto? Teacher { get; set; }
        public List<QuestionDto>? Questions { get; set; }
        public List<StudentScoreDto>? StudentScores { get; set; }
    }
}
