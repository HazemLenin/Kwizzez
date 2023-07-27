using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.StudentScores;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.StudentScores
{
    public interface IStudentScoresService
    {
        List<StudentScoreDto> GetAllStudentScores(QueryFilter<StudentScore> queryFilter);
        StudentScoreDto GetStudentScoreById(string id, string includeProperties = "");
        void AddStudentScore(StudentScoreDto studentScoreDto);
        void UpdateStudentScore(StudentScoreDto studentScoreDto);
        void DeleteStudentScore(StudentScoreDto studentScoreDto);
        void DeleteStudentScores(IEnumerable<StudentScoreDto> studentScoreDtos);
    }
}