using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ODataModel;

namespace ODataIssueApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ParentDtoController : ODataController
    {

        TestModelContext testModelContext;

        public ParentDtoController(TestModelContext context)
        {
            testModelContext = context;
        }

        [EnableQuery(AllowedQueryOptions =Microsoft.AspNet.OData.Query.AllowedQueryOptions.All)]
        [HttpGet]
        public IQueryable<ParentDto> Get()
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ParentMapperProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            var repo = new ParentRespository(testModelContext, mapper);
            var data = repo.GetAllParents();

            return data;
        }
    }
}
