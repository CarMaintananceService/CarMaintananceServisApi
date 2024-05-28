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

            configuration.CreateMap<Firm, FirmInput>().ReverseMap();
            configuration.CreateMap<Firm, FirmOutput>().ReverseMap();
            configuration.CreateMap<Firm, FirmOutputSimple>().ReverseMap();

            configuration.CreateMap<Manufacturer, ManufacturerInput>().ReverseMap();
            configuration.CreateMap<Manufacturer, ManufacturerOutput>().ReverseMap();
            configuration.CreateMap<Manufacturer, ManufacturerOutputSimple>().ReverseMap();

            configuration.CreateMap<OutSourceLabor, OutSourceLaborInput>().ReverseMap();
            configuration.CreateMap<OutSourceLabor, OutSourceLaborOutput>().ReverseMap();
            configuration.CreateMap<OutSourceLabor, OutSourceLaborOutputSimple>().ReverseMap();

            configuration.CreateMap<RepairActivity, RepairActivityInput>().ReverseMap();
            configuration.CreateMap<RepairActivity, RepairActivityOutput>().ReverseMap();

            configuration.CreateMap<ProductGroup, ProductGroupInput>().ReverseMap();
            configuration.CreateMap<ProductGroup, ProductGroupOutput>().ReverseMap();
            configuration.CreateMap<ProductGroup, ProductGroupOutputSimple>().ReverseMap();

            configuration.CreateMap<Setting, SettingInput>().ReverseMap();
            configuration.CreateMap<Setting, SettingOutput>().ReverseMap();

            configuration.CreateMap<StockCard, StockCardInput>().ReverseMap();
            configuration.CreateMap<StockCard, StockCardOutput>().ReverseMap();
            configuration.CreateMap<StockCard, StockCardOutputSimple>().ReverseMap();

            configuration.CreateMap<StockCardBrand, StockCardBrandInput>().ReverseMap();
            configuration.CreateMap<StockCardBrand, StockCardBrandOutput>().ReverseMap();
            configuration.CreateMap<StockCardBrand, StockCardBrandOutputSimple>().ReverseMap();

            configuration.CreateMap<StockCardUnit, StockCardUnitInput>().ReverseMap();
            configuration.CreateMap<StockCardUnit, StockCardUnitOutput>().ReverseMap();
            configuration.CreateMap<StockCardUnit, StockCardUnitOutputSimple>().ReverseMap();

            configuration.CreateMap<StockMovement, StockMovementInput>().ReverseMap();
            configuration.CreateMap<StockMovement, StockMovementOutput>().ReverseMap();

            configuration.CreateMap<Unit, UnitInput>().ReverseMap();
            configuration.CreateMap<Unit, UnitOutput>().ReverseMap();
            configuration.CreateMap<Unit, UnitOutputSimple>().ReverseMap();

            configuration.CreateMap<Vehicle, VehicleInput>().ReverseMap();
            configuration.CreateMap<Vehicle, VehicleOutput>().ReverseMap();
            configuration.CreateMap<Vehicle, VehicleOutputSimple>().ReverseMap();

            configuration.CreateMap<VehicleBrand, VehicleBrandInput>().ReverseMap();
            configuration.CreateMap<VehicleBrand, VehicleBrandOutput>().ReverseMap();
            configuration.CreateMap<VehicleBrand, VehicleBrandOutputSimple>().ReverseMap();

            configuration.CreateMap<VehicleType, VehicleTypeInput>().ReverseMap();
            configuration.CreateMap<VehicleType, VehicleTypeOutput>().ReverseMap();
            configuration.CreateMap<VehicleType, VehicleTypeOutputSimple>().ReverseMap();












        }

	}
}
