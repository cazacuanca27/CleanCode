namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Profile1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetProfile1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        Mark = c.Int(nullable: false),
                        CNP = c.String(nullable: false, maxLength: 13),
                        Location = c.String(nullable: false, maxLength: 50),
                        Team = c.String(nullable: false, maxLength: 50),
                        TeamLeaderEmail = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AspNetProfile1");
        }
    }
}
