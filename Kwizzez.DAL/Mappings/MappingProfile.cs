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

            CreateMap<QuizDetailedDto, AddQuizDto>()
                .ReverseMap();

            CreateMap<AddQuizDto, QuizDetailedDto>()
                .ReverseMap();

            CreateMap<AddQuizDto, QuizDto>()
                .ReverseMap();

            CreateMap<QuizDetailedDto, EditQuizDto>()
                .ReverseMap();

            CreateMap<EditQuizDto, QuizDetailedDto>()
                .ReverseMap();

            CreateMap<EditQuizDto, QuizDto>()
                .ReverseMap();

            CreateMap<Question, QuestionDto>()
                .ReverseMap();

            CreateMap<QuestionDto, QuestionFormDto>()
                .ReverseMap();

            CreateMap<Answer, AnswerDto>()
                .ReverseMap();

            CreateMap<AnswerDto, AnswerFormDto>()
                .ReverseMap();

            CreateMap<StudentScore, StudentScoreDto>()
                .ReverseMap();

            CreateMap<StudentScoreAnswer, StudentScoreAnswerDto>()
                .ReverseMap();
        }
    }
}