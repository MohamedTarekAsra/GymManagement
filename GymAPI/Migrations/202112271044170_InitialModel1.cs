namespace GymAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "CreatedBy_ID", "dbo.Users");
            DropForeignKey("dbo.Users", "TenantId", "dbo.Tenants");
            DropIndex("dbo.Users", new[] { "TenantId" });
            DropIndex("dbo.Users", new[] { "CreatedBy_ID" });
            AddColumn("dbo.Users", "CreatedBy", c => c.Int());
            AddColumn("dbo.Users", "LastChangedBy", c => c.Int());
            AlterColumn("dbo.Tenants", "Gender", c => c.Int());
            AlterColumn("dbo.Users", "Gender", c => c.Int());
            AlterColumn("dbo.Users", "GymId", c => c.Int());
            DropColumn("dbo.Users", "CreatedBy_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "CreatedBy_ID", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "GymId", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Gender", c => c.Int(nullable: false));
            AlterColumn("dbo.Tenants", "Gender", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "LastChangedBy");
            DropColumn("dbo.Users", "CreatedBy");
            CreateIndex("dbo.Users", "CreatedBy_ID");
            CreateIndex("dbo.Users", "TenantId");
            AddForeignKey("dbo.Users", "TenantId", "dbo.Tenants", "ID");
            AddForeignKey("dbo.Users", "CreatedBy_ID", "dbo.Users", "ID");
        }
    }
}
