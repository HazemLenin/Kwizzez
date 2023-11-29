using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kwizzez.Domain.Common;
using Kwizzez.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Kwizzez.DAL.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>().HasQueryFilter(u => u.IsActive);
            builder.Entity<Quiz>().HasQueryFilter(q => !q.IsDeleted);
            builder.Entity<Question>().HasQueryFilter(q => !q.IsDeleted);
            builder.Entity<Answer>().HasQueryFilter(a => !a.IsDeleted);
            builder.Entity<StudentScore>().HasQueryFilter(s => !s.IsDeleted);
            builder.Entity<StudentScoreAnswer>().HasQueryFilter(s => !s.IsDeleted);

            builder.Entity<Quiz>()
                .HasMany(q => q.StudentScores)
                .WithOne(s => s.Quiz)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<StudentScore>()
                .HasMany(q => q.StudentScoreAnswers)
                .WithOne(s => s.StudentScore)
                .OnDelete(DeleteBehavior.NoAction);
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
                else if (entry.Entity is ApplicationUser)
                {
                    entry.State = EntityState.Modified;
                    entry.CurrentValues["IsActive"] = false;
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
