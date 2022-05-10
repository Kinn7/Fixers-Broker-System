namespace Fixers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Address",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        kebele_id = c.String(maxLength: 100, unicode: false),
                        sub_city = c.String(maxLength: 15, unicode: false),
                        woreda = c.Int(),
                        house_no = c.String(maxLength: 10, unicode: false),
                        phone_no = c.String(maxLength: 20, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Employer",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 20, unicode: false),
                        LastName = c.String(maxLength: 20, unicode: false),
                        Password = c.String(maxLength: 150, unicode: false),
                        AddressID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Address", t => t.AddressID)
                .Index(t => t.AddressID);
            
            CreateTable(
                "dbo.HireStatus",
                c => new
                    {
                        ProfessionalID = c.Int(nullable: false),
                        EmployerID = c.Int(nullable: false),
                        date = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.ProfessionalID, t.EmployerID })
                .ForeignKey("dbo.Professional", t => t.ProfessionalID)
                .ForeignKey("dbo.Employer", t => t.EmployerID)
                .Index(t => t.ProfessionalID)
                .Index(t => t.EmployerID);
            
            CreateTable(
                "dbo.Professional",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50, unicode: false),
                        LastName = c.String(maxLength: 50, unicode: false),
                        Balance = c.Double(),
                        Status = c.Int(),
                        Fee = c.Double(),
                        Password = c.String(maxLength: 150, unicode: false),
                        Picture = c.Binary(),
                        AddressID = c.Int(),
                        ProfessionID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Address", t => t.AddressID)
                .ForeignKey("dbo.Profession", t => t.ProfessionID)
                .Index(t => t.AddressID)
                .Index(t => t.ProfessionID);
            
            CreateTable(
                "dbo.Profession",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Clerk",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        UserName = c.String(maxLength: 50, unicode: false),
                        password = c.String(maxLength: 150, unicode: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HireStatus", "EmployerID", "dbo.Employer");
            DropForeignKey("dbo.Professional", "ProfessionID", "dbo.Profession");
            DropForeignKey("dbo.HireStatus", "ProfessionalID", "dbo.Professional");
            DropForeignKey("dbo.Professional", "AddressID", "dbo.Address");
            DropForeignKey("dbo.Employer", "AddressID", "dbo.Address");
            DropIndex("dbo.Professional", new[] { "ProfessionID" });
            DropIndex("dbo.Professional", new[] { "AddressID" });
            DropIndex("dbo.HireStatus", new[] { "EmployerID" });
            DropIndex("dbo.HireStatus", new[] { "ProfessionalID" });
            DropIndex("dbo.Employer", new[] { "AddressID" });
            DropTable("dbo.Clerk");
            DropTable("dbo.Profession");
            DropTable("dbo.Professional");
            DropTable("dbo.HireStatus");
            DropTable("dbo.Employer");
            DropTable("dbo.Address");
        }
    }
}
