using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.DAL
{
    public class DataTableToHTMLConverter
    {
        public static string ConvertDataTableToHTML(DataTable dt)
        {
            string html = "<table style='width:1px;border-style:solid;border-collapse:collapse;color:#183544;font-family:calibri'>";
            //add header row
            html += "<tr>";
            for (int i = 0; i < dt.Columns.Count; i++)
                html += "<td style='border-style:solid;border-width:1px;padding:5px 10px; white-space:nowrap'><b>" + dt.Columns[i].ColumnName + "</b></td>";
            html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td style='border-style:solid;border-width:1px;padding:5px 10px; white-space:nowrap'>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }

        public static string ConvertDataTableToHTMLWithoutHeading(DataTable dt)
        {
            string html = "<table style='width:1px;border-style:solid;border-collapse:collapse;color:#183544;font-family:calibri'>";
            //add header row
            //html += "<tr>";
            //for (int i = 0; i < dt.Columns.Count; i++)
            //    html += "<td style='border-style:solid;border-width:1px;padding:5px 10px; white-space:nowrap'>" + dt.Columns[i].ColumnName + "</td>";
            //html += "</tr>";
            //add rows
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                html += "<tr>";
                for (int j = 0; j < dt.Columns.Count; j++)
                    html += "<td style='border-style:solid;border-width:1px;padding:5px 10px; white-space:nowrap'>" + dt.Rows[i][j].ToString() + "</td>";
                html += "</tr>";
            }
            html += "</table>";
            return html;
        }
    }
}
