using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Common;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Dtos.StudentScoreAnswers;
using Kwizzez.DAL.Dtos.Users;

namespace Kwizzez.DAL.Dtos.StudentScores
{
    public class StudentScoreDto : BaseDto
    {
        public string ApplicationUserId { get; set; }
        public string QuizId { get; set; }
        public int Score { get; set; }

        // Navigation Props

        public UserDto? ApplicationUser { get; set; }
        public QuizDetailedDto? Quiz { get; set; }
        public List<StudentScoreAnswerDto>? StudentScoreAnswers { get; set; }
    }
}
