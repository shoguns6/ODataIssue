using AutoMapper;
using ODataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODataIssueApi
{
    public class ParentMapperProfile : Profile
    {
        public ParentMapperProfile()
        {
            CreateMap<ParentModel, ParentDto>()
                //.ForMember(dto => dto.Childrens, conf => { conf.AllowNull(); conf.ExplicitExpansion(); })
                ;
            CreateMap<ParentDto, ParentModel>()
                //.ForMember(model => model.Childrens, conf => { conf.AllowNull(); conf.ExplicitExpansion(); })
                ;

            CreateMap<ChildModel, ChildDto>();
            CreateMap<ChildDto, ChildModel>();
        }
    }
}
