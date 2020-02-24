using ReadingWithPassion.Web.Models.DataAccess;
using Microsoft.EntityFrameworkCore;
using ReadingWithPassion.Web.Models.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using ReadingWithPassion.Web.ViewModels.Employee;
using AutoMapper;

namespace ReadingWithPassion.Web.Models.Repository.Classes
{
    public class EmployeeRepository : Repository<Employee, EmployeeViewModel>,IEmployeeRepository
    {
        public EmployeeRepository(AppDbContext dbContext,IMapper mapper):base(dbContext,mapper) { }
        //private readonly AppDbContext appDbContext;
        //public EmployeeRepository(AppDbContext appDbContext)
        //{
        //    this.appDbContext = appDbContext;
        //}
        //public Employee Create(Employee employee)
        //{
        //    //employee.Id = _employeeList.Max(e => e.Id) + 1;
        //    appDbContext.Employees.Add(employee);
        //    appDbContext.SaveChanges();
        //    return employee;
        //}
        //public Employee Delete(int id)
        //{
        //   var employee= appDbContext.Employees.FirstOrDefault(e => e.Id == id);
        //    if (employee != null)
        //        appDbContext.Employees.Remove(employee);
        //    appDbContext.SaveChanges();
        //    return employee;
        //}
        //public IEnumerable<Employee> GetAll()
        //{
        //    return appDbContext.Employees;
        //}
        //public Employee GetById(int id)
        //{
        //    return appDbContext.Employees.Find(id);
        //}
        //public Employee Update(Employee employee)
        //{
        //    var employeeFromDb = appDbContext.Employees.Attach(employee);
        //    employeeFromDb.State = EntityState.Modified;
        //    appDbContext.SaveChanges();
        //    return employee;
        //}
    }
}
