using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.DAL.Data;
using Kwizzez.DAL.Repositories;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public virtual IGenericRepository<Quiz> quizzesRepository { get; private set; }
        public virtual IGenericRepository<Question> questionsRepository { get; private set; }
        public virtual IGenericRepository<Answer> answersRepository { get; private set; }
        public virtual IGenericRepository<StudentScore> studentScoresRepository { get; private set; }
        public virtual IGenericRepository<StudentScoreAnswer> studentScoreAnswersRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            quizzesRepository = new GenericRepository<Quiz>(_context);
            questionsRepository = new GenericRepository<Question>(_context);
            answersRepository = new GenericRepository<Answer>(_context);
            studentScoresRepository = new GenericRepository<StudentScore>(_context);
            studentScoreAnswersRepository = new GenericRepository<StudentScoreAnswer>(_context);
        }

        public virtual int Save()
        {
            return _context.SaveChanges();
        }
    }
}