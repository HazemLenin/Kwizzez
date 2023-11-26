using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Data;
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
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public QuizzesService(IUnitOfWork unitOfWork, IMapper mapper, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        public void AddQuiz(AddQuizDto QuizAddDto, string teacherId)
        {
            var quiz = _mapper.Map<Quiz>(QuizAddDto);
            quiz.ApplicationUserId = teacherId;
            quiz.Score = quiz.Questions.Sum(q => q.Degree);

            _unitOfWork.quizzesRepository.Add(quiz);
            _unitOfWork.Save();
        }

        public void DeleteQuiz(string id)
        {
            _unitOfWork.quizzesRepository.Delete(id);
            _unitOfWork.Save();
        }

        public void DeleteQuizzes(IEnumerable<string> ids)
        {
            _unitOfWork.quizzesRepository.DeleteRange(ids);
            _unitOfWork.Save();
        }

        public PaginatedList<QuizDto> GetPaginatedQuizzes(int pageNumber, int pageSize)
        {
            var quizzes = from quiz in _unitOfWork.quizzesRepository.GetAll()
                          join teacher in _userManager.Users
                          on quiz.ApplicationUserId equals teacher.Id
                          where teacher.IsActive
                          orderby quiz.CreatedAt descending
                          select new QuizDto()
                          {
                              Title= quiz.Title,
                              Score= quiz.Score,
                              QuestionsNumber= quiz.QuestionsNumber,
                              TeacherId= quiz.ApplicationUserId,
                              TeacherName= $"{teacher.FirstName} {teacher.LastName}",
                          };

            return PaginatedList<QuizDto>.Create(quizzes, pageNumber, pageSize);
        }

        public QuizDetailedDto? GetQuizById(string id)
        {
            var quiz = _unitOfWork.quizzesRepository.GetAll(new() {
                IncludeProperties = "ApplicationUser,Questions,StudentScores"
            }).FirstOrDefault(q => q.Id == id);
            
            return _mapper.Map<QuizDetailedDto>(quiz);
        }

        public void UpdateQuiz(EditQuizDto editQuizDto)
        {
            var quiz = _unitOfWork.quizzesRepository.GetById(editQuizDto.Id);

            quiz.Score = quiz.Questions.Sum(q => q.Degree);
            quiz.Id = editQuizDto.Id;
            quiz.Title = editQuizDto.Title;
            quiz.Description = editQuizDto.Description;
            quiz.Questions = _mapper.Map<List<Question>>(editQuizDto.Questions);

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
                            CreatedAt = q.CreatedAt,
                            UpdatedAt = q.UpdatedAt
                        }).FirstOrDefault();

            return quiz;
        }

        public PaginatedList<QuizDto> GetPaginatedUserQuizzes(string userId, int pageNumber, int pageSize)
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
                          where teacher.Id == userId
                          orderby quiz.CreatedAt descending
                          select new QuizDto()
                          {
                              Id = quiz.Id,
                              Title= quiz.Title,
                              Score= quiz.Score,
                              QuestionsNumber= quiz.QuestionsNumber,
                              TeacherId= quiz.ApplicationUserId,
                              TeacherName= $"{teacher.FirstName} {teacher.LastName}",
                              CreatedAt = quiz.CreatedAt,
                              UpdatedAt = quiz.UpdatedAt
                          };

            return PaginatedList<QuizDto>.Create(quizzes, pageNumber, pageSize);
        }
    }
}
