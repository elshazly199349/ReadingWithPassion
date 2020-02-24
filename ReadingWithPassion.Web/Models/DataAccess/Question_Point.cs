using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class Question_Point
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public string Description { get; set; }
        public int Score { get; set; }
        public int DurtationInSeconds { get; set; }
        public DateTime InsertionDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        
        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        [ForeignKey("Question")]
        public int QuestionId { get; set; }

        [ForeignKey("AdminId")]
        [InverseProperty("Question_Points")]
        public virtual ApplicationUser Admin { get; set; }
        [ForeignKey("QuestionId")]
        [InverseProperty("Question_Points")]
        public virtual Question Question { get; set; }

        [InverseProperty("Question_Point")]
        public virtual ICollection<Question_Point_Choice> Question_Point_Choices { get; set; }
    }
}
