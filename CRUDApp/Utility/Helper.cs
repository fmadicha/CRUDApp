using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRUDApp.Utility
{
    public  static class Helper
    {
        public static string Admin = "Admin";
        public static string Patient = "Patient";
        public static string Doctor = "Doctor";

        //dropdown
        public static List<SelectListItem> GetRoleForDropdown()
        {
            return new List<SelectListItem>
            {
                new SelectListItem{Value=Helper.Admin,Text=Helper.Admin},
                new SelectListItem{Value=Helper.Patient,Text=Helper.Patient},
                new SelectListItem{Value=Helper.Doctor,Text=Helper.Doctor}
            };
        }


    }
}
