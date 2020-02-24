using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class WorkShop_Game
    {
        public int Id { get; set; }
        public DateTime InsertionDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
     
        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        [ForeignKey("Game")]
        public int GameId { get; set; }
        [ForeignKey("WorkShop")]
        public int WorkShopId { get; set; }

        [ForeignKey("AdminId")]
        [InverseProperty("WorkShop_Games")]
        public virtual ApplicationUser Admin { get; set; }
        [ForeignKey("WorkShopId")]
        [InverseProperty("WorkShop_Games")]
        public virtual WorkShop WorkShop { get; set; }
        [ForeignKey("GameId")]
        [InverseProperty("WorkShop_Games")]
        public virtual Game Game { get; set; }
    }
}
