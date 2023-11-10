using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Dtos.Questions;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Dtos.StudentScores;
using Kwizzez.DAL.Dtos.Users;
using Kwizzez.DAL.Repositories;
using Kwizzez.DAL.UnitOfWork;
using Kwizzez.DAL.Utilities;
using Kwizzez.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Kwizzez.DAL.Services.Quizzes
{
    public class QuizzesService : IQuizzesService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        public QuizzesService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public void AddQuiz(QuizDetailedDto QuizDetailedDto)
        {
            var quiz = _mapper.Map<Quiz>(QuizDetailedDto);

            _unitOfWork.quizzesRepository.Add(quiz);
            _unitOfWork.Save();
        }

        public void DeleteQuiz(QuizDetailedDto QuizDetailedDto)
        {
            var quiz = _mapper.Map<Quiz>(QuizDetailedDto);

            _unitOfWork.quizzesRepository.Delete(quiz);
            _unitOfWork.Save();
        }

        public void DeleteQuizzes(IEnumerable<QuizDetailedDto> quizzesDtos)
        {
            var quizzes = _mapper.Map<List<Quiz>>(quizzesDtos);

            _unitOfWork.quizzesRepository.DeleteRange(quizzes);
            _unitOfWork.Save();
        }

        public PaginatedList<QuizDto> GetPaginatedQuizzes(int pageNumber, int pageSize)
        {
            // var quizzes = _unitOfWork.quizzesRepository
            //     .GetAll(new()
            //     {
            //         Filter = q => q.IsPublic,
            //         OrderExpression = quizzes => quizzes.OrderByDescending(q => q.CreatedAt)
            //     })
            //     .Select(q => _mapper.Map<QuizDto>(q));
            var quizzes = from quiz in _unitOfWork.quizzesRepository.GetAll()
                          join teacher in _userManager.Users
                          on quiz.ApplicationUserId equals teacher.Id
                          where quiz.IsPublic &&
                                teacher.IsActive
                          orderby quiz.CreatedAt descending
                          select new QuizDto()
                          {
                              Title= quiz.Title,
                              Score= quiz.Score,
                              QuestionsNumber= quiz.QuestionsNumber,
                              TeacherId= quiz.ApplicationUserId,
                              TeacherName= $"{teacher.FirstName} {teacher.LastName}",
                          };

            // quizzes = new List<QuizDto>(){
            //     new() {
            //         Id = "1",
            //         CreatedAt = DateTime.Now,
            //         QuestionsNumber = 3,
            //         Score = 300,
            //         TeacherId = "1",
            //         TeacherName = "teacher 1",
            //         Title = "Quiz 1: about engineering and the software development",
            //         Description = "This is just a description to provide you with essential information  about the quiz and for the clarification about our website.!!",
            //         UpdatedAt = DateTime.Now
            //     },
            //     new() {
            //         Id = "2",
            //         CreatedAt = DateTime.Now,
            //         QuestionsNumber = 3,
            //         Score = 300,
            //         TeacherId = "1",
            //         TeacherName = "teacher 1",
            //         Title = "Quiz 2: about graphic design and history",
            //         Description = "This is just a description to provide you with essential information  about the quiz and for the clarification about our website.!!",
            //         UpdatedAt = DateTime.Now
            //     },
            //     new() {
            //         Id = "3",
            //         CreatedAt = DateTime.Now,
            //         QuestionsNumber = 3,
            //         Score = 300,
            //         TeacherId = "1",
            //         TeacherName = "teacher 1",
            //         Title = "Quiz 2: about graphic design and history",
            //         Description = "This is just a description to provide you with essential information  about the quiz and for the clarification about our website.!!",
            //         UpdatedAt = DateTime.Now
            //     },
            //     new() {
            //         Id = "4",
            //         CreatedAt = DateTime.Now,
            //         QuestionsNumber = 3,
            //         Score = 300,
            //         TeacherId = "1",
            //         TeacherName = "teacher 1",
            //         Title = "Quiz 2: about graphic design and history",
            //         Description = "This is just a description to provide you with essential information  about the quiz and for the clarification about our website.!!",
            //         UpdatedAt = DateTime.Now
            //     },
            //     new() {
            //         Id = "5",
            //         CreatedAt = DateTime.Now,
            //         QuestionsNumber = 3,
            //         Score = 300,
            //         TeacherId = "1",
            //         TeacherName = "teacher 1",
            //         Title = "Quiz 2: about graphic design and history",
            //         Description = "This is just a description to provide you with essential information  about the quiz and for the clarification about our website.!!",
            //         UpdatedAt = DateTime.Now
            //     },
            // }.AsQueryable();

            return PaginatedList<QuizDto>.Create(quizzes, pageNumber, pageSize);
        }

        public QuizDetailedDto? GetQuizById(string id)
        {
            var quiz = _unitOfWork.quizzesRepository.GetAll(new() {
                IncludeProperties = "Teacher,Questions,StudentScores"
            }).FirstOrDefault(q => q.Id == id);

            return _mapper.Map<QuizDetailedDto>(quiz);
        }

        public QuizDetailedDto? GetQuizByCode(int code)
        {
            var quiz = _unitOfWork.quizzesRepository.GetAll(new() {
                IncludeProperties = "Teacher,Questions,StudentScores"
            }).FirstOrDefault(q => q.Code == code);

            return _mapper.Map<QuizDetailedDto>(quiz);
        }

        public void UpdateQuiz(QuizDetailedDto QuizDetailedDto)
        {
            var quiz = _mapper.Map<Quiz>(QuizDetailedDto);

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

        public QuizInfoDto? GetQuizInfo(string id)
        {
            var quiz = (from q in _unitOfWork.quizzesRepository.GetAll()
                        join t in _userManager.Users
                        on q.ApplicationUserId equals t.Id
                        where q.Id == id
                        orderby q.CreatedAt descending
                        select new QuizInfoDto()
                        {
                            Title = q.Title,
                            Score = q.Score,
                            QuestionsNumber = q.QuestionsNumber,
                            TeacherId = q.ApplicationUserId,
                            TeacherName = $"{t.FirstName} {t.LastName}",
                            Description = q.Description,
                            HasLimitedTime = q.HasLimitedTime,
                            TimeLimit = q.TimeLimit,
                            PublishDate = q.PublishDate,
                            ExpirationDate = q.ExpirationDate,
                        }).FirstOrDefault();

            return quiz;
        }
    }
}
