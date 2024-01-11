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
    public class StudentScoreAnswersDto : BaseDto
    {
        public Dictionary<string, string> AnswersIds { get; set; }
        public int Score { get; set; }
    }
}
