using ONLINEAPP.MODEL;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.DAL
{
    public class OverallReport
    {
        public static List<dynamic> GetOverallReport<T>(string reportType, string listName, string siteUrl, string token)
        {
            try
            {
                DateTime todaysDate = DateTime.Now;

                int yearCount = Convert.ToInt32(Constants.OverallReportYearsCount);
                string value2 = todaysDate.Year + "-" + todaysDate.Month + "-" + todaysDate.Day + "T23:59:59";
                string value1 = todaysDate.AddYears(-yearCount).Year + "-01-01T00:00:00";

                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithSelectedViewModel<T>(listName),
                                                       string.Format(RESTFilters.ByCreatedDateRange, value1, value2),
                                                       string.Format(RESTFilters.topItems, GetTop._50000),
                                                       string.Format(RESTFilters.orderByDescending, Fields.ID));

                List<T> lst = CRUDOperations.GetListByRestURL<T>(RestUrl, token).ToList();

                if (reportType.ToLower().Equals(Constants.Quarterly.ToLower()))
                {
                    return GetOverallReportByQuarter(lst, Fields.Created);
                }
                else
                {
                    return GetOverallReportByMonth(lst, Fields.Created);
                }
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static List<dynamic> GetOverallReport<T>(string reportType, string listName1, string listName2,string filter, string siteUrl, string token)
        {
            try
            {
                DateTime todaysDate = DateTime.Now;

                int yearCount = Convert.ToInt32(Constants.OverallReportYearsCount);
                string value2 = todaysDate.Year + "-" + todaysDate.Month + "-" + todaysDate.Day + "T23:59:59";
                string value1 = todaysDate.AddYears(-yearCount).Year + "-01-01T00:00:00";

                string RestUrl1 = string.Concat(siteUrl, ListURLs.RestUrlListItemWithSelectedViewModel<T>(listName1),
                                                       string.Format(filter, value1, value2),
                                                       string.Format(RESTFilters.topItems, GetTop._50000),
                                                       string.Format(RESTFilters.orderByDescending, Fields.ID));

                string RestUrl2 = string.Concat(siteUrl, ListURLs.RestUrlListItemWithSelectedViewModel<T>(listName2),
                                                       string.Format(filter, value1, value2),
                                                       string.Format(RESTFilters.topItems, GetTop._50000),
                                                       string.Format(RESTFilters.orderByDescending, Fields.ID));


                List<T> lst = new List<T>();

                Task<List<T>> lst1 = new Task<List<T>>(() => { return CRUDOperations.GetListByRestURL<T>(RestUrl1, token).ToList(); });
                lst1.Start();

                Task<List<T>> lst2 = new Task<List<T>>(() => { return CRUDOperations.GetListByRestURL<T>(RestUrl2, token).ToList(); });
                lst2.Start();
                lst1.Wait();
                lst2.Wait();

                lst.AddRange(lst1.Result.ToList());
                lst.AddRange(lst2.Result.ToList());

                if (reportType.ToLower().Equals(Constants.Quarterly.ToLower()))
                {
                    return GetOverallReportByQuarter(lst, Fields.LogDate);
                }
                else
                {
                    return GetOverallReportByMonth(lst, Fields.LogDate);
                }
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static List<dynamic> GetOverallReportByMonth<T>(List<T> lst, string fieldName)
        {
            List<dynamic> lstResult = new List<dynamic>();
            try
            {
                List<string> lstMonths = DateAndQuarterOperations.GetMonths();

                int yearCount = Convert.ToInt32(Constants.OverallReportYearsCount) - 1;

                List<string> headers = new List<string>();

                headers.Add("Month");
                for (int i = DateTime.Now.AddYears(-yearCount).Year; i <= DateTime.Now.Year; i++)
                {
                    headers.Add(string.Concat("CY", i.ToString()));
                }

                var lstResultThread = new ConcurrentBag<dynamic>();
                Parallel.ForEach(lstMonths, month =>
                {
                    dynamic obj = new ExpandoObject();
                    obj.Month = month;
                    for (int i = 1; i < headers.Count(); i++)
                    {
                        string headerValue = headers[i].Replace("CY", "");
                        int? count = lst.Cast<T>().Where(item => item.GetType().GetProperty(fieldName) != null && !string.IsNullOrEmpty(Convert.ToString(item.GetType().GetProperty(fieldName).GetValue(item, null)))
                                                            && Convert.ToDateTime(Convert.ToString(item.GetType().GetProperty(fieldName).GetValue(item, null))).Year.ToString().Equals(headerValue)
                                                            && DateAndQuarterOperations.GetMonthByInteger(Convert.ToDateTime(Convert.ToString(item.GetType().GetProperty(fieldName).GetValue(item, null))).Month).ToLower().Equals(month.ToLower()))
                                                            .Select(item => item).ToList().Count();
                        if (headerValue.Equals(DateTime.Now.Year.ToString()))
                        {
                            int monthInt = DateAndQuarterOperations.GetMonthByString(month);
                            if (monthInt > DateTime.Now.Month)
                            {
                                count = null;
                            }
                        }
                        DynamicExpandoObjects.AddProperty(obj, headers[i], count);
                    }
                    lstResultThread.Add(obj);
                });

                lstResult = lstResultThread.Cast<dynamic>().ToList();

                return lstResult.OrderBy(x => DateAndQuarterOperations.GetMonthByString(x.Month)).ToList();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public static List<dynamic> GetOverallReportByQuarter<T>(List<T> lst, string fieldName)
        {
            List<dynamic> lstResult = new List<dynamic>();
            try
            {
                List<string> lstQuarters = DateAndQuarterOperations.GetQuarters();

                int yearCount = Convert.ToInt32(Constants.OverallReportYearsCount) - 1;

                List<string> headers = new List<string>();

                headers.Add("Quarter");
                for (int i = DateTime.Now.AddYears(-yearCount).Year; i <= DateTime.Now.Year; i++)
                {
                    headers.Add(string.Concat("CY", i.ToString()));
                }

                var lstResultThread = new ConcurrentBag<dynamic>();
                Parallel.ForEach(lstQuarters, quarter =>
                {
                    dynamic obj = new ExpandoObject();
                    obj.Quarter = quarter;
                    for (int i = 1; i < headers.Count(); i++)
                    {
                        string headerValue = headers[i].Replace("CY", "");
                        int? count = lst.Cast<T>().Where(item => item.GetType().GetProperty(fieldName) != null && !string.IsNullOrEmpty(Convert.ToString(item.GetType().GetProperty(fieldName).GetValue(item, null)))
                                                            && Convert.ToDateTime(Convert.ToString(item.GetType().GetProperty(fieldName).GetValue(item, null))).Year.ToString().Contains(headerValue)
                                                            && DateAndQuarterOperations.GetQuarterByInteger(Convert.ToDateTime(Convert.ToString(item.GetType().GetProperty(fieldName).GetValue(item, null))).Month).ToLower().Equals(quarter.ToLower()))
                                                            .Select(item => item).ToList().Count();
                        if (headerValue.Equals(DateTime.Now.Year.ToString()))
                        {
                            int quarterInt = DateAndQuarterOperations.GetQuarterByString(quarter);
                            string currentQuarter = DateAndQuarterOperations.GetQuarterByInteger(DateTime.Now.Month);
                            int currentQuarterInt = DateAndQuarterOperations.GetQuarterByString(currentQuarter);
                            if (quarterInt > currentQuarterInt)
                            {
                                count = null;
                            }
                        }
                        DynamicExpandoObjects.AddProperty(obj, headers[i], count);
                    }
                    lstResultThread.Add(obj);
                });

                lstResult = lstResultThread.Cast<dynamic>().ToList();

                return lstResult.OrderBy(x => DateAndQuarterOperations.GetQuarterByString(x.Quarter)).ToList();
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }
    }
}
