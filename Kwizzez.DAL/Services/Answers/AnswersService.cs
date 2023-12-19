using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Questions;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.UnitOfWork;
using Kwizzez.DAL.Utilities;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.Answers
{
    public class AnswersService : IAnswersService
    {
        private readonly IUnitOfWork _unitOfWork;
        public AnswersService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool AnswerExists(string id)
        {
            return _unitOfWork.answersRepository.GetAll(new()
            {
                Filter = a => a.Id == id
            }).Any();
        }
    }
}
