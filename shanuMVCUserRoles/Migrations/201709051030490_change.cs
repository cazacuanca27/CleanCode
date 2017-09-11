namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetProfile1", "Email", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetProfile1", "Email", c => c.String(nullable: false));
        }
    }
}
