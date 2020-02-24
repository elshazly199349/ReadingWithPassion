using AutoMapper;
using ReadingWithPassion.Web.Models.DataAccess;
using ReadingWithPassion.Web.ViewModels.Employee;
using ReadingWithPassion.Web.ViewModels.WorkShop;

namespace ReadingWithPassion.Web.AutoMapper
{
    public class MapperProfiler:Profile
    {
        public MapperProfiler() {
            CreateMap<WorkShop, WorkShopViewModel>().ReverseMap();
            CreateMap<Employee, EmployeeViewModel>().ReverseMap();
        }
    }
}
