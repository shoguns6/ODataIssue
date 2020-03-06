using AutoMapper;
using AutoMapper.Extensions.ExpressionMapping;
using ODataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ODataIssueApi
{
    public class ParentRespository
    {
        private IMapper autoMapper;
        private TestModelContext testModelContext;
        public ParentRespository(TestModelContext context, IMapper mapper)
        {
            this.autoMapper = mapper;
            this.testModelContext = context;
        }

        public IQueryable<ParentDto> GetAllParents()
        {

            /// If not null then get from database and set here 
            var entities = testModelContext.Parent.UseAsDataSource(autoMapper).For<ParentDto>();
            //var entitiesList = mapper.Map<T[], List<DTOType>>(entities.ToArray());
            //var entitiesList = entities.ProjectTo<DTOType>();
            return entities.AsQueryable();
        }
    }
}
