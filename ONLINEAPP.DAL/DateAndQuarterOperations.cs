using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.DAL
{
    public class DateAndQuarterOperations
    {
        public static string GetCurrentQuarter()
        {
            int month = DateTime.Now.Month;

            if ((month > 0) & (month <= 3))
            {
                return "Q1";
            }
            else if ((month > 3) & (month <= 6))
            {
                return "Q2";
            }
            else if ((month > 6) & (month <= 9))
            {
                return "Q3";
            }
            else
            {
                return "Q4";
            }
        }

        public static string GetCurrentQuarterStartDate()
        {
            string startDate = string.Empty;
            try
            {
                int month = DateTime.Now.Month;
                if (month >= 1 && month <= 3)
                {
                    startDate = DateTime.Now.Year + "-01-01T00:00:00";
                }
                else if (month >= 4 && month <= 6)
                {
                    startDate = DateTime.Now.Year + "-04-01T00:00:00";
                }
                else if (month >= 7 && month <= 9)
                {
                    startDate = DateTime.Now.Year + "-07-01T00:00:00";
                }
                else
                {
                    startDate = DateTime.Now.Year + "-10-01T00:00:00";
                }


                return startDate;

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static List<string> GetQuarterStartAndEndDateByQuarterAndYear(string year, string quarter)
        {
            List<string> dates = new List<string>();
            try
            {
                string startDate = string.Empty;
                string enddate = string.Empty;
                if (quarter.ToUpper().Equals(Constants.Q1))
                {
                    startDate = year + "-01-01T00:00:00";
                    enddate = year + "-03-" + DateTime.DaysInMonth(Convert.ToInt32(year), 3) + "T23:59:59";
                }
                else if (quarter.ToUpper().Equals(Constants.Q2))
                {
                    startDate = year + "-04-01T00:00:00";
                    enddate = year + "-06-" + DateTime.DaysInMonth(Convert.ToInt32(year), 6) + "T23:59:59";
                }
                else if (quarter.ToUpper().Equals(Constants.Q3))
                {
                    startDate = year + "-07-01T00:00:00";
                    enddate = year + "-09-" + DateTime.DaysInMonth(Convert.ToInt32(year), 9) + "T23:59:59";
                }
                else
                {
                    startDate = year + "-10-01T00:00:00";
                    enddate = year + "-12-" + DateTime.DaysInMonth(Convert.ToInt32(year), 12) + "T23:59:59";
                }

                dates.Add(startDate);
                dates.Add(enddate);
                return dates;

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static string GetQuarterByDate(DateTime dt)
        {
            string quarter = string.Empty;
            try
            {
                int month = dt.Month;
                if (month >= 1 && month <= 3)
                {
                    return dt.Year + "-Q1";
                }
                else if (month >= 4 && month <= 6)
                {
                    return dt.Year + "-Q2";
                }
                else if (month >= 7 && month <= 9)
                {
                    return dt.Year + "-Q3";
                }
                else
                {
                    return dt.Year + "-Q4";
                }

            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }



        public static DateTime GetFirstDateOfQuarter(DateTime dt)
        {
            int month = dt.Month;
            if (month >= 1 && month <= 3)
            {
                return new DateTime(dt.Year, 1, 1);
            }
            else if (month >= 4 && month <= 6)
            {
                return new DateTime(dt.Year, 4, 1);
            }
            else if (month >= 7 && month <= 9)
            {
                return new DateTime(dt.Year, 7, 1);
            }
            else
            {
                return new DateTime(dt.Year, 10, 1);
            }
        }

        public static DateTime GetLastDateOfQuarter(DateTime dt)
        {
            int month = dt.Month;
            if (month >= 1 && month <= 3)
            {
                return new DateTime(dt.Year, 1, DateTime.DaysInMonth(dt.Year, 3));
            }
            else if (month >= 4 && month <= 6)
            {
                return new DateTime(dt.Year, 4, DateTime.DaysInMonth(dt.Year, 6));
            }
            else if (month >= 7 && month <= 9)
            {
                return new DateTime(dt.Year, 7, DateTime.DaysInMonth(dt.Year, 9));
            }
            else
            {
                return new DateTime(dt.Year, 10, DateTime.DaysInMonth(dt.Year, 12));
            }
        }

        public static DateTime GetFirstDateOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime firstDayInWeek = dayInWeek.Date;
            while (firstDayInWeek.DayOfWeek != firstDay)
                firstDayInWeek = firstDayInWeek.AddDays(-1);

            return firstDayInWeek;
        }


        public static DateTime GetLastDateOfWeek(DateTime dayInWeek, DayOfWeek firstDay)
        {
            DateTime lastDayInWeek = dayInWeek.Date;
            while (lastDayInWeek.DayOfWeek != firstDay)
                lastDayInWeek = lastDayInWeek.AddDays(1);

            return lastDayInWeek;
        }

        public static List<string> GetMonths()
        {
            string[] months = new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sept", "Oct", "Nov", "Dec" };

            return months.ToList<string>();

        }

        public static List<string> GetQuarters()
        {
            string[] quarters = new string[] { "Q1", "Q2", "Q3", "Q4" };

            return quarters.ToList<string>();

        }

        public static string GetMonthByInteger(int month)
        {
            if (month == 1)
                return "Jan";
            else if (month == 2)
                return "Feb";
            else if (month == 3)
                return "Mar";
            else if (month == 4)
                return "Apr";
            else if (month == 5)
                return "May";
            else if (month == 6)
                return "Jun";
            else if (month == 7)
                return "Jul";
            else if (month == 8)
                return "Aug";
            else if (month == 9)
                return "Sept";
            else if (month == 10)
                return "Oct";
            else if (month == 11)
                return "Nov";
            else
                return "Dec";
        }

        public static int GetMonthByString(string month)
        {
            if (month.ToLower().Equals("jan"))
                return 1;
            else if (month.ToLower().Equals("feb"))
                return 2;
            else if (month.ToLower().Equals("mar"))
                return 3;
            else if (month.ToLower().Equals("apr"))
                return 4;
            else if (month.ToLower().Equals("may"))
                return 5;
            else if (month.ToLower().Equals("jun"))
                return 6;
            else if (month.ToLower().Equals("jul"))
                return 7;
            else if (month.ToLower().Equals("aug"))
                return 8;
            else if (month.ToLower().Equals("sept"))
                return 9;
            else if (month.ToLower().Equals("oct"))
                return 10;
            else if (month.ToLower().Equals("nov"))
                return 11;
            else
                return 12;
        }

        public static string GetQuarterByInteger(int month)
        {
            string quarter = string.Empty;
            try
            {
                if (month >= 1 && month <= 3)
                    return "Q1";
                else if (month >= 4 && month <= 6)
                    return "Q2";
                else if (month >= 7 && month <= 9)
                    return "Q3";
                else
                    return "Q4";
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static int GetQuarterByString(string quarter)
        {
            if (quarter.ToLower().Equals("q1"))
                return 1;
            else if (quarter.ToLower().Equals("q2"))
                return 2;
            else if (quarter.ToLower().Equals("q3"))
                return 3;
            else
                return 4;
        }
    }
}
