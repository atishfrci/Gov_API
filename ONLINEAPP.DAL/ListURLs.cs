using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.DAL
{
    public class ListURLs
    {
        public static string RestUrlList(string listName)
        {
            return string.Format("/_api/web/lists/GetByTitle('{0}')", listName);
        }

        public static string RestUrlListItemWithQuery(string listName, bool isQueryRequired)
        {
            if (isQueryRequired)
            {
                Type myType = typeof(SelectExpandListFields);
                string selectExpandListName = listName.Replace(" ", string.Empty);
                if (myType.GetField(selectExpandListName) == null)
                    return string.Concat(RestUrlList(listName), "/items?$select=*");
                else
                    return string.Concat(RestUrlList(listName), "/items", myType.GetField(selectExpandListName).GetValue(null));
            }
            else
            {
                return string.Concat(RestUrlList(listName), "/items");
            }
        }

        public static string RestUrlListItemWithQueryViewModel(string listName, string selectQueryName)
        {
            return string.Concat(RestUrlList(listName), "/items", selectQueryName);
        }

        public static string RestUrlListChoiceFields(string listName, string ChoiceFieldName)
        {
            return string.Format(string.Concat(RestUrlList(listName), "/fields?$filter=EntityPropertyName eq '{0}'"), ChoiceFieldName);
        }

        public static string RestUrlListItemWithSelectedViewModel<T>(string listName)
        {

            Type myType = typeof(T);
            PropertyInfo[] propInfo = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

            if (propInfo != null && propInfo.Count() > 0)
            {
                //int cnt = 0;
                List<string> lstSelectFields = new List<string>();
                List<string> lstExpandFields = new List<string>();
                StringBuilder sb = new StringBuilder();
                foreach (PropertyInfo prop in propInfo)
                {
                    string propertyName = GenerateJsonFromObject.GetJsonPropertyName<T>(prop.Name);
                    if (prop.PropertyType == typeof(User) || prop.PropertyType == typeof(List<User>) || prop.PropertyType == typeof(LookupValue))
                    {
                        lstSelectFields.Add(propertyName + "/ID," + propertyName + "/Title");
                        lstExpandFields.Add(propertyName);
                    }
                    else if (prop.PropertyType.Name.Contains(Constants.User))
                    {
                        if (prop.PropertyType == typeof(UserAndEmail))
                        {
                            lstSelectFields.Add(propertyName + "/ID," + propertyName + "/Title," + propertyName + "/EMail");
                            lstExpandFields.Add(propertyName);
                        }
                        else if (prop.PropertyType == typeof(UserEmployeeName))
                        {
                            lstSelectFields.Add(propertyName + "/ID," + propertyName + "/EmployeeName");
                            lstExpandFields.Add(propertyName);
                        }
                    }
                    else if (prop.PropertyType == typeof(List<Attachment>))
                    {
                        lstSelectFields.Add(propertyName);
                        lstExpandFields.Add(propertyName);
                    }
                    else if (prop.PropertyType.Name.Contains(Constants.Lookup))
                    {
                        if (prop.PropertyType == typeof(LookupDesignationValue))
                        {
                            lstSelectFields.Add(propertyName + "/ID," + propertyName + "/Designation");
                            lstExpandFields.Add(propertyName);
                        }
                        else if (prop.PropertyType == typeof(LookupGradeValue))
                        {
                            lstSelectFields.Add(propertyName + "/ID," + propertyName + "/Grade");
                            lstExpandFields.Add(propertyName);
                        }
                        else if (prop.PropertyType == typeof(LookupLevelValue))
                        {
                            lstSelectFields.Add(propertyName + "/ID," + propertyName + "/Level");
                            lstExpandFields.Add(propertyName);
                        }
                        else if (prop.PropertyType == typeof(LookupGroupValue))
                        {
                            lstSelectFields.Add(propertyName + "/ID," + propertyName + "/Group");
                            lstExpandFields.Add(propertyName);
                        }
                        else if (prop.PropertyType == typeof(LookupChartLevelValue))
                        {
                            lstSelectFields.Add(propertyName + "/ID," + propertyName + "/ChartLevel");
                            lstExpandFields.Add(propertyName);
                        }
                        else if (prop.PropertyType == typeof(LookupMainGroupValue))
                        {
                            lstSelectFields.Add(propertyName + "/ID," + propertyName + "/MainGroup");
                            lstExpandFields.Add(propertyName);
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(propertyName))
                            lstSelectFields.Add(propertyName);
                    }
                }
                lstSelectFields = lstSelectFields.Distinct().ToList();

                lstExpandFields = lstExpandFields.Distinct().ToList();

                if (lstExpandFields.Count > 0)
                {
                    return string.Concat(RestUrlList(listName), "/items?$select=", String.Join(",", lstSelectFields), "&$expand=", String.Join(",", lstExpandFields));
                }
                else
                {
                    return string.Concat(RestUrlList(listName), "/items?$select=", String.Join(",", lstSelectFields));
                }
            }
            else
            {
                return string.Concat(RestUrlList(listName), "/items?$select=*");
            }
        }

        public static string RestUrlListItemWithQueryOnlyID(string listName, bool isQueryRequired)
        {
            if (isQueryRequired)
            {
                Type myType = typeof(SelectExpandListFields);
                string selectExpandListName = listName.Replace(" ", string.Empty);
                if (myType.GetField(selectExpandListName) == null)
                    return string.Concat(RestUrlList(listName), "/items?$select=ID");
                else
                    return string.Concat(RestUrlList(listName), "/items", myType.GetField(selectExpandListName).GetValue(null));
            }
            else
            {
                return string.Concat(RestUrlList(listName), "/items");
            }
        }

        public static string RestUrlListGetItem(string listName)
        {
            return string.Concat(RestUrlList(listName), "/getitems");
        }

        public static string RestUrlLibrary(string libName, string fileName)
        {
            return string.Format("/_api/web/GetFolderByServerRelativeUrl('{0}')/Files('{1}')/$value", libName, fileName);
        }

        public static string RestUrlGroupsByUserId(string userId)
        {
            return string.Format("/_api/web/getuserbyid({0})/groups", userId);
        }

        public static string RestUrlSiteGroups()
        {
            return "/_api/web/roleassignments/groups";
        }

        public static string RestUrlSiteGroupByID(string id)
        {
            return string.Format("/_api/web/sitegroups({0})", id);
        }

        public static string RestUrlSiteGroupByGroupName(string name)
        {
            return string.Format("/_api/web/sitegroups/getbyname({0})", name);
        }

        public static string RestUrlUsersFromGroup(string name)
        {
            return string.Format("/_api/web/sitegroups/getbyname('{0}')/users", name);
        }

        public static string RestDiscussionUrl(string discussionlistName, string DicussionID)
        {
            return string.Format("/_api/web/lists/getbytitle('{0}')/getItemById({1})/Folder", discussionlistName, DicussionID);
        }
        public static string RestDiscussionDirRef(string discussionlistName, string DicussionID)
        {
            return string.Format("/_api/web/lists/getbytitle('{0}')/getItemById({1})?$select=FileDirRef,FileRef", discussionlistName, DicussionID);
        }
        public static string RestUrlMoveDirRef(string fileUrl, string moveFileUrl)
        {
            return string.Format("/_api/web/getfilebyserverrelativeurl('{0}')/moveto(newurl='{1}',flags=1)", fileUrl, moveFileUrl);
        }
    }
}
