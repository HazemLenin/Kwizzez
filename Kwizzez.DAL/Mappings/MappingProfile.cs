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

            CreateMap<Quiz, QuizDetailedDto>()
                .ForMember(dest => dest.TeacherId, src => src.MapFrom(src => src.ApplicationUserId))
                .ForMember(dest => dest.Teacher, src => src.MapFrom(src => src.ApplicationUser))
                .ReverseMap();

            CreateMap<Quiz, QuizDto>()
                .ForMember(dest => dest.TeacherId, src => src.MapFrom(src => src.ApplicationUserId))
                .ReverseMap();

            CreateMap<Quiz, QuizInfoDto>()
                .ForMember(dest => dest.TeacherId, src => src.MapFrom(src => src.ApplicationUserId))
                .ReverseMap();

            CreateMap<Quiz, AddQuizDto>()
                .ReverseMap();

            CreateMap<QuizDto, AddQuizDto>()
                .ReverseMap();

            CreateMap<Quiz, EditQuizDto>()
                .ReverseMap();

            CreateMap<Question, QuestionDto>()
                .ReverseMap();

            CreateMap<Question, AddQuestionDto>()
                .ReverseMap();

            CreateMap<Question, EditQuestionDto>()
                .ReverseMap();

            CreateMap<Answer, AnswerDto>()
                .ReverseMap();

            CreateMap<Answer, AddAnswerDto>()
                .ReverseMap();

            CreateMap<Answer, EditAnswerDto>()
                .ReverseMap();

            CreateMap<StudentScore, StudentScoreDto>()
                .ReverseMap();

            CreateMap<StudentScoreAnswer, StudentScoreAnswerDto>()
                .ReverseMap();
        }
    }
}