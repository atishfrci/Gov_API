using ONLINEAPP.MODEL;
using ONLINEAPP.TRANSPORTS.MODEL;
using ONLINEAPP.TRANSPORTS.VIEWMODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.TRANSPORTS.INTERFACE
{
    public interface INews
    {
        List<NewsViewModel> GetNews(string siteUrl, string token);

        NewsViewModel GetNewsByID(string id, string siteUrl, string token);

        List<NewsModel> GetNewsByStatus(string Status, string siteUrl, string token);

        Result AddNews(NewsModel objNews, string siteUrl, string token);

        Result UpdateNewsByID(NewsModel objNews, string siteUrl, string token);

        Result DeleteNewsByID(string id, string siteUrl, string token);

    }
}
