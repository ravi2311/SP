using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http;

namespace SP.Controllers
{
    public class SpMetadaCollectorController : ApiController
    {
        private readonly SpDbContext db;

        public SpMetadaCollectorController()
        {
            this.db = new SpDbContext();
        }
        [HttpGet]
        public IHttpActionResult Get(bool isList, string siteId = "", string siteCollection = "", DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            //Authorization
            //if (!HttpContext.Current.User.IsInRole("IM-SharePoint-Metadata-DEV"))
            //{
            //    return BadRequest("User is not authorized to access the API");
            //}
            //Validation
            if (string.IsNullOrEmpty(siteId) && string.IsNullOrEmpty(siteCollection))
                return BadRequest("SiteID or SiteCollection parameter is required");
            //Get Latest job run
            var mostRecentJobEndDate = db.JobStatus.OrderByDescending(e => e.JobEndDate).FirstOrDefault();
            if (isList)
            {
                var listDetails = from ld in db.ListDetails
                                  join site in db.SiteMaster on ld.SiteID equals site.SiteID
                                  where (ld.SiteID == siteId || site.SiteURL == siteCollection)
                                 && (dateFrom == null || ld.ModifiedDate >= dateFrom) && (dateTo == null || ld.ModifiedDate <= dateTo)
                                  select ld;
                //ListDetails
                return Json(new
                {
                    ItemCount = listDetails.Count(),
                    LastModifiedDate = listDetails.OrderByDescending(e => e.ModifiedDate).FirstOrDefault()?.ModifiedDate?.ToString("yyyy-MM-dd hh:mm:ss tt"),
                    LastCrawlDate = mostRecentJobEndDate?.JobEndDate?.ToString("yyyy-MM-dd hh:mm:ss tt"),
                });
            }
            else
            {
                var libDetails = from ld in db.DocLibDetails
                                 join site in db.SiteMaster on ld.SiteID equals site.SiteID
                                 where (ld.SiteID == siteId || site.SiteURL == siteCollection)
                                 && (dateFrom == null || ld.ModifiedDate >= dateFrom) && (dateTo == null || ld.ModifiedDate <= dateTo)
                                 select ld;
                //DocLibdetails
                return Json(new
                {
                    ItemCount = libDetails.Count(),
                    LastModifiedDate = libDetails.OrderByDescending(e => e.ModifiedDate).FirstOrDefault()?.ModifiedDate?.ToString("yyyy-MM-dd hh:mm:ss tt"),
                    LastCrawlDate = mostRecentJobEndDate?.JobEndDate?.ToString("yyyy-MM-dd hh:mm:ss tt"),
                });
            }
        }
        public IHttpActionResult Get(bool isList, int page, int pageSize, string siteId = "", string siteCollection = "", DateTime? dateFrom = null, DateTime? dateTo = null)
        {
            //Authorization
            //if(!HttpContext.Current.User.IsInRole("IM-SharePoint-Metadata-DEV"))
            //{
            //    return BadRequest("User is not authorized to access the API");
            //}
            //Validation
            if (string.IsNullOrEmpty(siteId) && string.IsNullOrEmpty(siteCollection))
                return BadRequest("SiteID or SiteCollection parameter is required");
            if (isList)
            {
                //ListDetails
                var data = (from ld in db.ListDetails
                           join site in db.SiteMaster on ld.SiteID equals site.SiteID
                           join web in db.WebMaster on ld.WebID equals web.WebID into ldweb
                           from web in ldweb.DefaultIfEmpty()
                           where (ld.SiteID == siteId || site.SiteURL == siteCollection)
                           && (dateFrom == null || ld.ModifiedDate >= dateFrom) && (dateTo == null || ld.ModifiedDate <= dateTo)
                           select new
                           {
                               ld.SiteID,
                               site.SiteURL,
                               ld.WebID,
                               web.WebURL,
                               ld.ListName,
                               ld.ListDescription,
                               ld.ListItemCount,
                               ld.ListColumns,
                               ld.CreatedBy,
                               ld.CreatedDate
                           }).OrderBy(e => e.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                //Data preprocessing
                var finalResult=data.Select(e=>new {
                    e.SiteID,
                    e.SiteURL,
                    e.WebID,
                    e.WebURL,
                    e.ListName,
                    e.ListDescription,
                    e.ListItemCount,
                    e.ListColumns,
                    CreatedDate=e.CreatedDate.Value.ToString("yyyy-MM-dd hh:mm:ss tt"),
                    CreatedBy = e.CreatedBy.Substring(e.CreatedBy.IndexOf("#")+1)
                });
                return Json(finalResult);
            }
            else
            {
                //Doc
                var data = (from ld in db.DocLibDetails
                           join site in db.SiteMaster on ld.SiteID equals site.SiteID
                           join web in db.WebMaster on ld.WebID equals web.WebID into ldwe
                           from web in ldwe.DefaultIfEmpty()
                           where (ld.SiteID == siteId || site.SiteURL == siteCollection)
                           && (dateFrom == null || ld.ModifiedDate >= dateFrom) && (dateTo == null || ld.ModifiedDate <= dateTo)
                           select new
                           {
                               ld.SiteID,
                               site.SiteURL,
                               ld.WebID,
                               web.WebURL,
                               ld.DocName,
                               ld.FileType,
                               ld.DocPath,
                               ld.DocSize,
                               ld.CreatedBy,
                               ld.CreatedDate
                           }).OrderBy(e => e.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
                //Data preprocessing
                var finalResult = data.Select(e => new {
                    e.SiteID,
                    e.SiteURL,
                    e.WebID,
                    e.WebURL,
                    e.DocName,
                    e.FileType,
                    e.DocPath,
                    e.DocSize,
                    CreatedDate= e.CreatedDate.Value.ToString("yyyy-MM-dd hh:mm:ss tt"),
                    CreatedBy = e.CreatedBy.Substring(e.CreatedBy.IndexOf("#")+1)
                });
                return Json(finalResult);
            }
        }
    }
}
