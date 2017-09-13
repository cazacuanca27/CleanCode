namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sli : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetHoliday1", "SickLeaveIndex", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetHoliday1", "SickLeaveIndex");
        }
    }
}
