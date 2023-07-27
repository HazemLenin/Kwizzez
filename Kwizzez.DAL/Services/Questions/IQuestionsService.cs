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
        void AddQuestion(QuestionDto quizDto);
        void UpdateQuestion(QuestionDto quizDto);
        void DeleteQuestion(QuestionDto quizDto);
        void DeleteQuestions(IEnumerable<QuestionDto> QuestionsDtos);
    }
}
