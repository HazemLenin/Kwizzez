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
        List<QuizDto> GetAllQuizzes(Expression<Func<Quiz, bool>> filter = null,
            string includeProperties = "",
            Func<IQueryable<Quiz>, IOrderedQueryable<Quiz>> orderExpression = null,
            int take = 0, int skip = 0);
        QuizDto? GetQuizByCode(int code);
        void AddQuiz(QuizDto quizDto);
        void UpdateQuiz(QuizDto quizDto);
        void DeleteQuiz(QuizDto quizDto);
        void DeleteQuizs(IEnumerable<QuizDto> quizzesDtos);
    }
}
