using System.ComponentModel.DataAnnotations;

namespace LMS.Web.Data
{
    public class LeaveTypeVM
    {
        public int ID { get; set; }
        [Display(Name = "Leave Type Name")]
        [Required]
        public string Name { get; set; }

        [Required]
        [Range(1,25,ErrorMessage="Days should be between 1 & 25")]
        [Display(Name = "Default No of days")]
        public int DefaultDays { get; set; }
    }
}
