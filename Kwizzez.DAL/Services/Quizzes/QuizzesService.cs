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
using Kwizzez.DAL.Utilities;
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
            _unitOfWork.Save();
        }

        public void DeleteQuiz(QuizDto quizDto)
        {
            var quiz = _mapper.Map<Quiz>(quizDto);

            _unitOfWork.quizzesRepository.Delete(quiz);
            _unitOfWork.Save();
        }

        public void DeleteQuizzes(IEnumerable<QuizDto> quizzesDtos)
        {
            var quizzes = _mapper.Map<List<Quiz>>(quizzesDtos);

            _unitOfWork.quizzesRepository.DeleteRange(quizzes);
            _unitOfWork.Save();
        }

        public PaginatedList<QuizDto> GetPaginatedQuizzes(int pageNumber, int pageSize)
        {
            var quizzes = _unitOfWork.quizzesRepository
                .GetAll(new()
                {
                    Filter = q => q.IsPublic,
                    OrderExpression = quizzes => quizzes.OrderByDescending(q => q.CreatedAt)
                })
                .Select(q => _mapper.Map<QuizDto>(q));

            return PaginatedList<QuizDto>.Create(quizzes, pageNumber, pageSize);
        }

        public QuizDto? GetQuizById(string id)
        {
            var quiz = _unitOfWork.quizzesRepository.GetById(id);

            return _mapper.Map<QuizDto>(quiz);
        }

        public QuizDto? GetQuizByCode(int code)
        {
            var quiz = _unitOfWork.quizzesRepository.GetAll(new()
            {
                Filter = q => q.Code == code
            }).FirstOrDefault();

            return _mapper.Map<QuizDto>(quiz);
        }

        public void UpdateQuiz(QuizDto quizDto)
        {
            var quiz = _mapper.Map<Quiz>(quizDto);

            _unitOfWork.quizzesRepository.Update(quiz);
            _unitOfWork.Save();
        }

        public bool QuizExists(string id)
        {
            return _unitOfWork.quizzesRepository.GetAll(new()
            {
                Filter = q => q.Id == id
            }).Any();
        }
    }
}
