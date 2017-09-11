namespace shanuMVCUserRoles.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ooh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetOOHRequest1",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(nullable: false, maxLength: 50),
                        Day = c.DateTime(nullable: false),
                        Hours = c.Double(nullable: false),
                        TicketNUmber = c.String(nullable: false, maxLength: 50),
                        TeamLeaderEmail = c.String(nullable: false),
                        Flag = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AspNetOOHRequest1");
        }
    }
}
