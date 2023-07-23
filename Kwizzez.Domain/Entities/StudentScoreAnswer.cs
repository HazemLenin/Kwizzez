using Kwizzez.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.Domain.Entities
{
    public class StudentScoreAnswer : Base
    {
        public Guid StudentScoreId { get; set; }
        public Guid AnswerId { get; set; }

        // Navigation Props

        public StudentScore? StudentScore { get; set; }
        public Answer? Answer { get; set; }
    }
}
