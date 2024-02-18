

using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Web.Data
{
    public class LeaveAllocation:BaseEntity
    {
        public int NumbeOfDays { get; set; }
        
        [ForeignKey("LeaveTypeID")] // Data annotation for FK is not mandatory
        public LeaveType LeaveType{ get; set; }
        public int LeaveTypeID{ get; set; }

        [ForeignKey("EmployeeID")] // Data annotation for FK is not mandatory
        public Employee Employee { get; set; }
        public string EmployeeID { get; set; }
        
    }
}
