using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.Domain.Common;

namespace Kwizzez.Domain.Entities
{
    public class Quiz : Base
    {
        public string Title { get; set; }
        // public bool HasLimitedTime { get; set; } = false;
        // public TimeSpan? TimeLimit { get; set; }
        // public DateTime PublishDate { get; set; }
        // public DateTime ExpirationDate { get; set; }
        public int Code { get; set; } = new Random().Next(1000000, 9999999);
        public int Score { get; set; }
        public string ApplicationUserId { get; set; }
        public bool IsPublic { get; set; } = true;
        public int QuestionsNumber { get; set; }
        public string? Description { get; set; }

        // Navigation Props

        public ApplicationUser? ApplicationUser { get; set; }
        public List<Question>? Questions { get; set; }
        public List<StudentScore>? StudentScores { get; set; }
    }
}
