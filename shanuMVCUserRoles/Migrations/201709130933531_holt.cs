namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class holt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetHoliday1", "HolidayType", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetHoliday1", "HolidayType");
        }
    }
}
