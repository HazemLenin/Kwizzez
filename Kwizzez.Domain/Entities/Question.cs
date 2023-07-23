﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.Domain.Common;

namespace Kwizzez.Domain.Entities
{
    public class Question : Base
    {
        public Guid QuizId { get; set; }
        public string Title { get; set; }
        
        // Navigation Props

        public Quiz? Quiz { get; set; }
        public List<Answer>? Answers { get; set; }
    }
}
