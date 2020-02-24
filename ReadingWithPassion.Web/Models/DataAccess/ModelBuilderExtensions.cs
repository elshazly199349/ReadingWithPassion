using Microsoft.EntityFrameworkCore;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(new Employee
    {
        Id = 1,
        Department = Helper.Dept.IT,
        Email = "omar.elsaid@arpuplus.com",
        Name = "Omar Elsaid"
    }, new Employee
    {
        Id = 2,
        Department = Helper.Dept.IT,
        Email = "john.elsaid@arpuplus.com",
        Name = "john john"
    });
        }
    }
}
