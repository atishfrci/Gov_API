using ONLINEAPP.ANONYMOUS.MODEL;
using ONLINEAPP.ANONYMOUS.VIEWMODEL;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.ANONYMOUS.INTERFACE
{
    public interface INews
    {
        List<NewsViewModel> GetNews(string siteUrl, string token);

        NewsViewModel GetNewsByID(string id, string siteUrl, string token);

        List<News> GetNewsByStatus(string Status, string siteUrl, string token);

        Result AddNews(News objNews, string siteUrl, string token);

        Result UpdateNewsByID(News objNews, string siteUrl, string token);

        Result DeleteNewsByID(string id, string siteUrl, string token);
    }
}
