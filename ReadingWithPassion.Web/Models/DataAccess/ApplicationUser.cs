using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class ApplicationUser:IdentityUser
    {
        public bool Gender { get; set; }

        [InverseProperty("Admin")]
        public virtual ICollection<WorkShop> WorkShops { get; set; }
        [InverseProperty("Admin")]
        public virtual ICollection<Game> Games { get; set; }
        [InverseProperty("Admin")]
        public virtual ICollection<WorkShop_Game> WorkShop_Games { get; set; }
        [InverseProperty("Admin")]
        public virtual ICollection<Question> Questions { get; set; }
        [InverseProperty("Admin")]
        public virtual ICollection<Question_Point> Question_Points { get; set; }
        [InverseProperty("Admin")]
        public virtual ICollection<Question_Point_Choice> Question_Point_Choices { get; set; }
        [InverseProperty("Admin")]
        public virtual ICollection<WorkShop_Schedule> WorkShop_SChedules { get; set; }
        [InverseProperty("Admin")]
        public virtual ICollection<User_WorkShop_Schedule> ApprovedSchedules { get; set; }
        [InverseProperty("User")]
        public virtual ICollection<User_WorkShop_Schedule> User_WorkShop_Schedules { get; set; }
    }
}
