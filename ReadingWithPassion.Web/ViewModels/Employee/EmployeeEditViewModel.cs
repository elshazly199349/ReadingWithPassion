using System.ComponentModel.DataAnnotations;

namespace ReadingWithPassion.Web.ViewModels.Employee
{
    public class EmployeeEditViewModel:EmployeeCreateViewModel
    {
        [Required]
        public int Id { get; set; }
        public string ExistingPhotoPath { get; set; }
    }
}
