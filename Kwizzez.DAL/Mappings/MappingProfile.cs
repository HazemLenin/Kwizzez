using AutoMapper;
using Kwizzez.DAL.Dtos.Answers;
using Kwizzez.DAL.Dtos.Questions;
using Kwizzez.DAL.Dtos.Quizzes;
using Kwizzez.DAL.Dtos.StudentScoreAnswers;
using Kwizzez.DAL.Dtos.StudentScores;
using Kwizzez.DAL.Dtos.Users;
using Kwizzez.Domain.Entities;

namespace Kwizzez.DAL.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ApplicationUser, UserDto>()
            .ReverseMap();

            CreateMap<Quiz, QuizDto>()
            .ReverseMap();

            CreateMap<Quiz, QuizFormDto>()
            .ReverseMap();

            CreateMap<Question, QuestionDto>()
            .ReverseMap();

            CreateMap<Answer, AnswerDto>()
            .ReverseMap();

            CreateMap<StudentScore, StudentScoreDto>()
            .ReverseMap();

            CreateMap<StudentScoreAnswer, StudentScoreAnswerDto>()
            .ReverseMap();
        }
    }
}