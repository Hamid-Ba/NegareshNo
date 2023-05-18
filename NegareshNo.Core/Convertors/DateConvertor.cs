using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NegareshNo.Core.Convertors
{
    public static class DateConvertor
    {
        public static string ToShamsi(this DateTime time)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.GetYear(time).ToString() + "/" + pc.GetMonth(time).ToString("00") + "/" + pc.GetDayOfMonth(time).ToString("00");
        }
    }
}
