using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class WorkShop_Schedule
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; }
        public string Location { get; set; }
        public int DurationInMinutes { get; set; }
        public int AvailableSpaces { get; set; }
        public DateTime InsertionDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }

        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        [ForeignKey("WorkShop")]
        public int WorkShopId { get; set; }

        [ForeignKey("AdminId")]
        [InverseProperty("WorkShop_SChedules")]
        public virtual ApplicationUser Admin { get; set; }
        [ForeignKey("WorkShopId")]
        [InverseProperty("WorkShop_SChedules")]
        public virtual WorkShop WorkShop { get; set; }
        
        [InverseProperty("WorkShop_Schedule")]
        public virtual ICollection<User_WorkShop_Schedule> User_WorkShop_Schedules { get; set; }
    }
}
