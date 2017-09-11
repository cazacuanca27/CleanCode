namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeLogin : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetHoliday1", "loginViewModel_UserName");
            DropColumn("dbo.AspNetHoliday1", "loginViewModel_Password");
            DropColumn("dbo.AspNetHoliday1", "loginViewModel_RememberMe");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetHoliday1", "loginViewModel_RememberMe", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetHoliday1", "loginViewModel_Password", c => c.String(nullable: false));
            AddColumn("dbo.AspNetHoliday1", "loginViewModel_UserName", c => c.String(nullable: false));
        }
    }
}
