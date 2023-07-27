using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Dtos.Questions;
using Kwizzez.DAL.Repositories;
using Kwizzez.DAL.UnitOfWork;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.Questions
{
    public class QuestionsService : IQuestionsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void AddQuestion(QuestionDto questionDto)
        {
            var question = _mapper.Map<Question>(questionDto);

            _unitOfWork.questionsRepository.Add(question);
            _unitOfWork.Save();
        }

        public void DeleteQuestion(QuestionDto questionDto)
        {
            var question = _mapper.Map<Question>(questionDto);

            _unitOfWork.questionsRepository.Delete(question);
            _unitOfWork.Save();
        }

        public void DeleteQuestions(IEnumerable<QuestionDto> questionzesDtos)
        {
            var questionzes = _mapper.Map<List<Question>>(questionzesDtos);

            _unitOfWork.questionsRepository.DeleteRange(questionzes);
            _unitOfWork.Save();
        }

        public List<QuestionDto> GetAllQuestions(QueryFilter<Question> queryFilter)
        {
            var questionzes = _unitOfWork.questionsRepository.GetAll(queryFilter);

            return _mapper.Map<List<QuestionDto>>(questionzes);
        }

        public void UpdateQuestion(QuestionDto questionDto)
        {
            var question = _mapper.Map<Question>(questionDto);

            _unitOfWork.questionsRepository.Update(question);
            _unitOfWork.Save();
        }
    }
}
