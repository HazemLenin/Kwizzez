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
    public class QuizInfoDto : BaseDto
    {
        public string Title { get; set; }
        public int Score { get; set; }
        public int QuestionsNumber { get; set; }
        public string TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string Description { get; set; }
        public bool HasLimitedTime { get; set; } = false;
        public TimeSpan? TimeLimit { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
