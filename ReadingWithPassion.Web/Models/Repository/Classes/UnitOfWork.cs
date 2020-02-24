using AutoMapper;
using ReadingWithPassion.Web.Models.DataAccess;
using ReadingWithPassion.Web.Models.Repository.Interfaces;

namespace ReadingWithPassion.Web.Models.Repository.Classes
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly AppDbContext _appContext;
        private readonly IMapper _mapper;
        public UnitOfWork(AppDbContext appContext,IMapper mapper) {
            _appContext = appContext;
            _mapper = mapper;
        }
        public IEmployeeRepository employeeRepository => new EmployeeRepository(_appContext, _mapper);

        public bool SaveChanges()
        {
            return _appContext.SaveChanges() > 0;
        }
    }
}
