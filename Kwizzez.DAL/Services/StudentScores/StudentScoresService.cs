using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Dtos.StudentScores;
using Kwizzez.DAL.UnitOfWork;
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

        public void DeleteStudentScore(StudentScoreDto studentScoreDto)
        {
            var studentScore = _mapper.Map<StudentScore>(studentScoreDto);
            _unitOfWork.studentScoresRepository.Delete(studentScore);
        }

        public void DeleteStudentScores(IEnumerable<StudentScoreDto> studentScoreDtos)
        {
            var studentScores = _mapper.Map<List<StudentScore>>(studentScoreDtos);
            _unitOfWork.studentScoresRepository.DeleteRange(studentScores);
        }

        public List<StudentScoreDto> GetAllStudentScores(QueryFilter<StudentScore> queryFilter)
        {
            var studentScores = _unitOfWork.studentScoresRepository.GetAll(queryFilter);
            return _mapper.Map<List<StudentScoreDto>>(studentScores);
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
    }
}