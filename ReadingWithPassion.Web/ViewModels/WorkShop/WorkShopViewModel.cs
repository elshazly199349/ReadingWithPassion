using System;
namespace ReadingWithPassion.Web.ViewModels.WorkShop
{
    public class WorkShopViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? InsetionDateTime { get; set; }
        public DateTime? ModificationDateTime { get; set; }
        public int? Duration { get; set; }
        public string AdminId { get; set; }
    }
}
