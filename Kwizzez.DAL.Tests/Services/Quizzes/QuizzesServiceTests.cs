using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Kwizzez.DAL.Data;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Repositories;
using Kwizzez.DAL.Services.Quizzes;
using Kwizzez.DAL.UnitOfWork;
using Kwizzez.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;

namespace Kwizzez.DAL.Tests.Services.Quizzes
{
    public class QuizzesServiceTests
    {
        private readonly Mock<DbContextOptions> _dbContextOptionsMock = new();
        private readonly Mock<ApplicationDbContext> _contextMock = new(new DbContextOptions<ApplicationDbContext>());
        private readonly Mock<UnitOfWork.UnitOfWork> _unitOfWorkMock;
        private readonly QuizzesService _quizzesService;

        private readonly List<Quiz> _quizzes = new()
        {
            new() { Title = "quiz 1"},
            new() { Title = "quiz 2"},
            new() { Title = "quiz 3"},
        };

        public QuizzesServiceTests()
        {
            _unitOfWorkMock = new(_contextMock.Object);

            _unitOfWorkMock
                .Setup(m => m.quizzesRepository.GetAll(It.IsAny<QueryFilter<Quiz>>()))
                .Returns(_quizzes.AsQueryable());
            _unitOfWorkMock
                .Setup(m => m.quizzesRepository.GetById(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(_quizzes.First());
            _unitOfWorkMock
                .Setup(m => m.quizzesRepository.Add(It.IsAny<Quiz>()));
            _unitOfWorkMock
                .Setup(m => m.quizzesRepository.Update(It.IsAny<Quiz>()));
            _unitOfWorkMock
                .Setup(m => m.quizzesRepository.Delete(It.IsAny<Quiz>()));
            _unitOfWorkMock
                .Setup(m => m.quizzesRepository.DeleteRange(It.IsAny<IEnumerable<Quiz>>()));

            MapperConfiguration mapperConfiguration = new(config =>
            {
                config.CreateMap<Quiz, QuizDetailedDto>().ReverseMap();
            });
            _quizzesService = new(_unitOfWorkMock.Object, new Mapper(mapperConfiguration));
        }

        [Fact]
        public void QuizzesService_AddQuiz_ShouldCallRepositoryAdd()
        {
            // Arrange
            QuizDetailedDto QuizDetailedDto = new()
            {
                Title = "Quiz 4"
            };

            // Act
            _quizzesService.AddQuiz(QuizDetailedDto);

            // Assert
            _unitOfWorkMock
                .Verify(m => m.quizzesRepository.Add(It.IsAny<Quiz>()), Times.Once);
        }

        [Fact]
        public void QuizzesService_GetQuizByCode_ShouldFilterByCode()
        {
            // Arrange
            // Act
            _quizzesService.GetQuizByCode(new Random().Next(4000000));

            // Assert
            _unitOfWorkMock
                .Verify(m => m.quizzesRepository.GetAll(It.IsAny<QueryFilter<Quiz>>()), Times.Once);
        }
    }
}