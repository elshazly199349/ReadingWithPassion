using ReadingWithPassion.Web.Models.DataAccess;
using ReadingWithPassion.Web.ViewModels.Employee;
using System.Collections.Generic;

namespace ReadingWithPassion.Web.Models.Repository.Interfaces
{
    public interface IEmployeeRepository:IRepository<Employee,EmployeeViewModel>
    {
    }
}
