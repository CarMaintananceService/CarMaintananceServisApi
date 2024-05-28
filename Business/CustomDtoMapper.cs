using AutoMapper;
using Business.Shared.Security.Users.Dtos;
using Cars.Models.Domain;
using Core.Security;

namespace Business
{
    public static class CustomDtoMapper
    {
		public static void CreateMappings(IMapperConfigurationExpression configuration)
        {
			configuration.CreateMap<User, UserDto>().ReverseMap();
			configuration.CreateMap<User, UserOutput>().ReverseMap();
			configuration.CreateMap<User, UserOutputSimple>().ReverseMap();
			configuration.CreateMap<User, UserInput>().ReverseMap();

            configuration.CreateMap<CaseType, CaseTypeInput>().ReverseMap();
            configuration.CreateMap<CaseType, CaseTypeOutput>().ReverseMap();
            

        }

	}
}
