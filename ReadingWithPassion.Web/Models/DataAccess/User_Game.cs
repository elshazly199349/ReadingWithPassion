using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class User_Game
    {
        public int Id { get; set; }
        public DateTime InsertionDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public int Score { get; set; }
        public int durationInSeconds { get; set; }

        [ForeignKey("Game")]
        public int GameId { get; set; }
        [ForeignKey("User_WorkShop_Schedule")]
        public int User_WorkShop_Schedule_Id { get; set; }

        [ForeignKey("GameId")]
        [InverseProperty("User_Games")]
        public virtual Game Game { get; set; }
        [ForeignKey("User_WorkShop_Schedule_Id")]
        [InverseProperty("User_Games")]
        public virtual User_WorkShop_Schedule User_WorkShop_Schedule { get; set; }
    }
}
