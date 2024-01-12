﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Dtos.Questions;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Utilities;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Services.Quizzes
{
    public interface IQuizzesService
    {
        PaginatedList<QuizDto> GetPaginatedQuizzes(int pageIndex, int pageSize);
        PaginatedList<QuizDto> GetPaginatedUserQuizzes(string userId, int pageIndex, int pageSize);
        QuizDto? GetQuizById(string quizId, string studentId);
        QuizDetailedDto? GetDetailedQuizById(string code);
        List<QuestionForStudentDto>? GetQuizQuestionsById(string id, string studentId);
        string AddQuiz(AddQuizDto QuizDetailedDto, string teacherId);
        void UpdateQuiz(EditQuizDto QuizDetailedDto);
        void DeleteQuiz(string id);
        void DeleteQuizzes(IEnumerable<string> ids);
        bool QuizExists(string id);
        void StartQuiz(string quizId, string studentId);
        bool AnswerRelatedToQuiz(string quizId, string answerId);
        bool VerifyQuizAccess(string quizId, int code);
    }
}
