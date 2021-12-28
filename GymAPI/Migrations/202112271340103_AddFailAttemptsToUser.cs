namespace GymAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFailAttemptsToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "LoginFailedAttempts", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Users", "LastLoginFailedDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "LastLoginFailedDate");
            DropColumn("dbo.Users", "LoginFailedAttempts");
        }
    }
}
