namespace CarService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Works", "Order_Id", c => c.Int());
            CreateIndex("dbo.Works", "Order_Id");
            AddForeignKey("dbo.Works", "Order_Id", "dbo.Orders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Works", "Order_Id", "dbo.Orders");
            DropIndex("dbo.Works", new[] { "Order_Id" });
            DropColumn("dbo.Works", "Order_Id");
        }
    }
}
