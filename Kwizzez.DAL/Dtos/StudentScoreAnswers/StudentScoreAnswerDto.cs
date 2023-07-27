using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Common;
using Kwizzez.DAL.Dtos.Answers;
using Kwizzez.DAL.Dtos.StudentScores;

namespace Kwizzez.DAL.Dtos.StudentScoreAnswers
{
    public class StudentScoreAnswerDto : BaseDto
    {
        public string StudentScoreId { get; set; }
        public string AnswerId { get; set; }

        // Navigation Props

        public StudentScoreDto? StudentScore { get; set; }
        public AnswerDto? Answer { get; set; }
    }
}
