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

            this.CreateMap<AddressDto, AddressModel>();
            this.CreateMap<AddressModel, AddressDto>();

            this.CreateMap<PortfolioDto, PortfolioModel>();
            this.CreateMap<PortfolioModel, PortfolioDto>();

            this.CreateMap<UnitTypeDto, UnitTypeModel>();
            this.CreateMap<UnitTypeModel, UnitTypeDto>();

            this.CreateMap<ContactTypeDto, ContactTypeModel>();
            this.CreateMap<ContactTypeModel, ContactTypeDto>();

            this.CreateMap<ContactDto, ContactResponseModel>();
            this.CreateMap<ContactBasicDto, ContactBasicResponseModel>();
            this.CreateMap<ContactEditModel, ContactDto>();
        }
    }
}
