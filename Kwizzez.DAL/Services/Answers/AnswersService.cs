using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Dtos.Answers;
using Kwizzez.DAL.Repositories;
using Kwizzez.DAL.UnitOfWork;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.Answers
{
    public class AnswersService : IAnswersService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AnswersService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddAnswer(AnswerDto answerDto)
        {
            var answer = _mapper.Map<Answer>(answerDto);

            _unitOfWork.answersRepository.Add(answer);
            _unitOfWork.Save();
        }

        public void DeleteAnswer(AnswerDto answerDto)
        {
            var answer = _mapper.Map<Answer>(answerDto);

            _unitOfWork.answersRepository.Delete(answer);
            _unitOfWork.Save();
        }

        public void DeleteAnswers(IEnumerable<AnswerDto> answerzesDtos)
        {
            var answerzes = _mapper.Map<List<Answer>>(answerzesDtos);

            _unitOfWork.answersRepository.DeleteRange(answerzes);
            _unitOfWork.Save();
        }

        public List<AnswerDto> GetAllAnswers(QueryFilter<Answer> queryFilter)
        {
            var answerzes = _unitOfWork.answersRepository.GetAll(queryFilter);

            return _mapper.Map<List<AnswerDto>>(answerzes);
        }

        public void UpdateAnswer(AnswerDto answerDto)
        {
            var answer = _mapper.Map<Answer>(answerDto);

            _unitOfWork.answersRepository.Update(answer);
            _unitOfWork.Save();
        }
    }
}
