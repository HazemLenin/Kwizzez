using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Answers;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.Answers
{
    public interface IAnswersService
    {
        List<AnswerDto> GetAllAnswers(QueryFilter<Answer> queryFilter);
        void AddAnswer(AnswerDto answerDto);
        void UpdateAnswer(AnswerDto answerDto);
        void DeleteAnswer(AnswerDto answerDto);
        void DeleteAnswers(IEnumerable<AnswerDto> AnswersDtos);
    }
}
