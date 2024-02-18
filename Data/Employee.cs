//using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Identity;

namespace LMS.Web.Data
{
    public class Employee : IdentityUser
    {
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? TaxID { get; set; }
        public DateTime DateofBirth { get; set; }
        public DateTime DateJoined { get; set; }
    }
}
