namespace Fixers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnnotaionsAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Professional", "ConfirmPassword", c => c.String());
            AlterColumn("dbo.Professional", "FirstName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Professional", "LastName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Professional", "Balance", c => c.Double(nullable: false));
            AlterColumn("dbo.Professional", "Password", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Professional", "Password", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("dbo.Professional", "Balance", c => c.Double());
            AlterColumn("dbo.Professional", "LastName", c => c.String(maxLength: 50, unicode: false));
            AlterColumn("dbo.Professional", "FirstName", c => c.String(maxLength: 50, unicode: false));
            DropColumn("dbo.Professional", "ConfirmPassword");
        }
    }
}
