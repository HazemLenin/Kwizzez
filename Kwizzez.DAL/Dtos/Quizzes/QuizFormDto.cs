using Kwizzez.DAL.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Dtos.Quizzes
{
    public class QuizFormDto : BaseDto
    {
        [Required]
        public string Title { get; set; }
        public bool HasLimitedTime { get; set; } = false;
        public TimeSpan? TimeLimit { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
    }
}
