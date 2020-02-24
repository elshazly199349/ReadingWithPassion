using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingWithPassion.Web.Models.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IEmployeeRepository employeeRepository { get; }
        bool SaveChanges();
    }
}
