using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Repositories;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<Quiz> quizzesRepository { get; }
        IGenericRepository<Question> questionsRepository { get; }
        IGenericRepository<Answer> answersRepository { get; }
        IGenericRepository<StudentScore> studentScoresRepository { get; }
        IGenericRepository<StudentScoreAnswer> studentScoreAnswersRepository { get; }
        int Save();
    }
}