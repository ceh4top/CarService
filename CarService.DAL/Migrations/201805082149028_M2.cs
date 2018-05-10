namespace CarService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M2 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CarOwner", newName: "CarOwners");
            RenameTable(name: "dbo.Humen", newName: "Humans");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Humans", newName: "Humen");
            RenameTable(name: "dbo.CarOwners", newName: "CarOwner");
        }
    }
}
