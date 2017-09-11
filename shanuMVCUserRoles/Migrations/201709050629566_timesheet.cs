namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class timesheet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetTimesheet1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Mark = c.Int(nullable: false),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        CNP = c.String(nullable: false, maxLength: 13),
                        TeamLeaderEmail = c.String(nullable: false),
                        Flag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AspNetTimesheet1");
        }
    }
}
