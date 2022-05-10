namespace Fixers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserNameAndPasswordRequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Clerk", "UserName", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AlterColumn("dbo.Clerk", "password", c => c.String(nullable: false, maxLength: 150, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Clerk", "password", c => c.String(maxLength: 150, unicode: false));
            AlterColumn("dbo.Clerk", "UserName", c => c.String(maxLength: 50, unicode: false));
        }
    }
}
