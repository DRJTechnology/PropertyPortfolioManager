using AutoMapper;
using Microsoft.Graph.Models;
using PropertyPortfolioManager.Models.Dto.Finance;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Enums;
using PropertyPortfolioManager.Models.Model.Document;
using PropertyPortfolioManager.Models.Model.Finance;
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

            this.CreateMap<EntityTypeDto, EntityTypeModel>();
            this.CreateMap<EntityTypeModel, EntityTypeDto>();

            this.CreateMap<ContactTypeDto, ContactTypeModel>();
            this.CreateMap<ContactTypeModel, ContactTypeDto>();

            this.CreateMap<ContactBasicDto, ContactResponseModel>();
            this.CreateMap<ContactDto, ContactResponseModel>();
            this.CreateMap<ContactBasicDto, ContactBasicResponseModel>();
            this.CreateMap<ContactEditModel, ContactDto>();

            this.CreateMap<TenancyDto, TenancyResponseModel>()
                .ForMember(d => d.DurationUnit, x => x.MapFrom(s => (DurationUnitEnum)s.DurationUnitId));
            this.CreateMap<TenancyEditModel, TenancyDto>()
                .ForMember(d => d.DurationUnitId, x => x.MapFrom(s => (int)s.DurationUnit));

            this.CreateMap<TenancyContactDto, TenancyContactModel>();
            this.CreateMap<TenancyContactModel, TenancyContactDto>();

            this.CreateMap<DriveItem, DriveItemModel>()
                .ForMember(d => d.IsFolder, x => x.MapFrom(s => s.Folder != null))
                .ForMember(d => d.FileMimeType, x => x.MapFrom(s => s.File != null ? s.File.MimeType : string.Empty))
                .ForMember(d => d.LastModifiedByName, x => x.MapFrom(s => s.LastModifiedBy.User.DisplayName));

            this.CreateMap<FileDto, FileModel>();
            this.CreateMap<FileModel, FileDto>();

            this.CreateMap<AccountDto, AccountResponseModel>()
                .ForMember(d => d.AccountName, x => x.MapFrom(s => s.Name));
            this.CreateMap<AccountEditModel, AccountDto>()
                .ForMember(d => d.Name, x => x.MapFrom(s => s.AccountName));

            this.CreateMap<AccountTypeDto, AccountTypeResponseModel>();

        }
    }
}
