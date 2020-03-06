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
            CreateMap<ParentModel, ParentDto>();
            CreateMap<ParentDto, ParentModel>();
        }
    }
}
