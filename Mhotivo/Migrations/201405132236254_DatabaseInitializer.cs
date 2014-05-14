namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatabaseInitializer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicYears",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.DateTime(nullable: false),
                        TeacherStartDate = c.DateTime(),
                        TeacherEndDate = c.DateTime(),
                        Schedule = c.DateTime(),
                        Room = c.String(),
                        Approved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        StudentsLimit = c.Int(nullable: false),
                        StudentsCount = c.Int(nullable: false),
                        Course_Id = c.Int(),
                        Grade_Id = c.Int(),
                        Teacher_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Grades", t => t.Grade_Id)
                .ForeignKey("dbo.People", t => t.Teacher_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Grade_Id)
                .Index(t => t.Teacher_Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Area_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.Area_Id)
                .Index(t => t.Area_Id);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Grades",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EducationLevel = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        IdNumber = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        FullName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        Nationality = c.String(),
                        City = c.String(),
                        State = c.String(),
                        Country = c.String(),
                        Address = c.String(),
                        UrlPicture = c.String(),
                        Gender = c.Boolean(nullable: false),
                        Biography = c.String(),
                        StartDate = c.DateTime(),
                        EndDate = c.DateTime(),
                        Capacity = c.Int(),
                        StartDate1 = c.DateTime(),
                        BloodType = c.String(),
                        AccountNumber = c.String(),
                        Biography1 = c.String(),
                        JustARandomColumn = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Benefactor_Id = c.Long(),
                        Tutor1_Id = c.Long(),
                        Tutor2_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Benefactor_Id)
                .ForeignKey("dbo.People", t => t.Tutor1_Id)
                .ForeignKey("dbo.People", t => t.Tutor2_Id)
                .Index(t => t.Benefactor_Id)
                .Index(t => t.Tutor1_Id)
                .Index(t => t.Tutor2_Id);
            
            CreateTable(
                "dbo.ContactInformations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Value = c.String(),
                        People_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.People_Id)
                .Index(t => t.People_Id);
            
            CreateTable(
                "dbo.AppointmentDiaries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        DateTimeScheduled = c.DateTime(nullable: false),
                        AppointmentLength = c.Int(nullable: false),
                        StatusEnum = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        Creator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Creator_Id)
                .Index(t => t.Creator_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        DisplayName = c.String(),
                        Password = c.String(),
                        Status = c.Boolean(nullable: false),
                        Role_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .Index(t => t.Role_Id);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClassActivities",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Type = c.String(),
                        Description = c.String(),
                        Value = c.Double(nullable: false),
                        AcademicYear_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYear_Id)
                .Index(t => t.AcademicYear_Id);
            
            CreateTable(
                "dbo.ClassActivityGradings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Double(nullable: false),
                        Percentage = c.Double(nullable: false),
                        Comments = c.String(),
                        ClassActivity_Id = c.Int(),
                        Student_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassActivities", t => t.ClassActivity_Id)
                .ForeignKey("dbo.People", t => t.Student_Id)
                .Index(t => t.ClassActivity_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Enrolls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AcademicYear_Id = c.Int(),
                        Student_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYears", t => t.AcademicYear_Id)
                .ForeignKey("dbo.People", t => t.Student_Id)
                .Index(t => t.AcademicYear_Id)
                .Index(t => t.Student_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Creator_Id)
                .Index(t => t.Creator_Id);
            
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        EventName = c.String(nullable: false),
                        From = c.String(nullable: false),
                        To = c.String(nullable: false),
                        WithCopyTo = c.String(nullable: false),
                        WithHiddenCopyTo = c.String(nullable: false),
                        Subject = c.String(),
                        Message = c.String(),
                        Created = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Pensums",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Course_Id = c.Int(),
                        Grade_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .ForeignKey("dbo.Grades", t => t.Grade_Id)
                .Index(t => t.Course_Id)
                .Index(t => t.Grade_Id);
            
            CreateTable(
                "dbo.GroupUsers",
                c => new
                    {
                        Group_Id = c.Long(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Group_Id, t.User_Id })
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Group_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pensums", "Grade_Id", "dbo.Grades");
            DropForeignKey("dbo.Pensums", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Events", "Creator_Id", "dbo.Users");
            DropForeignKey("dbo.Enrolls", "Student_Id", "dbo.People");
            DropForeignKey("dbo.Enrolls", "AcademicYear_Id", "dbo.AcademicYears");
            DropForeignKey("dbo.ClassActivityGradings", "Student_Id", "dbo.People");
            DropForeignKey("dbo.ClassActivityGradings", "ClassActivity_Id", "dbo.ClassActivities");
            DropForeignKey("dbo.ClassActivities", "AcademicYear_Id", "dbo.AcademicYears");
            DropForeignKey("dbo.AppointmentDiaries", "Creator_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.GroupUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.GroupUsers", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.AcademicYears", "Teacher_Id", "dbo.People");
            DropForeignKey("dbo.People", "Tutor2_Id", "dbo.People");
            DropForeignKey("dbo.People", "Tutor1_Id", "dbo.People");
            DropForeignKey("dbo.People", "Benefactor_Id", "dbo.People");
            DropForeignKey("dbo.ContactInformations", "People_Id", "dbo.People");
            DropForeignKey("dbo.AcademicYears", "Grade_Id", "dbo.Grades");
            DropForeignKey("dbo.AcademicYears", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Area_Id", "dbo.Areas");
            DropIndex("dbo.GroupUsers", new[] { "User_Id" });
            DropIndex("dbo.GroupUsers", new[] { "Group_Id" });
            DropIndex("dbo.Pensums", new[] { "Grade_Id" });
            DropIndex("dbo.Pensums", new[] { "Course_Id" });
            DropIndex("dbo.Events", new[] { "Creator_Id" });
            DropIndex("dbo.Enrolls", new[] { "Student_Id" });
            DropIndex("dbo.Enrolls", new[] { "AcademicYear_Id" });
            DropIndex("dbo.ClassActivityGradings", new[] { "Student_Id" });
            DropIndex("dbo.ClassActivityGradings", new[] { "ClassActivity_Id" });
            DropIndex("dbo.ClassActivities", new[] { "AcademicYear_Id" });
            DropIndex("dbo.Users", new[] { "Role_Id" });
            DropIndex("dbo.AppointmentDiaries", new[] { "Creator_Id" });
            DropIndex("dbo.ContactInformations", new[] { "People_Id" });
            DropIndex("dbo.People", new[] { "Tutor2_Id" });
            DropIndex("dbo.People", new[] { "Tutor1_Id" });
            DropIndex("dbo.People", new[] { "Benefactor_Id" });
            DropIndex("dbo.Courses", new[] { "Area_Id" });
            DropIndex("dbo.AcademicYears", new[] { "Teacher_Id" });
            DropIndex("dbo.AcademicYears", new[] { "Grade_Id" });
            DropIndex("dbo.AcademicYears", new[] { "Course_Id" });
            DropTable("dbo.GroupUsers");
            DropTable("dbo.Pensums");
            DropTable("dbo.Notifications");
            DropTable("dbo.Events");
            DropTable("dbo.Enrolls");
            DropTable("dbo.ClassActivityGradings");
            DropTable("dbo.ClassActivities");
            DropTable("dbo.Roles");
            DropTable("dbo.Groups");
            DropTable("dbo.Users");
            DropTable("dbo.AppointmentDiaries");
            DropTable("dbo.ContactInformations");
            DropTable("dbo.People");
            DropTable("dbo.Grades");
            DropTable("dbo.Areas");
            DropTable("dbo.Courses");
            DropTable("dbo.AcademicYears");
        }
    }
}
