using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class AppDbContext:IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkShop> WorkShops { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Question_Point> Question_Points { get; set; }
        public DbSet<Question_Point_Choice> Question_Point_Choices { get; set; }
        public DbSet<WorkShop_Schedule> WorkShop_Schedules { get; set; }
        public DbSet<WorkShop_Game> WorkShop_Games { get; set; }
        public DbSet<User_Game> User_Games { get; set; }
        public DbSet<User_WorkShop_Schedule> User_WorkShop_Schedules { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder==null)
            {
                throw new System.Exception();
            }
            base.OnModelCreating(modelBuilder);
            modelBuilder.Seed();

            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
        }
    }
}
