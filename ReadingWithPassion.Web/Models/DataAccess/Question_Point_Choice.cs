using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReadingWithPassion.Web.Models.DataAccess
{
    public class Question_Point_Choice
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public bool IsCorrectAnswer { get; set; }
        public DateTime InsertionDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        
        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        [ForeignKey("Question_Point")]
        public int Question_Point_Id { get; set; }

        [ForeignKey("AdminId")]
        [InverseProperty("Question_Point_Choices")]
        public virtual ApplicationUser Admin { get; set; }
        [ForeignKey("Question_Point_Id")]
        [InverseProperty("Question_Point_Choices")]
        public virtual Question_Point Question_Point { get; set; }
    }
}
