namespace WebApplication3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class vacatFK : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Vacation", "UserId_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Vacation", new[] { "UserId_Id" });
            RenameColumn(table: "dbo.Vacation", name: "UserId_Id", newName: "UserId");
            AlterColumn("dbo.Vacation", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Vacation", "UserId");
            AddForeignKey("dbo.Vacation", "UserId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Vacation", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Vacation", new[] { "UserId" });
            AlterColumn("dbo.Vacation", "UserId", c => c.String(nullable: false, maxLength: 128));
            RenameColumn(table: "dbo.Vacation", name: "UserId", newName: "UserId_Id");
            CreateIndex("dbo.Vacation", "UserId_Id");
            AddForeignKey("dbo.Vacation", "UserId_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
