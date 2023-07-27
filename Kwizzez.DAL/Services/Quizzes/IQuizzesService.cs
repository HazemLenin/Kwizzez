using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.Quizzes
{
    public interface IQuizzesService
    {
        List<QuizDto> GetAllQuizzes(QueryFilter<Quiz> queryFilter);
        QuizDto? GetQuizByCode(int code);
        void AddQuiz(QuizDto quizDto);
        void UpdateQuiz(QuizDto quizDto);
        void DeleteQuiz(QuizDto quizDto);
        void DeleteQuizzes(IEnumerable<QuizDto> quizzesDtos);
    }
}
