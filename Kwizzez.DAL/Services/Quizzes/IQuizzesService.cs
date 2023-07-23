using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Services.Quizzes
{
    public interface IQuizzesService
    {
        List<QuizDto> GetAllQuizzes(Expression<Func<Quiz, bool>> filter = null,
            string includeProperties = "",
            Func<IQueryable<Quiz>, IOrderedQueryable<Quiz>> orderExpression = null,
            int take = 0, int skip = 0);
        QuizDto? GetQuizById(Guid id);
        QuizDto? GetQuizByCode(int code);
        void AddQuiz(QuizFormDto quiz);
        void UpdateQuiz(QuizFormDto quiz);
        void DeleteQuiz(Guid id);
    }
}
