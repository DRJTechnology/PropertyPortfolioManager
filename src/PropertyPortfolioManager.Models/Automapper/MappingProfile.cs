﻿using AutoMapper;
using Microsoft.Graph.Models;
using PropertyPortfolioManager.Models.Dto.General;
using PropertyPortfolioManager.Models.Dto.Property;
using PropertyPortfolioManager.Models.Model.Document;
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

            this.CreateMap<DriveItem, DriveItemModel>()
                .ForMember(d => d.IsFolder, x => x.MapFrom(s => s.Folder != null))
                .ForMember(d => d.FileMimeType, x => x.MapFrom(s => s.File != null ? s.File.MimeType : string.Empty))
                .ForMember(d => d.LastModifiedByName, x => x.MapFrom(s => s.LastModifiedBy.User.DisplayName));

            this.CreateMap<FileDto, FileModel>();
            this.CreateMap<FileModel, FileDto>();

        }
    }
}
