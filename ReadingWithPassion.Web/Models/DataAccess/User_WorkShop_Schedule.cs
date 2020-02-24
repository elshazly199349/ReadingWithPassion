using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class User_WorkShop_Schedule
    {
        public int Id { get; set; }
        public DateTime InsertionDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public bool IsApproved { get; set; }
        public bool IsPaid { get; set; }
        public bool IsCompleted { get; set; }

        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        [ForeignKey("WorkShop_SChedule")]
        public int WorkShop_Schedule_Id { get; set; }

        [ForeignKey("UserId")]
        [InverseProperty("User_WorkShop_Schedules")]
        public virtual ApplicationUser User { get; set; }
        [ForeignKey("AdminId")]
        [InverseProperty("ApprovedSchedules")]
        public virtual ApplicationUser Admin { get; set; }
        [ForeignKey("WorkShop_Schedule_Id")]
        [InverseProperty("User_WorkShop_Schedules")]
        public virtual WorkShop_Schedule WorkShop_Schedule { get; set; }
        
        [InverseProperty("User_WorkShop_Schedule")]
        public ICollection<User_Game> User_Games { get; set; }
    }
}
