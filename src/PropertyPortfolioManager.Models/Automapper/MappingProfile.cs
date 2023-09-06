using AutoMapper;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.General;
using PropertyPortfolioManager.Models.Model.Property;

namespace PropertyPortfolioManager.Models.Automapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            this.CreateMap<UnitDto, UnitResponseModel>();
            this.CreateMap<UnitBasicDto, UnitBasicResponseModel>();
            this.CreateMap<UnitEditModel, UnitDto>();

            this.CreateMap<AddressDto, AddressResponseModel>();
            this.CreateMap<AddressEditModel, AddressDto>();

            this.CreateMap<UnitTypeDto, UnitTypeModel>();
        }
    }
}
