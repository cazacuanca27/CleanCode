namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nochanges : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetHoliday1", "HolidayType");
            DropColumn("dbo.AspNetHoliday1", "SickLeaveIndex");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetHoliday1", "SickLeaveIndex", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetHoliday1", "HolidayType", c => c.String());
        }
    }
}
