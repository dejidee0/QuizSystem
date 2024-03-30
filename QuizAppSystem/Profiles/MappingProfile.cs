using AutoMapper;
using QuizAppSystem.DTO;
using QuizAppSystem.DTOs;
using QuizAppSystem.Models;
using QuizAppSystem.Models.Enums;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WalletDTO, Wallet>()
            .ForMember(dest => dest.ParticipantId, opt => opt.MapFrom(src => Guid.Parse(src.ParticipantId)));

        CreateMap<ExamCreationDTO, Exam>()
            .ForMember(dest => dest.ExamCode, opt => opt.Ignore()); // Ignore ExamCode during mapping

        CreateMap<ExamCreationDTO, ExamCreationModel>();

        CreateMap<Exam, ExamDTO>();
        CreateMap<ExamDTO, Exam>();
        CreateMap<ExamReport, ExamReportDto>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();

        CreateMap<Participant, ParticipantDTO>();
        CreateMap<Question, QuestionDTO>();
        CreateMap<User, UserDto>();
        CreateMap<Answer, AnswerDTO>(); 
        CreateMap<AnswerDTO, Answer>();
    }
}

