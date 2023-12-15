using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.StudentScores;
using Kwizzez.DAL.Utilities;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.StudentScores
{
    public interface IStudentScoresService
    {
        PaginatedList<StudentScoreDto> GetPaginatedStudentScore(int pageNumber, int pageSize);
        StudentScoreDto GetStudentScoreById(string id, string includeProperties = "");
        void AddStudentScore(StudentScoreDto studentScoreDto);
        void UpdateStudentScore(StudentScoreDto studentScoreDto);
        void DeleteStudentScore(string id);
        void DeleteStudentScores(IEnumerable<string> ids);
        string? GetStudentScoreId(string studentId, string quizId);
    }
}