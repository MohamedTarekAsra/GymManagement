namespace GymAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tenants",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(maxLength: 50),
                        LastName = c.String(maxLength: 50),
                        UserName = c.String(maxLength: 50),
                        Password = c.String(maxLength: 255),
                        UserType = c.Int(),
                        LastLoginDate = c.DateTime(),
                        ChangePassowrdOnLogin = c.Boolean(),
                        PasswordExpiryDate = c.DateTime(),
                        Email = c.String(maxLength: 255),
                        MobileNumber = c.String(maxLength: 50),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        LastChangedBy = c.Int(),
                        LastChangedDate = c.DateTime(),
                        ProfileImage = c.String(),
                        Gender = c.Int(nullable: false),
                        GymId = c.Int(nullable: false),
                        Address = c.String(maxLength: 255),
                        VerificationCode = c.String(maxLength: 255),
                        Token = c.String(),
                        VerificationStartDate = c.DateTime(),
                        IsMobileVerified = c.Boolean(),
                        UserNameFilter = c.String(),
                        SubscribeStartDate = c.DateTime(),
                        SubscribeEndDate = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        UserName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(maxLength: 255),
                        UserType = c.Int(),
                        LastLoginDate = c.DateTime(),
                        ChangePassowrdOnLogin = c.Boolean(),
                        PasswordExpiryDate = c.DateTime(),
                        Email = c.String(nullable: false, maxLength: 255),
                        MobileNumber = c.String(nullable: false, maxLength: 50),
                        CreatedDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        LastChangedDate = c.DateTime(),
                        ProfileImage = c.String(),
                        Gender = c.Int(nullable: false),
                        GymId = c.Int(nullable: false),
                        Address = c.String(maxLength: 255),
                        VerificationCode = c.String(maxLength: 255),
                        Token = c.String(),
                        VerificationStartDate = c.DateTime(),
                        IsMobileVerified = c.Boolean(),
                        UserNameFilter = c.String(),
                        TenantId = c.Int(),
                        CreatedBy_ID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Users", t => t.CreatedBy_ID)
                .ForeignKey("dbo.Tenants", t => t.TenantId)
                .Index(t => t.TenantId)
                .Index(t => t.CreatedBy_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "TenantId", "dbo.Tenants");
            DropForeignKey("dbo.Users", "CreatedBy_ID", "dbo.Users");
            DropIndex("dbo.Users", new[] { "CreatedBy_ID" });
            DropIndex("dbo.Users", new[] { "TenantId" });
            DropTable("dbo.Users");
            DropTable("dbo.Tenants");
        }
    }
}
