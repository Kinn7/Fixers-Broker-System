namespace Fixers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Employer : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employer", "FirstName", c => c.String(nullable: false, maxLength: 20, unicode: false));
            AlterColumn("dbo.Employer", "Password", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Employer", "Password", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("dbo.Employer", "FirstName", c => c.String(maxLength: 20, unicode: false));
        }
    }
}
