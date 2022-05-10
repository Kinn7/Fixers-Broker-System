namespace Fixers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfessionalStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Professional", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Professional", "Status", c => c.Int());
        }
    }
}
