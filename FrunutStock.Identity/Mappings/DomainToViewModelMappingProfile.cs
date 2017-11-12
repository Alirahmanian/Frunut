using AutoMapper;
using FrunutStock.Model.Models;
using FrunutStock.Identity.ViewModels;
using FrunutStock.Identity.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrunutStock.Identity.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "DomainToViewModelMappings"; }
        }
       
        protected override void Configur()
        {
            Mapper.<ProductCategory, ProductCategoryViewModel>();
            Mapper.CreateMap<Product, ProductViewModel>();
            Mapper.CreateMap<Employee, EmployeeViewModel>();
        }
    }
}