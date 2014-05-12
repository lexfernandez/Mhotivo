namespace Mhotivo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TM : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AcademicYear",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Year = c.DateTime(nullable: false),
                        TeacherStartDate = c.DateTime(nullable: false),
                        TeacherEndDate = c.DateTime(nullable: false),
                        Schedule = c.DateTime(nullable: false),
                        Room = c.String(),
                        Approved = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        StudentsLimit = c.Int(nullable: false),
                        StudentsCount = c.Int(nullable: false),
                        Course_CourseId = c.Int(),
                        Grade_GradeId = c.Int(),
                        Teacher_PeopleId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.Course_CourseId)
                .ForeignKey("dbo.Grade", t => t.Grade_GradeId)
                .ForeignKey("dbo.Meister", t => t.Teacher_PeopleId)
                .Index(t => t.Course_CourseId)
                .Index(t => t.Grade_GradeId)
                .Index(t => t.Teacher_PeopleId);
            
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Area_Id = c.Int(),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Area", t => t.Area_Id)
                .Index(t => t.Area_Id);
            
            CreateTable(
                "dbo.Area",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Grade",
                c => new
                    {
                        GradeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        EducationLevel = c.String(),
                    })
                .PrimaryKey(t => t.GradeId);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        PeopleId = c.Long(nullable: false, identity: true),
                        IDNumber = c.String(),
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
                    })
                .PrimaryKey(t => t.PeopleId);
            
            CreateTable(
                "dbo.ContactInformation",
                c => new
                    {
                        ContactId = c.Int(nullable: false, identity: true),
                        Type = c.String(),
                        Value = c.String(),
                        People_PeopleId = c.Long(),
                    })
                .PrimaryKey(t => t.ContactId)
                .ForeignKey("dbo.People", t => t.People_PeopleId)
                .Index(t => t.People_PeopleId);
            
            CreateTable(
                "dbo.AppointmentDiaries",
                c => new
                    {
                        AppointmentDiaryId = c.Int(nullable: false, identity: true),
                        Title = c.String(),
                        SomeImportantKey = c.Int(nullable: false),
                        DateTimeScheduled = c.DateTime(nullable: false),
                        AppointmentLength = c.Int(nullable: false),
                        StatusENUM = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentDiaryId);
            
            CreateTable(
                "dbo.ClassActivity",
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
                .ForeignKey("dbo.AcademicYear", t => t.AcademicYear_Id)
                .Index(t => t.AcademicYear_Id);
            
            CreateTable(
                "dbo.ClassActivityGrading",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Score = c.Double(nullable: false),
                        Percentage = c.Double(nullable: false),
                        Comments = c.String(),
                        ClassActivity_Id = c.Int(),
                        Student_PeopleId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassActivity", t => t.ClassActivity_Id)
                .ForeignKey("dbo.Student", t => t.Student_PeopleId)
                .Index(t => t.ClassActivity_Id)
                .Index(t => t.Student_PeopleId);
            
            CreateTable(
                "dbo.Enroll",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AcademicYear_Id = c.Int(),
                        Student_PeopleId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AcademicYear", t => t.AcademicYear_Id)
                .ForeignKey("dbo.Student", t => t.Student_PeopleId)
                .Index(t => t.AcademicYear_Id)
                .Index(t => t.Student_PeopleId);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventId = c.Int(nullable: false, identity: true),
                        CreationDate = c.DateTime(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Description = c.String(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventId);
            
            CreateTable(
                "dbo.Groups",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        DisplayName = c.String(),
                        Password = c.String(),
                        Status = c.Boolean(nullable: false),
                        Role_RoleId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Role", t => t.Role_RoleId)
                .Index(t => t.Role_RoleId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
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
                "dbo.Pensum",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Course_CourseId = c.Int(),
                        Grade_GradeId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Course", t => t.Course_CourseId)
                .ForeignKey("dbo.Grade", t => t.Grade_GradeId)
                .Index(t => t.Course_CourseId)
                .Index(t => t.Grade_GradeId);
            
            CreateTable(
                "dbo.UserGroups",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Group_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Group_Id })
                .ForeignKey("dbo.User", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Groups", t => t.Group_Id, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Group_Id);
            
            CreateTable(
                "dbo.Benefactor",
                c => new
                    {
                        PeopleId = c.Long(nullable: false),
                        Capacity = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PeopleId)
                .ForeignKey("dbo.People", t => t.PeopleId)
                .Index(t => t.PeopleId);
            
            CreateTable(
                "dbo.Meister",
                c => new
                    {
                        PeopleId = c.Long(nullable: false),
                        Biography = c.String(),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.PeopleId)
                .ForeignKey("dbo.People", t => t.PeopleId)
                .Index(t => t.PeopleId);
            
            CreateTable(
                "dbo.Parent",
                c => new
                    {
                        PeopleId = c.Long(nullable: false),
                        JustARandomColumn = c.String(),
                    })
                .PrimaryKey(t => t.PeopleId)
                .ForeignKey("dbo.People", t => t.PeopleId)
                .Index(t => t.PeopleId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        PeopleId = c.Long(nullable: false),
                        Benefactor_PeopleId = c.Long(),
                        Tutor1_PeopleId = c.Long(),
                        Tutor2_PeopleId = c.Long(),
                        StartDate = c.DateTime(nullable: false),
                        BloodType = c.String(),
                        AccountNumber = c.String(),
                        Biography = c.String(),
                    })
                .PrimaryKey(t => t.PeopleId)
                .ForeignKey("dbo.People", t => t.PeopleId)
                .ForeignKey("dbo.Benefactor", t => t.Benefactor_PeopleId)
                .ForeignKey("dbo.Parent", t => t.Tutor1_PeopleId)
                .ForeignKey("dbo.Parent", t => t.Tutor2_PeopleId)
                .Index(t => t.PeopleId)
                .Index(t => t.Benefactor_PeopleId)
                .Index(t => t.Tutor1_PeopleId)
                .Index(t => t.Tutor2_PeopleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "Tutor2_PeopleId", "dbo.Parent");
            DropForeignKey("dbo.Student", "Tutor1_PeopleId", "dbo.Parent");
            DropForeignKey("dbo.Student", "Benefactor_PeopleId", "dbo.Benefactor");
            DropForeignKey("dbo.Student", "PeopleId", "dbo.People");
            DropForeignKey("dbo.Parent", "PeopleId", "dbo.People");
            DropForeignKey("dbo.Meister", "PeopleId", "dbo.People");
            DropForeignKey("dbo.Benefactor", "PeopleId", "dbo.People");
            DropForeignKey("dbo.Pensum", "Grade_GradeId", "dbo.Grade");
            DropForeignKey("dbo.Pensum", "Course_CourseId", "dbo.Course");
            DropForeignKey("dbo.User", "Role_RoleId", "dbo.Role");
            DropForeignKey("dbo.UserGroups", "Group_Id", "dbo.Groups");
            DropForeignKey("dbo.UserGroups", "User_UserId", "dbo.User");
            DropForeignKey("dbo.Enroll", "Student_PeopleId", "dbo.Student");
            DropForeignKey("dbo.Enroll", "AcademicYear_Id", "dbo.AcademicYear");
            DropForeignKey("dbo.ClassActivityGrading", "Student_PeopleId", "dbo.Student");
            DropForeignKey("dbo.ClassActivityGrading", "ClassActivity_Id", "dbo.ClassActivity");
            DropForeignKey("dbo.ClassActivity", "AcademicYear_Id", "dbo.AcademicYear");
            DropForeignKey("dbo.AcademicYear", "Teacher_PeopleId", "dbo.Meister");
            DropForeignKey("dbo.ContactInformation", "People_PeopleId", "dbo.People");
            DropForeignKey("dbo.AcademicYear", "Grade_GradeId", "dbo.Grade");
            DropForeignKey("dbo.AcademicYear", "Course_CourseId", "dbo.Course");
            DropForeignKey("dbo.Course", "Area_Id", "dbo.Area");
            DropIndex("dbo.Student", new[] { "Tutor2_PeopleId" });
            DropIndex("dbo.Student", new[] { "Tutor1_PeopleId" });
            DropIndex("dbo.Student", new[] { "Benefactor_PeopleId" });
            DropIndex("dbo.Student", new[] { "PeopleId" });
            DropIndex("dbo.Parent", new[] { "PeopleId" });
            DropIndex("dbo.Meister", new[] { "PeopleId" });
            DropIndex("dbo.Benefactor", new[] { "PeopleId" });
            DropIndex("dbo.UserGroups", new[] { "Group_Id" });
            DropIndex("dbo.UserGroups", new[] { "User_UserId" });
            DropIndex("dbo.Pensum", new[] { "Grade_GradeId" });
            DropIndex("dbo.Pensum", new[] { "Course_CourseId" });
            DropIndex("dbo.User", new[] { "Role_RoleId" });
            DropIndex("dbo.Enroll", new[] { "Student_PeopleId" });
            DropIndex("dbo.Enroll", new[] { "AcademicYear_Id" });
            DropIndex("dbo.ClassActivityGrading", new[] { "Student_PeopleId" });
            DropIndex("dbo.ClassActivityGrading", new[] { "ClassActivity_Id" });
            DropIndex("dbo.ClassActivity", new[] { "AcademicYear_Id" });
            DropIndex("dbo.ContactInformation", new[] { "People_PeopleId" });
            DropIndex("dbo.Course", new[] { "Area_Id" });
            DropIndex("dbo.AcademicYear", new[] { "Teacher_PeopleId" });
            DropIndex("dbo.AcademicYear", new[] { "Grade_GradeId" });
            DropIndex("dbo.AcademicYear", new[] { "Course_CourseId" });
            DropTable("dbo.Student");
            DropTable("dbo.Parent");
            DropTable("dbo.Meister");
            DropTable("dbo.Benefactor");
            DropTable("dbo.UserGroups");
            DropTable("dbo.Pensum");
            DropTable("dbo.Notifications");
            DropTable("dbo.Role");
            DropTable("dbo.User");
            DropTable("dbo.Groups");
            DropTable("dbo.Event");
            DropTable("dbo.Enroll");
            DropTable("dbo.ClassActivityGrading");
            DropTable("dbo.ClassActivity");
            DropTable("dbo.AppointmentDiaries");
            DropTable("dbo.ContactInformation");
            DropTable("dbo.People");
            DropTable("dbo.Grade");
            DropTable("dbo.Area");
            DropTable("dbo.Course");
            DropTable("dbo.AcademicYear");
        }
    }
}
