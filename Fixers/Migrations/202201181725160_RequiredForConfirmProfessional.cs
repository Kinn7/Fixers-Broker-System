namespace Fixers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredForConfirmProfessional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Professional", "ConfirmPassword", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Professional", "ConfirmPassword", c => c.String());
        }
    }
}
