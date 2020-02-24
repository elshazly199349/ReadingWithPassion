using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class WorkShop
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? InsetionDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public int? Duration { get; set; }

        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        
        [ForeignKey("AdminId")]
        [InverseProperty("WorkShops")]
        public virtual ApplicationUser Admin { get; set; }

        [InverseProperty("WorkShop")]
        public virtual ICollection<WorkShop_Schedule> WorkShop_SChedules { get; set; }
        [InverseProperty("WorkShop")]
        public virtual ICollection<WorkShop_Game> WorkShop_Games { get; set; }
    }
}
