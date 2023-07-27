using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.Domain.Common;

namespace Kwizzez.Domain.Entities
{
    public class Answer : Base
    {
        public string QuestionId { get; set; }
        public string Title { get; set; }
        public bool IsCorrect { get; set; }

        // Navigation Props

        public Question? Question { get; set; }
        public List<StudentScoreAnswer>? ScoreAnswers { get; set; }
    }
}
