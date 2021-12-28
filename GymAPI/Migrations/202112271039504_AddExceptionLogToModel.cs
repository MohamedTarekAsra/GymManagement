namespace GymAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExceptionLogToModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExceptionLogs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Controller = c.String(),
                        Action = c.String(),
                        CreationDate = c.DateTime(nullable: false),
                        CreatedBy = c.Int(nullable: false),
                        Message = c.String(),
                        StackTrace = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ExceptionLogs");
        }
    }
}
