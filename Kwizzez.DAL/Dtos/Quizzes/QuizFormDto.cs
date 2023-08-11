using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Kwizzez.DAL.Common;
using Kwizzez.DAL.Dtos.Questions;

namespace Kwizzez.DAL.Dtos.Quizzes
{
    public class QuizFormDto
    {
        [Required]
        public string Title { get; set; }
        public bool HasLimitedTime { get; set; } = false;
        public long? TimeLimitTicks { get; set; }
        [JsonIgnore]
        public TimeSpan? TimeLimit => TimeLimitTicks.HasValue ? new(TimeLimitTicks.Value) : null;
        [Required]
        public DateTime PublishDate { get; set; }
        [Required]
        public DateTime ExpirationDate { get; set; }
        public List<QuestionFormDto>? Questions { get; set; }
    }
}
