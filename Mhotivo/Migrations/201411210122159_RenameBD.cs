namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameBD : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Events", "Creator_Id", "dbo.Users");
            DropIndex("dbo.Events", new[] { "Creator_Id" });
            AddColumn("dbo.AppointmentDiaries", "IsAproveed", c => c.Boolean(nullable: false));
            AlterColumn("dbo.People", "StartDate", c => c.String());
            AlterColumn("dbo.People", "EndDate", c => c.String());
            AlterColumn("dbo.Notifications", "EventName", c => c.String());
            AlterColumn("dbo.Notifications", "From", c => c.String());
            AlterColumn("dbo.Notifications", "To", c => c.String());
            AlterColumn("dbo.Notifications", "WithCopyTo", c => c.String());
            AlterColumn("dbo.Notifications", "WithHiddenCopyTo", c => c.String());
            DropColumn("dbo.AppointmentDiaries", "IsActive");
            DropTable("dbo.Events");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Events",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                        Creator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AppointmentDiaries", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.Notifications", "WithHiddenCopyTo", c => c.String(nullable: false));
            AlterColumn("dbo.Notifications", "WithCopyTo", c => c.String(nullable: false));
            AlterColumn("dbo.Notifications", "To", c => c.String(nullable: false));
            AlterColumn("dbo.Notifications", "From", c => c.String(nullable: false));
            AlterColumn("dbo.Notifications", "EventName", c => c.String(nullable: false));
            AlterColumn("dbo.People", "EndDate", c => c.DateTime());
            AlterColumn("dbo.People", "StartDate", c => c.DateTime());
            DropColumn("dbo.AppointmentDiaries", "IsAproveed");
            CreateIndex("dbo.Events", "Creator_Id");
            AddForeignKey("dbo.Events", "Creator_Id", "dbo.Users", "Id");
        }
    }
}
