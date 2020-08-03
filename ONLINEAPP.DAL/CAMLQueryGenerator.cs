using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.DAL
{
    public class CAMLQueryGenerator
    {

        public static string GenerateCAMLQuery(List<string> allSearchResult, string fieldName, string valueType, string condition, string operation, string rowLimit)
        {
            string query = string.Empty;
            if (allSearchResult != null && allSearchResult.Count > 0)
            {
                if (allSearchResult.Count() == 1)
                {
                    query = string.Concat("<", condition, "><FieldRef Name='", fieldName, "' /><Value Type='", valueType, "'>", allSearchResult[0].ToString().Trim(), "</Value></", condition, ">");
                }
                else if (allSearchResult.Count() == 2)
                {
                    query = string.Concat("<", operation, ">",
                                                "<", condition, "><FieldRef Name='", fieldName, "' /><Value Type='", valueType, "'>", allSearchResult[0].ToString().Trim(), "</Value></", condition, ">",
                                                "<", condition, "><FieldRef Name='", fieldName, "' /><Value Type='", valueType, "'>", allSearchResult[1].ToString().Trim(), "</Value></", condition, ">",
                                          "</", operation, ">");
                }
                else
                {
                    query = string.Concat("<", operation, ">",
                                               "<", condition, "><FieldRef Name='", fieldName, "' /><Value Type='", valueType, "'>", allSearchResult[0].ToString().Trim(), "</Value></", condition, ">",
                                               "<", condition, "><FieldRef Name='", fieldName, "' /><Value Type='", valueType, "'>", allSearchResult[1].ToString().Trim(), "</Value></", condition, ">",
                                         "</", operation, ">");

                    for (int i = 2; i < allSearchResult.Count(); i++)
                    {

                        query = string.Concat("<", operation, ">", query);
                        query += string.Concat("<", condition, "><FieldRef Name='", fieldName, "' /><Value Type='", valueType, "'>", allSearchResult[i].ToString().Trim(), "</Value></", condition, ">");
                        query += string.Concat("</", operation, ">");

                    }
                }

                query = string.Concat("{ 'query' : {'__metadata': { 'type': 'SP.CamlQuery' }, \"ViewXml\": \"<View><Query><Where>", query,
                                        "</Where></Query><RowLimit>", rowLimit, "</RowLimit></View>\" } }");
            }

            return query;
        }

    }
}
