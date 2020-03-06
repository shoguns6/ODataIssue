using AutoMapper;
using ODataModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace ODataExpandIssue
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
