using AutoMapper;
using FrunutStock.Model.Models;
using FrunutStock.Web.ViewModels;
using FrunutStock.Web.Areas.Admin.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FrunutStock.Identity.Mappings
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public override string ProfileName
        {
            get { return "ViewModelToDomainMappings"; }
        }

        protected override void Configure()
        {
            Mapper.CreateMap<ProductFormViewModel, Product>()
                .ForMember(g => g.Name, map => map.MapFrom(vm => vm.ProductTitle))
                .ForMember(g => g.Description, map => map.MapFrom(vm => vm.ProductDescription))
                .ForMember(g => g.Price, map => map.MapFrom(vm => vm.ProductPrice))
                .ForMember(g => g.ProductCategoryID, map => map.MapFrom(vm => vm.ProductCategory));
            Mapper.CreateMap<EmployeeFormViewModel, Employee>()
                .ForMember(e => e.Id, map => map.MapFrom(vm => vm.EmployeeID))
                .ForMember(e => e.FirstName, map => map.MapFrom(vm => vm.EmployeeFirstName))
                .ForMember(e => e.LastName, map => map.MapFrom(vm => vm.EmployeeLastName));
        }
    }
}