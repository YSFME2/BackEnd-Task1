using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Extentions
{
    public static class ConvertExtenstions
    {
        public static DateOnly ToDate(this DateTime dateTime)
        {
            return DateOnly.FromDateTime(dateTime);
        }
        public static DateOnly ToDate(this DateTime? dateTime)
        {
            return DateOnly.FromDateTime(dateTime ?? DateTime.Now);
        }
    }
}
