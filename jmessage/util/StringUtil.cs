using System;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace jmessage.util
{
    public class StringUtil
    {
        public static bool IsNumber(string strNumber)
        {
            Regex objNotNumberPattern = new Regex("[^0-9.-]");
            Regex objTwoDotPattern = new Regex("[0-9]*[.][0-9]*[.][0-9]*");
            Regex objTwoMinusPattern = new Regex("[0-9]*[-][0-9]*[-][0-9]*");

            string strValidRealPattern = "^([-]|[.]|[-.]|[0-9])[0-9]*[.]*[0-9]+$";
            string strValidIntegerPattern = "^([-]|[0-9])[0-9]*$";
            Regex objNumberPattern = new Regex("(" + strValidRealPattern + ")|(" + strValidIntegerPattern + ")");

            return !objNotNumberPattern.IsMatch(strNumber) &&
                   !objTwoDotPattern.IsMatch(strNumber) &&
                   !objTwoMinusPattern.IsMatch(strNumber) &&
                   objNumberPattern.IsMatch(strNumber);
        }

        public static bool IsNumeric(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*[.]?\d*$");
        }

        public static bool IsInt(string value)
        {
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }

        public static bool IsUnsign(string value)
        {
            return Regex.IsMatch(value, @"^\d*[.]?\d*$");
        }

        public static string arrayToString(string[] values)
        {
            if (null == values) return "";

            StringBuilder buffer = new StringBuilder(values.Length);
            for (int i = 0; i < values.Length; i++)
            {
                buffer.Append(values[i]).Append(",");
            }

            if (buffer.Length > 0)
            {
                return buffer.ToString().Substring(0, buffer.Length - 1);
            }
            return "";
        }

        public static bool IsDateTime(string datetime)
        {
            bool isdatetime;
            DateTime dateTime;
            try
            {
                dateTime = DateTime.ParseExact(datetime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                isdatetime = true;
            }
            catch
            {
                isdatetime = false;
            }
            return isdatetime;
        }

        public static bool IsTime(string time)
        {
            bool istime;
            try
            {
                DateTime righttime;
                righttime = DateTime.ParseExact(time, "HH:mm:ss", CultureInfo.InvariantCulture);
                istime = true;
            }
            catch
            {
                istime = false;
            }
            return istime;
        }

        public static bool IsMobile(string mobile)
        {
            return Regex.IsMatch(mobile, @"^(1[34578][0-9])(\\d{4})(\\d{4})$");
        }

        public static bool IsTimeunit(string time_unit)
        {
            bool istime_unit;
            if (string.Equals(time_unit, "day", StringComparison.CurrentCultureIgnoreCase))
            {
                istime_unit = true;
            }
            else if (string.Equals(time_unit, "week", StringComparison.CurrentCultureIgnoreCase))
            {
                istime_unit = true;
            }
            else if (string.Equals(time_unit, "month", StringComparison.CurrentCultureIgnoreCase))
            {
                istime_unit = true;
            }
            else
            {
                istime_unit = false;
            }
            return istime_unit;
        }

        public static bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Z0-9_\u4e00-\u9fa5]+$") && (name.Length < 256);
        }

        public static bool IsValidTag(string tag)
        {
            return Regex.IsMatch(tag, @"^[a-zA-Z0-9_\u4e00-\u9fa5]+$") && (tag.Length < 41);
        }

        public static bool IsValidAlias(string alias)
        {
            return Regex.IsMatch(alias, @"^[a-zA-Z0-9_\u4e00-\u9fa5]+$") && (alias.Length < 41);
        }
    }
}
