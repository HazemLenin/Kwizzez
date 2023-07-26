using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Repositories;
using Kwizzez.DAL.UnitOfWork;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.Quizzes
{
    public class QuizzesService : IQuizzesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuizzesService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddQuiz(QuizDto quizDto)
        {
            var quiz = _mapper.Map<Quiz>(quizDto);

            _unitOfWork.quizzesRepository.Add(quiz);
        }

        public void DeleteQuiz(QuizDto quizDto)
        {
            var quiz = _mapper.Map<Quiz>(quizDto);

            _unitOfWork.quizzesRepository.Delete(quiz);
        }

        public void DeleteQuizs(IEnumerable<QuizDto> quizzesDtos)
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

        public void UpdateQuiz(QuizDto quizDto)
        {
            throw new NotImplementedException();
        }
    }
}
