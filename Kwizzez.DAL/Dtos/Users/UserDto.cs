using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Dtos.StudentScores;
using Kwizzez.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Dtos.Users
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public List<string> Roles { get; set; }

        // Navigation Props

        public List<QuizDto>? Quizzes { get; set; }
        public List<StudentScoreDto>? StudentScores { get; set; }
    }
}
