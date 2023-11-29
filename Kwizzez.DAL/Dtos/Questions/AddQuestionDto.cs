using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Common;
using Kwizzez.DAL.Dtos.Answers;
using Kwizzez.DAL.Dtos.Quizzes;

namespace Kwizzez.DAL.Dtos.Questions
{
    public class AddQuestionDto
    {
        [Required]
        public string Title { get; set; }
        public byte[]? Image { get; set; }
        [Required]
        public int Order { get; set; }
        [Required]
        public int Degree { get; set; }

        // Navigation Props
        [Required]
        public List<AddAnswerDto> Answers { get; set; }
    }
}
