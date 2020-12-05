using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SP.Models
{
    [Table("IM_SPListDetails")]
    public class IM_SPListDetailsDM
    {
        //CreatedBy CreatedDate ModifiedBy ModifiedDate
        [ForeignKey("Site")]
        [DataType("nvarchar(100)")]
        [Required]
        public string SiteID { get; set; }
        public IM_SPSiteMasterDM Site { get; set; }
        [DataType("nvarchar(100)")]
        public string WebID { get; set; }
        [Key]
        [DataType("nvarchar(1000)")]
        public string ListName { get; set; }
        [DataType("nvarchar(4000)")]
        public string ListDescription { get; set; }
        public int? ListItemCount { get; set; }
        [DataType("nvarchar(4000)")]
        public string ListColumns { get; set; }
        [DataType("nvarchar(100)")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [DataType("nvarchar(100)")]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    [Table("IM_SPDocLibDetails")]
    public class IM_SPDocLibDetailsDM
    {
        //SiteID WebID   DocLibID DocName FileType DocPath DocSize CreatedBy   CreatedDate ModifiedBy  ModifiedDate
        [ForeignKey("Site")]
        [DataType("nvarchar(100)")]
        [Required]
        public string SiteID { get; set; }
        public IM_SPSiteMasterDM Site { get; set; }
        [DataType("nvarchar(100)")]
        public string WebID { get; set; }
        [Key,Column(Order =1)]
        [DataType("nvarchar(100)")]
        public string DocLibID { get; set; }
        [DataType("nvarchar(1000)")]
        [Key,Column(Order =2)]
        public string DocName { get; set; }
        [DataType("nvarchar(100)")]
        public string FileType { get; set; }
        [DataType("nvarchar(4000)")]
        public string DocPath { get; set; }
        public int? DocSize { get; set; }
        [DataType("nvarchar(100)")]
        public string CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        [DataType("nvarchar(100)")]
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    [Table("IM_SPDocLibMaster")]
    public class IM_SPDocLibMasterDM
    {
        //SiteID WebID   DocLibID DocLibTitle DocLibDesc DocLibItemCount
        [ForeignKey("Site")]
        [DataType("nvarchar(100)")]
        [Required]
        public string SiteID { get; set; }
        public IM_SPSiteMasterDM Site { get; set; }
        [DataType("nvarchar(100)")]
        public string WebID { get; set; }
        [Key]
        [DataType("nvarchar(100)")]
        public Guid DocLibID { get; set; }
        [DataType("nvarchar(1000)")]
        public string DocLibTitle { get; set; }
        [DataType("nvarchar(4000)")]
        public string DocLibDesc { get; set; }
        public int? DocLibItemCount { get; set; }
    }
    [Table("IM_SPWebMaster")]
    public class IM_SPWebMaster
    {
        [ForeignKey("Site")]
        [DataType("nvarchar(100)")]
        [Required]
        public string SiteID { get; set; }
        public IM_SPSiteMasterDM Site { get; set; }
        [DataType("nvarchar(100)")]
        [Key]
        public string WebID { get; set; }
        [DataType("nvarchar(4000)")]
        public string WebURL { get; set; }
        [DataType("nvarchar(1000)")]
        public string WebTitle { get; set; }
        [DataType("nvarchar(4000)")]
        public string WebDesc { get; set; }
    }
    [Table("IM_SPSiteMaster")]
    public class IM_SPSiteMasterDM
    {
        //SiteID SiteURL PortalHierarcy Region  SiteTitle SiteDesc    WebCount
        [Key]
        [DataType("nvarchar(100)")]
        public string SiteID { get; set; }
        [DataType("nvarchar(4000)")]
        public string SiteURL { get; set; }
        [DataType("nvarchar(50)")]
        public string PortalHierarchy { get; set; }
        [DataType("nvarchar(50)")]
        public string Region { get; set; }
        [DataType("nvarchar(1000)")]
        public string SiteTitle { get; set; }
        [DataType("nvarchar(4000)")]
        public string SiteDesc { get; set; }
        public int WebCount { get; set; }
    }
    [Table("IM_JobStatus")]
    public class IM_JobStatus
    {
        [Key]
        public long JobID { get; set; }
        [DataType(DataType.Date)]
        public DateTime? JobStartDate { get; set; }
        [DataType(DataType.Date)]
        public DateTime? JobEndDate { get; set; }
        [DataType("nvarchar(100)")]
        [Required]
        public string ServerName { get; set; }
        [DataType("nvarchar(50)")]
        public string PortalHierarchy { get; set; }
        [DataType("varchar(50)")]
        public string Region { get; set; }
        public int JobErrorCount { get; set; }
        [DataType("nvarchar(max)")]
        public string Comments { get; set; }
    }
}