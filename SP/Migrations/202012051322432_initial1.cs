namespace SP.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial1 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.IM_SPDocLibDetails");
            AlterColumn("dbo.IM_SPDocLibDetails", "DocLibID", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.IM_SPDocLibDetails", "DocName", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.IM_SPDocLibDetails", new[] { "DocLibID", "DocName" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.IM_SPDocLibDetails");
            AlterColumn("dbo.IM_SPDocLibDetails", "DocName", c => c.String());
            AlterColumn("dbo.IM_SPDocLibDetails", "DocLibID", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.IM_SPDocLibDetails", "DocLibID");
        }
    }
}
