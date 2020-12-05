using SP.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SP
{
    public class SpDbContext : DbContext
    {
        public SpDbContext():base(nameOrConnectionString: "MyConnection")
        {
        }
        public DbSet<IM_SPWebMaster> WebMaster { get; set; }
        public DbSet<IM_SPSiteMasterDM> SiteMaster { get; set; }
        public DbSet<IM_SPListDetailsDM> ListDetails { get; set; }
        public DbSet<IM_SPDocLibMasterDM> DocLibMaster { get; set; }
        public DbSet<IM_SPDocLibDetailsDM> DocLibDetails { get; set; }
        public DbSet<IM_JobStatus> JobStatus { get; set; }

    }
}