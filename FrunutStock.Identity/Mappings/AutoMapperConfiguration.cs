using AutoMapper;
using FrunutStock.Identity.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrunutStock.Identity.Mappings
{
    public class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<DomainToViewModelMappingProfile>();
                x.AddProfile<ViewModelToDomainMappingProfile>();
            });
        }
    }
}