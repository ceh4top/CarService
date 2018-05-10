namespace CarService.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class M1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CarBrands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarBrands", t => t.BrandId, cascadeDelete: true)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StateNumber = c.String(),
                        ModelId = c.Int(nullable: false),
                        YearOfManufacture = c.DateTime(nullable: false),
                        TransmissionTypeId = c.Int(nullable: false),
                        EnginePower = c.Int(nullable: false),
                        CarOwnerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CarOwner", t => t.CarOwnerId)
                .ForeignKey("dbo.CarModels", t => t.ModelId, cascadeDelete: true)
                .ForeignKey("dbo.CarTransmissionTypes", t => t.TransmissionTypeId, cascadeDelete: true)
                .Index(t => t.ModelId)
                .Index(t => t.TransmissionTypeId)
                .Index(t => t.CarOwnerId);
            
            CreateTable(
                "dbo.Humen",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        MiddleName = c.String(),
                        YearOfBirth = c.DateTime(nullable: false),
                        PhoneNumber = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cars", t => t.CarId, cascadeDelete: true)
                .Index(t => t.CarId);
            
            CreateTable(
                "dbo.OrderWorks",
                c => new
                    {
                        OrderId = c.Int(nullable: false),
                        WorkId = c.Int(nullable: false),
                        Start = c.DateTime(),
                        End = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.OrderId, t.WorkId })
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .ForeignKey("dbo.Works", t => t.WorkId, cascadeDelete: true)
                .Index(t => t.OrderId)
                .Index(t => t.WorkId);
            
            CreateTable(
                "dbo.Works",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarTransmissionTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CarOwner",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Humen", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CarOwner", "Id", "dbo.Humen");
            DropForeignKey("dbo.Cars", "TransmissionTypeId", "dbo.CarTransmissionTypes");
            DropForeignKey("dbo.OrderWorks", "WorkId", "dbo.Works");
            DropForeignKey("dbo.OrderWorks", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "CarId", "dbo.Cars");
            DropForeignKey("dbo.Cars", "ModelId", "dbo.CarModels");
            DropForeignKey("dbo.Cars", "CarOwnerId", "dbo.CarOwner");
            DropForeignKey("dbo.CarModels", "BrandId", "dbo.CarBrands");
            DropIndex("dbo.CarOwner", new[] { "Id" });
            DropIndex("dbo.OrderWorks", new[] { "WorkId" });
            DropIndex("dbo.OrderWorks", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "CarId" });
            DropIndex("dbo.Cars", new[] { "CarOwnerId" });
            DropIndex("dbo.Cars", new[] { "TransmissionTypeId" });
            DropIndex("dbo.Cars", new[] { "ModelId" });
            DropIndex("dbo.CarModels", new[] { "BrandId" });
            DropTable("dbo.CarOwner");
            DropTable("dbo.CarTransmissionTypes");
            DropTable("dbo.Works");
            DropTable("dbo.OrderWorks");
            DropTable("dbo.Orders");
            DropTable("dbo.Humen");
            DropTable("dbo.Cars");
            DropTable("dbo.CarModels");
            DropTable("dbo.CarBrands");
        }
    }
}
