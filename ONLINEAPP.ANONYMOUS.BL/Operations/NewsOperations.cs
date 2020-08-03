﻿using ONLINEAPP.ANONYMOUS.INTERFACE;
using ONLINEAPP.ANONYMOUS.MODEL;
using ONLINEAPP.ANONYMOUS.VIEWMODEL;
using ONLINEAPP.DAL;
using ONLINEAPP.MODEL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ONLINEAPP.ANONYMOUS.BL.Operations
{
    public class NewsOperations : INews
    {
        public List<NewsViewModel> GetNews(string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.FrciNews, true),
                                                    string.Format(RESTFilters.topItems, GetTop._1000), string.Format(RESTFilters.orderByDescending, Fields.ID));

                var _result = CRUDOperations.GetListByRestURL<News>(RestUrl, token);

                List<NewsViewModel> _list = MapNewsAndImages(siteUrl, token, _result);

                return _list;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        private List<NewsViewModel> MapNewsAndImages(string siteUrl, string token, List<News> _result)
        {
            List<NewsViewModel> _list = new List<NewsViewModel>();
            foreach (News item in _result)
            {
                NewsViewModel _news = new NewsViewModel();
                _news.Id = item.Id;
                _news.Title = item.Title;
                _news.newstitle = item.newstitle;
                _news.Description = item.Description;
                _news.ActiveOn = item.ActiveOn;
                _news.PinonTop = item.PinonTop;
                _news.Photos = GetImages(siteUrl, token, item.Title);
                _list.Add(_news);
            }
            return _list;
        }

        public NewsViewModel GetNewsByID(string id, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.FrciNews, true),
                                                    string.Format(RESTFilters.ByID, id));

                var result = CRUDOperations.GetListByRestURL<News>(RestUrl, token).FirstOrDefault();
                NewsViewModel _news = new NewsViewModel()
                {
                    Id = result.Id,
                    Title = result.Title,
                    newstitle = result.newstitle,
                    Description = result.Description,
                    ActiveOn = result.ActiveOn,
                    PinonTop = result.PinonTop,
                    Photos = GetImages(siteUrl, token, result.Title)
                };
                return _news;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public List<News> GetNewsByStatus(string Status, string siteUrl, string token)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, ListURLs.RestUrlListItemWithQuery(Lists.FrciNews, true),
                                                    string.Format(RESTFilters.ByStatus, Status));

                return CRUDOperations.GetListByRestURL<News>(RestUrl, token);
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }

        public Result AddNews(News objNews, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.AddListItem<News>(siteUrl, token, Lists.FrciNews, objNews);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgNewsAddedSuccessfully;
                return res;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                res.Message = Messages.MsgSomethingWentWrong;
                res.StatusCode = StatusCode.Error;
                return res;
            }
        }

        public Result UpdateNewsByID(News objNews, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.UpdateListItem<News>(siteUrl, token, Lists.FrciNews, objNews);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgNewsUpdatedSuccessfully;
                return res;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                res.Message = Messages.MsgNewsUpdatedSuccessfully;
                res.StatusCode = StatusCode.Error;
                return res;
            }
        }

        public Result DeleteNewsByID(string id, string siteUrl, string token)
        {
            Result res = new Result();
            try
            {
                res = CRUDOperations.DeleteListItem(siteUrl, Lists.FrciNews, id);

                res.StatusCode = StatusCode.Success;
                res.Message = Messages.MsgNewsDeletedSuccessfully;
                return res;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                res.Message = Messages.MsgSomethingWentWrong;
                res.StatusCode = StatusCode.Error;
                return res;
            }
        }

        public List<File> GetImages(string siteUrl, string token, string NewsTitle)
        {
            try
            {
                string RestUrl = string.Concat(siteUrl, "");

                var result = CRUDOperations.GetPublishingImages<File>(RestUrl, token, Constants.NewsPublisingImgRelativeUrl + NewsTitle);
                if (result != null)
                {
                    result.ForEach(img => img.ServerRelativeUrl = Constants.ROOTSiteUrl + img.ServerRelativeUrl);
                }
                return result;
            }
            catch (Exception ex)
            {
                string guid = CRUDOperations.WriteException(ex, MethodBase.GetCurrentMethod().Name, MethodBase.GetCurrentMethod().DeclaringType.Name);
                throw new Exception(string.Format(Messages.MsgExceptionOccured, guid));
            }
        }
    }
}
