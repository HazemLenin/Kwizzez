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
    public class QuizDto : BaseDto
    {
        public string Title { get; set; }
        public bool HasLimitedTime { get; set; }
        public TimeSpan? TimeLimit { get; set; }
        public long? TimeLimitTicks => TimeLimit.HasValue ? TimeLimit.Value.Ticks : null;
        public DateTime PublishDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int Code { get; set; }
        public int Score { get; set; }
        public string TeacherId { get; set; }

        // Navigation Props

        public UserDto? Teacher { get; set; }
        public List<QuestionDto>? Questions { get; set; }
        public List<StudentScoreDto>? StudentScores { get; set; }
    }
}
