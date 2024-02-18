
using AutoMapper;
using LMS.Web.Data;

namespace LMS.Web.Config
{
    public class MapperConfig: Profile
    {
        public MapperConfig()
        {
            CreateMap<LeaveType, LeaveTypeVM>().ReverseMap();
        }
    }
}
