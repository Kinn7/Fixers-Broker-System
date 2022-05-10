namespace Fixers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailAddedEmployer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Employer", "Email", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Employer", "Email");
        }
    }
}
