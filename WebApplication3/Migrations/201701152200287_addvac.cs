namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvac : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Vacation",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartsAt = c.DateTime(nullable: false),
                        EndsAt = c.DateTime(nullable: false),
                        Description = c.String(maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Vacation");
        }
    }
}
