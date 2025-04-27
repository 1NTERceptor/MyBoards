using AutoMapper;
using StackOverflow.Entities;
using StackOverflow.DTO;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Question, QuestionModel>();
        CreateMap<QuestionModel, Question>();

        CreateMap<UserModel, User>();
        CreateMap<User, UserModel>();

        CreateMap<AnswerModel, Answer>();
        CreateMap<Answer, AnswerModel>();
    }
}
