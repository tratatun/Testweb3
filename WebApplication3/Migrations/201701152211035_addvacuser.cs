namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addvacuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Vacation", "UserId_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Vacation", "UserId_Id");
            AddForeignKey("dbo.Vacation", "UserId_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            DropColumn("dbo.Vacation", "UserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Vacation", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Vacation", "UserId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Vacation", new[] { "UserId_Id" });
            DropColumn("dbo.Vacation", "UserId_Id");
        }
    }
}
