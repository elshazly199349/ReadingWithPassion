using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class Question
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Desciption { get; set; }
        public DateTime InsertionDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public int Score { get; set; }
        public int? DurationInSeconds { get; set; }
       
        [ForeignKey("Game")]
        public int GameId { get; set; }
        [ForeignKey("Admin")]
        public string AdminId { get; set; }

        [ForeignKey("AdminId")]
        [InverseProperty("Questions")]
        public virtual ApplicationUser Admin { get; set; }
        [ForeignKey("GameId")]
        [InverseProperty("Questions")]
        public virtual Game Game { get; set; }

        [InverseProperty("Question")]
        public virtual ICollection<Question_Point> Question_Points { get; set; }
    }
}
