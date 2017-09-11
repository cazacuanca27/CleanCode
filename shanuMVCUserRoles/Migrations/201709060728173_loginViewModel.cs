namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class loginViewModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetHoliday1", "loginViewModel_UserName", c => c.String(nullable: false));
            AddColumn("dbo.AspNetHoliday1", "loginViewModel_Password", c => c.String(nullable: false));
            AddColumn("dbo.AspNetHoliday1", "loginViewModel_RememberMe", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetHoliday1", "loginViewModel_RememberMe");
            DropColumn("dbo.AspNetHoliday1", "loginViewModel_Password");
            DropColumn("dbo.AspNetHoliday1", "loginViewModel_UserName");
        }
    }
}
