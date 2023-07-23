using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Repositories;
using Kwizzez.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Services.Quizzes
{
    public class QuizzesService : IQuizzesService
    {
        private readonly GenericRepository<Quiz> _quizzesRepostiory;
        public QuizzesService(GenericRepository<Quiz> quizzesRepostiory)
        {
            _quizzesRepostiory = quizzesRepostiory;
        }

        public void AddQuiz(QuizFormDto quiz)
        {
            throw new NotImplementedException();
        }

        public void DeleteQuiz(Guid id)
        {
            throw new NotImplementedException();
        }

        public List<QuizDto> GetAllQuizzes(Expression<Func<Quiz, bool>> filter = null, string includeProperties = "", Func<IQueryable<Quiz>, IOrderedQueryable<Quiz>> orderExpression = null, int take = 0, int skip = 0)
        {
            throw new NotImplementedException();
        }

        public QuizDto? GetQuizByCode(int code)
        {
            throw new NotImplementedException();
        }

        public QuizDto? GetQuizById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void UpdateQuiz(QuizFormDto quiz)
        {
            throw new NotImplementedException();
        }
    }
}
