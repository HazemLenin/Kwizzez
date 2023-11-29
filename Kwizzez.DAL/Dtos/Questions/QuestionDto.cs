using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Common;
using Kwizzez.DAL.Dtos.Answers;
using Kwizzez.DAL.Dtos.Quizzes;

namespace Kwizzez.DAL.Dtos.Questions
{
    public class QuestionDto : BaseDto
    {
        public string QuizId { get; set; }
        public string Title { get; set; }
        public byte[]? Image { get; set; }
        public int Order { get; set; }
        public int Degree { get; set; }

        // Navigation Props

        public QuizDetailedDto? Quiz { get; set; }
        public List<AnswerDto>? Answers { get; set; }
    }
}
