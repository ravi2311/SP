namespace SP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.IM_SPDocLibDetails",
                c => new
                    {
                        DocLibID = c.Guid(nullable: false),
                        SiteID = c.String(nullable: false, maxLength: 128),
                        WebID = c.String(),
                        DocName = c.String(),
                        FileType = c.String(),
                        DocPath = c.String(),
                        DocSize = c.Int(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.DocLibID)
                .ForeignKey("dbo.IM_SPSiteMaster", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
            CreateTable(
                "dbo.IM_SPSiteMaster",
                c => new
                    {
                        SiteID = c.String(nullable: false, maxLength: 128),
                        SiteURL = c.String(),
                        PortalHierarchy = c.String(),
                        Region = c.String(),
                        SiteTitle = c.String(),
                        SiteDesc = c.String(),
                        WebCount = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SiteID);
            
            CreateTable(
                "dbo.IM_SPDocLibMaster",
                c => new
                    {
                        DocLibID = c.Guid(nullable: false),
                        SiteID = c.String(nullable: false, maxLength: 128),
                        WebID = c.String(),
                        DocLibTitle = c.String(),
                        DocLibDesc = c.String(),
                        DocLibItemCount = c.Int(),
                    })
                .PrimaryKey(t => t.DocLibID)
                .ForeignKey("dbo.IM_SPSiteMaster", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
            CreateTable(
                "dbo.IM_JobStatus",
                c => new
                    {
                        JobID = c.Long(nullable: false, identity: true),
                        JobStartDate = c.DateTime(),
                        JobEndDate = c.DateTime(),
                        ServerName = c.String(nullable: false),
                        PortalHierarchy = c.String(),
                        Region = c.String(),
                        JobErrorCount = c.Int(nullable: false),
                        Comments = c.String(),
                    })
                .PrimaryKey(t => t.JobID);
            
            CreateTable(
                "dbo.IM_SPListDetails",
                c => new
                    {
                        ListName = c.String(nullable: false, maxLength: 128),
                        SiteID = c.String(nullable: false, maxLength: 128),
                        WebID = c.String(),
                        ListDescription = c.String(),
                        ListItemCount = c.Int(),
                        ListColumns = c.String(),
                        CreatedBy = c.String(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.String(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.ListName)
                .ForeignKey("dbo.IM_SPSiteMaster", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
            CreateTable(
                "dbo.IM_SPWebMaster",
                c => new
                    {
                        WebID = c.String(nullable: false, maxLength: 128),
                        SiteID = c.String(nullable: false, maxLength: 128),
                        WebURL = c.String(),
                        WebTitle = c.String(),
                        WebDesc = c.String(),
                    })
                .PrimaryKey(t => t.WebID)
                .ForeignKey("dbo.IM_SPSiteMaster", t => t.SiteID, cascadeDelete: true)
                .Index(t => t.SiteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.IM_SPWebMaster", "SiteID", "dbo.IM_SPSiteMaster");
            DropForeignKey("dbo.IM_SPListDetails", "SiteID", "dbo.IM_SPSiteMaster");
            DropForeignKey("dbo.IM_SPDocLibMaster", "SiteID", "dbo.IM_SPSiteMaster");
            DropForeignKey("dbo.IM_SPDocLibDetails", "SiteID", "dbo.IM_SPSiteMaster");
            DropIndex("dbo.IM_SPWebMaster", new[] { "SiteID" });
            DropIndex("dbo.IM_SPListDetails", new[] { "SiteID" });
            DropIndex("dbo.IM_SPDocLibMaster", new[] { "SiteID" });
            DropIndex("dbo.IM_SPDocLibDetails", new[] { "SiteID" });
            DropTable("dbo.IM_SPWebMaster");
            DropTable("dbo.IM_SPListDetails");
            DropTable("dbo.IM_JobStatus");
            DropTable("dbo.IM_SPDocLibMaster");
            DropTable("dbo.IM_SPSiteMaster");
            DropTable("dbo.IM_SPDocLibDetails");
        }
    }
}
