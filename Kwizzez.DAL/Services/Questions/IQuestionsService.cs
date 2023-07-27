using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Questions;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.Questions
{
    public interface IQuestionsService
    {
        List<QuestionDto> GetAllQuestions(QueryFilter<Question> queryFilter);
        void AddQuestion(QuestionDto questionDto);
        void UpdateQuestion(QuestionDto questionDto);
        void DeleteQuestion(QuestionDto questionDto);
        void DeleteQuestions(IEnumerable<QuestionDto> questionsDtos);
    }
}
