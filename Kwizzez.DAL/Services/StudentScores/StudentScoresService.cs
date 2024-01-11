using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Dtos.StudentScores;
using Kwizzez.DAL.UnitOfWork;
using Kwizzez.DAL.Utilities;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.StudentScores
{
    public class StudentScoresService : IStudentScoresService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public StudentScoresService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddStudentScore(StudentScoreDto studentScoreDto)
        {
            var studentScore = _mapper.Map<StudentScore>(studentScoreDto);
            _unitOfWork.studentScoresRepository.Add(studentScore);
        }

        public void DeleteStudentScore(string id)
        {
            _unitOfWork.studentScoresRepository.Delete(id);
        }

        public void DeleteStudentScores(IEnumerable<string> ids)
        {
            _unitOfWork.studentScoresRepository.DeleteRange(ids);
        }

        public PaginatedList<StudentScoreDto> GetPaginatedStudentScore(int pageNumber, int pageSize)
        {
            var scores = _unitOfWork.studentScoresRepository
                .GetAll(new()
                {
                    OrderExpression = scores => scores.OrderByDescending(s => s.CreatedAt)
                })
                .Select(q => _mapper.Map<StudentScoreDto>(q));

            return PaginatedList<StudentScoreDto>.Create(scores, pageNumber, pageSize);
        }

        public StudentScoreDto GetStudentScoreById(string id)
        {
            var studentScore = _unitOfWork.studentScoresRepository.GetById(id, "StudentScoreAnswers");
            return _mapper.Map<StudentScoreDto>(studentScore);
        }

        public StudentScoreAnswersDto GetStudentScoreAnswersById(string id)
        {
            var studentScore = _unitOfWork.studentScoresRepository.GetById(id);
            var dto = _mapper.Map<StudentScoreAnswersDto>(studentScore);
            dto.AnswersIds = new();
            dto.Score = studentScore.Score;
            var studentScoreAnswers = _unitOfWork.studentScoreAnswersRepository.GetAll(new()
            {
                Filter = a => a.StudentScoreId == id
            })
                .Select(a => new
                {
                    a.AnswerId,
                    a.Answer.QuestionId
                });
            studentScoreAnswers.ToList().ForEach(a => dto.AnswersIds.Add(a.QuestionId, a.AnswerId));
            return dto;
        }

        public void UpdateStudentScore(StudentScoreDto studentScoreDto)
        {
            var studentScore = _mapper.Map<StudentScore>(studentScoreDto);
            _unitOfWork.studentScoresRepository.Update(studentScore);
        }

        public string? GetStudentScoreId(string studentId, string quizId)
        {
            var studentScoreIds = _unitOfWork.studentScoresRepository.GetAll(new() {
                Filter = s => s.ApplicationUserId == studentId && s.QuizId == quizId
            }).Select(s => s.Id);

            return studentScoreIds.FirstOrDefault();
        }

        public void SelectAnswer(string answerId, string studentScoreId)
        {
            var questionId = _unitOfWork.answersRepository.GetAll(new()
            {
                Filter = a => a.Id == answerId
            })
                .Select(a => a.QuestionId)
                .First();

            var studentScoreAnswers = _unitOfWork.studentScoreAnswersRepository.GetAll(new()
            {
                Filter = a => a.StudentScoreId == studentScoreId,
            })
                .Select(a => new
                {
                    a.Id,
                    a.AnswerId,
                    a.Answer.QuestionId
                })
                .ToList();

            // Get old answer for the same question and delete it
            var oldSelectedAnswer = studentScoreAnswers.FirstOrDefault(a => a.QuestionId == questionId);

            if (oldSelectedAnswer != null)
                _unitOfWork.studentScoreAnswersRepository.Delete(oldSelectedAnswer.Id);

            // Add the new answer
            _unitOfWork.studentScoreAnswersRepository.Add(new()
            {
                AnswerId = answerId,
                StudentScoreId = studentScoreId,
            });

            _unitOfWork.Save();
        }

        public bool IsFinished(string id)
        {
            return _unitOfWork.studentScoresRepository.GetAll(new()
            {
                Filter = s => s.Id == id
            }).Select(s => s.Finished).First();
        }

        public void FinishScore(string id)
        {
            var studentScore = _unitOfWork.studentScoresRepository.GetById(id);

            studentScore.Score = 0;

            // Get score answers
            var studentScoreAnswers = _unitOfWork.studentScoreAnswersRepository.GetAll(new()
            {
                Filter = a => a.StudentScoreId == studentScore.Id
            })
                .Select(a => new
                {
                    a.Id,
                    a.AnswerId
                });

            // for each answer
            foreach (var scoreAnswer in studentScoreAnswers)
            {
                // get the real answer is correct field
                var answer = _unitOfWork.answersRepository.GetAll(new()
                {
                    Filter = a => a.Id == scoreAnswer.AnswerId
                })
                    .Select(a => new
                    {
                        a.IsCorrect,
                        a.QuestionId
                    })
                    .First();
                // if correct
                if (answer.IsCorrect)
                {
                    // get question score and add it to student score
                    var questionDegree = _unitOfWork.questionsRepository.GetAll(new()
                    {
                        Filter = q => q.Id == answer.QuestionId
                    })
                        .Select(q => new
                        {
                            q.Degree
                        })
                        .First();

                    studentScore.Score += questionDegree.Degree;
                }
            }


            studentScore.Finished = true;

            _unitOfWork.studentScoresRepository.Update(studentScore);

            _unitOfWork.Save();
        }
    }
}