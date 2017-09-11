namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeRequiredFirstNameHoliday : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetHoliday1", "FirstName", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetHoliday1", "FirstName", c => c.String(nullable: false, maxLength: 50));
        }
    }
}
