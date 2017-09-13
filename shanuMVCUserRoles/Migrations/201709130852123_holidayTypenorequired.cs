namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class holidayTypenorequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetHoliday1", "HolidayType", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetHoliday1", "HolidayType", c => c.String(nullable: false));
        }
    }
}
