using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciprion { get; set; }
        public int Score { get; set; }
        public int? DurationInSeconds { get; set; }
        public DateTime InsertionDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        
        [ForeignKey("Admin")]
        public string AdminId { get; set; }

        [ForeignKey("AdminId")]
        [InverseProperty("Games")]
        public virtual ApplicationUser Admin { get; set; }

        [InverseProperty("Game")]
        public virtual ICollection<WorkShop_Game> WorkShop_Games { get; set; }
        [InverseProperty("Game")]
        public virtual ICollection<User_Game> User_Games { get; set; }
        [InverseProperty("Game")]
        public virtual ICollection<Question> Questions { get; set; }
    }
}
