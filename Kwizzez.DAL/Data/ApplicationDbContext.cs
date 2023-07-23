using Kwizzez.Domain.Common;
using Kwizzez.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kwizzez.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Quiz>().HasQueryFilter(q => !q.IsDeleted);
            builder.Entity<Question>().HasQueryFilter(q => !q.IsDeleted);
            builder.Entity<Answer>().HasQueryFilter(a => !a.IsDeleted);
            builder.Entity<StudentScore>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<StudentScoreAnswer>().HasQueryFilter(s => !s.IsDeleted);
        }

        public override int SaveChanges()
        {
            var entries = ChangeTracker.Entries().Where(e => e.State == EntityState.Deleted);

            foreach (var entry in entries)
            {
                if (entry.Entity is Base)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsDeleted"] = true;
                }
            }
            return base.SaveChanges();
        }

        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Answer> Answers { get; set; }
        public DbSet<StudentScore> StudentScores { get; set; }
        public DbSet<StudentScoreAnswer> StudentScoreAnswers { get; set; }
    }
}
