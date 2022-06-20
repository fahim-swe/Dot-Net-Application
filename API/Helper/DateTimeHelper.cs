using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helper
{
    public static class DateTimeHelper
    {   
        
        public static int CalculateAge(DateTime someone)
        {

            var today = DateTime.Today;
            var age = today.Year-someone.Year;
            if(someone.Date > today.AddYears(-age)) age--;
            return age;
        }
    }
}