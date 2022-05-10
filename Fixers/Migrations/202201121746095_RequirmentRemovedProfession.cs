namespace Fixers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequirmentRemovedProfession : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Professional", "ProfessionID", "dbo.Profession");
            DropIndex("dbo.Professional", new[] { "ProfessionID" });
            AlterColumn("dbo.Professional", "ProfessionID", c => c.Int());
            CreateIndex("dbo.Professional", "ProfessionID");
            AddForeignKey("dbo.Professional", "ProfessionID", "dbo.Profession", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Professional", "ProfessionID", "dbo.Profession");
            DropIndex("dbo.Professional", new[] { "ProfessionID" });
            AlterColumn("dbo.Professional", "ProfessionID", c => c.Int(nullable: false));
            CreateIndex("dbo.Professional", "ProfessionID");
            AddForeignKey("dbo.Professional", "ProfessionID", "dbo.Profession", "id", cascadeDelete: true);
        }
    }
}
