namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class alterOOH : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetOOHRequest1", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetOOHRequest1", "Email");
        }
    }
}
