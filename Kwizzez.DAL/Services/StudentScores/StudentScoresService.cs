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

        public StudentScoreDto GetStudentScoreById(string id, string includeProperties = "")
        {
            var studentScore = _unitOfWork.studentScoresRepository.GetById(id, includeProperties);
            return _mapper.Map<StudentScoreDto>(studentScore);
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
    }
}