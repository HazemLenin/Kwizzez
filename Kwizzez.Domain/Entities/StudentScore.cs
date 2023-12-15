using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.Domain.Common;

namespace Kwizzez.Domain.Entities
{
    public class StudentScore : Base
    {
        public string ApplicationUserId { get; set; }
        public string QuizId { get; set; }
        public int Score { get; set; }
        public bool Finished { get; set; } = false;
        
        // Navigation Props

        public ApplicationUser? ApplicationUser { get; set; }
        public Quiz? Quiz { get; set; }
        public List<StudentScoreAnswer>? StudentScoreAnswers { get; set; }
    }
}
