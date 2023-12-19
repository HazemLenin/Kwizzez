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
using Kwizzez.DAL.Migrations;
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

        public string AddQuiz(AddQuizDto QuizAddDto, string teacherId)
        {
            var quiz = _mapper.Map<Quiz>(QuizAddDto);
            quiz.ApplicationUserId = teacherId;
            quiz.Score = quiz.Questions.Sum(q => q.Degree);
            quiz.QuestionsNumber = quiz.Questions.Count();

            _unitOfWork.quizzesRepository.Add(quiz);
            _unitOfWork.Save();
            return quiz.Id;
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
                              Id = quiz.Id,
                              Description = quiz.Description,
                              Title= quiz.Title,
                              Score= quiz.Score,
                              QuestionsNumber= quiz.QuestionsNumber,
                              TeacherId= quiz.ApplicationUserId,
                              TeacherName= $"{teacher.FirstName} {teacher.LastName}",
                              UpdatedAt = quiz.UpdatedAt,
                              CreatedAt = quiz.CreatedAt
                          };

            return PaginatedList<QuizDto>.Create(quizzes, pageNumber, pageSize);
        }

        public QuizDetailedDto? GetDetailedQuizById(string id)
        {
            var quiz = _unitOfWork.quizzesRepository.GetById(id, "ApplicationUser,Questions,Questions.Answers,StudentScores");
            
            return _mapper.Map<QuizDetailedDto>(quiz);
        }

        public void UpdateQuiz(EditQuizDto editQuizDto)
        {
            var quiz = _unitOfWork.quizzesRepository.GetById(editQuizDto.Id);


            // Update the quiz only without its questions
            quiz.Score = editQuizDto.Questions.Sum(q => q.Degree);
            quiz.QuestionsNumber = editQuizDto.Questions.Count();
            quiz.Id = editQuizDto.Id;
            quiz.Title = editQuizDto.Title;
            quiz.Description = editQuizDto.Description;

            _unitOfWork.quizzesRepository.Update(quiz);

            // Updating the old questions
            foreach (var editedQuestionDto in editQuizDto.Questions.Where(q => q.Id != null))
            {
                var question = _unitOfWork.questionsRepository.GetById(editedQuestionDto.Id);
                question.Title = editedQuestionDto.Title;
                question.Degree = editedQuestionDto.Degree;

                _unitOfWork.questionsRepository.Update(question);

                // Updating the old answers
                foreach (var editedAnswerDto in editedQuestionDto.Answers.Where(a => a.Id != null))
                {
                    var answer = _unitOfWork.answersRepository.GetById(editedAnswerDto.Id);
                    answer.Title = editedAnswerDto.Title;
                    answer.IsCorrect = editedAnswerDto.IsCorrect;

                    _unitOfWork.answersRepository.Update(answer);
                }

                // Adding the new answers
                foreach(var newAnswerDto in editedQuestionDto.Answers.Where(a => a.Id == null))
                {
                    var newAnswer = _mapper.Map<Answer>(newAnswerDto);
                    // Changing the ID because it's mapped from edit dto not add dto
                    newAnswer.Id = Guid.NewGuid().ToString();
                    newAnswer.QuestionId = editedQuestionDto.Id;
                    _unitOfWork.answersRepository.Add(newAnswer);
                }

                // Deleting the removed answers
                var removedAnswersIds = _unitOfWork.answersRepository.GetAll(new()
                {
                    Filter = a => a.QuestionId == editedQuestionDto.Id && !editedQuestionDto.Answers.Select(a => a.Id).Contains(a.Id)
                }).Select(a => a.Id);

                _unitOfWork.answersRepository.DeleteRange(removedAnswersIds);
            }

            // Adding the new questions
            foreach (var newQuestionDto in editQuizDto.Questions.Where(q => q.Id == null))
            {
                var question = _mapper.Map<Question>(newQuestionDto);
                // Changing the ID because it's mapped from edit dto not add dto
                question.Id = Guid.NewGuid().ToString();
                question.Answers.ForEach(a => a.Id = Guid.NewGuid().ToString());
                question.QuizId = editQuizDto.Id;
                _unitOfWork.questionsRepository.Add(question);
            }

            // Deleting the removed questions
            var removedQuestionsIds = _unitOfWork.questionsRepository.GetAll(new()
            {
                Filter = q => q.QuizId == editQuizDto.Id && !editQuizDto.Questions.Select(q => q.Id).Contains(q.Id)
            }).Select(q => q.Id);
            
            _unitOfWork.questionsRepository.DeleteRange(removedQuestionsIds);

            // Deleting removed questions answers
            var removedQuestionAnswersIds = _unitOfWork.answersRepository.GetAll(new()
            {
                Filter = a => removedQuestionsIds.Contains(a.QuestionId)
            }).Select(a => a.Id);

            _unitOfWork.answersRepository.DeleteRange(removedQuestionAnswersIds);

            _unitOfWork.Save();
        }

        public bool QuizExists(string id)
        {
            return _unitOfWork.quizzesRepository.GetAll(new()
            {
                Filter = q => q.Id == id
            }).Any();
        }

        public QuizDto? GetQuizById(string quizId, string studentId)
        {
            var quiz = (from q in _unitOfWork.quizzesRepository.GetAll()
                        
                        join t in _userManager.Users
                        on q.ApplicationUserId equals t.Id 
                        
                        where q.Id == quizId
                        orderby q.CreatedAt descending
                        select new QuizDto()
                        {
                            Id = q.Id,
                            Title = q.Title,
                            Score = q.Score,
                            QuestionsNumber = q.QuestionsNumber,
                            TeacherId = q.ApplicationUserId,
                            TeacherName = $"{t.FirstName} {t.LastName}",
                            Description = q.Description,
                            CreatedAt = q.CreatedAt,
                            UpdatedAt = q.UpdatedAt
                        }).FirstOrDefault();

            if (quiz != null)
            {
                var studentScore = _unitOfWork.studentScoresRepository.GetAll(new()
                {
                    Filter = s => s.QuizId == quiz.Id && s.ApplicationUserId == studentId
                }).FirstOrDefault();

                if (studentScore != null)
                {
                    quiz.Took = true;
                    quiz.Finished = studentScore.Finished;
                    quiz.StudentScoreId = studentScore.Id;
                }
            }

            return quiz;
        }

        public PaginatedList<QuizDto> GetPaginatedUserQuizzes(string userId, int pageNumber, int pageSize)
        {
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
                              Description = quiz.Description,
                              QuestionsNumber= quiz.QuestionsNumber,
                              TeacherId= quiz.ApplicationUserId,
                              TeacherName= $"{teacher.FirstName} {teacher.LastName}",
                              CreatedAt = quiz.CreatedAt,
                              UpdatedAt = quiz.UpdatedAt
                          };

            return PaginatedList<QuizDto>.Create(quizzes, pageNumber, pageSize);
        }

        public List<QuestionForStudentDto>? GetQuizQuestionsById(string id)
        {
            var questions = _unitOfWork.questionsRepository.GetAll(new() {
                Filter = questions => questions.QuizId == id,
                IncludeProperties = "Answers"
            });

            var mappedQuestions = _mapper.Map<List<QuestionForStudentDto>>(questions);

            return mappedQuestions;
        }

        public void StartQuiz(string quizId, string studentId)
        {
            StudentScore studentScore = new()
            {
                ApplicationUserId = studentId,
                QuizId = quizId,
            };

            _unitOfWork.studentScoresRepository.Add(studentScore);
            _unitOfWork.Save();
        }

        public bool AnswerRelatedToQuiz(string quizId, string answerId)
        {
            var questionId = _unitOfWork.answersRepository.GetAll(new()
            {
                Filter = a => a.Id == answerId
            })
                .Select(a => a.QuestionId)
                .First();

            var questionQuizId = _unitOfWork.questionsRepository.GetAll(new()
            {
                Filter = q => q.Id == questionId
            })
                .Select(q => q.QuizId)
                .First();

            return questionQuizId == quizId;
        }
    }
}
