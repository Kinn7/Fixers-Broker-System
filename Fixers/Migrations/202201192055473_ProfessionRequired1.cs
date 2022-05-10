namespace Fixers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ProfessionRequired1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Profession", "name", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Profession", "name", c => c.String(maxLength: 50, unicode: false));
        }
    }
}
