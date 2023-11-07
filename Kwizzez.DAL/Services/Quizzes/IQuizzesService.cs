using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Utilities;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.Quizzes
{
    public interface IQuizzesService
    {
        PaginatedList<QuizDto> GetPaginatedQuizzes(int pageIndex, int pageSize);
        QuizDetailedDto? GetQuizById(string code);
        QuizDetailedDto? GetQuizByCode(int code);
        void AddQuiz(QuizDetailedDto QuizDetailedDto);
        void UpdateQuiz(QuizDetailedDto QuizDetailedDto);
        void DeleteQuiz(QuizDetailedDto QuizDetailedDto);
        void DeleteQuizzes(IEnumerable<QuizDetailedDto> quizzesDtos);
        bool QuizExists(string id);
    }
}
