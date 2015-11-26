namespace MySens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationshipstudentcourses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "AppUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Course_CourseID", "dbo.Courses");
            DropIndex("dbo.Courses", new[] { "AppUser_Id" });
            DropIndex("dbo.Courses", new[] { "AppUser_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "Course_CourseID" });
            CreateTable(
                "dbo.StudentEnrollments",
                c => new
                    {
                        StudentId = c.String(nullable: false, maxLength: 128),
                        CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.StudentId, t.CourseId })
                .ForeignKey("dbo.AspNetUsers", t => t.StudentId, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.CourseId, cascadeDelete: true)
                .Index(t => t.StudentId)
                .Index(t => t.CourseId);
            
            DropColumn("dbo.Courses", "AppUser_Id");
            DropColumn("dbo.Courses", "AppUser_Id1");
            DropColumn("dbo.AspNetUsers", "Course_CourseID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Course_CourseID", c => c.Int());
            AddColumn("dbo.Courses", "AppUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Courses", "AppUser_Id", c => c.String(maxLength: 128));
            DropForeignKey("dbo.StudentEnrollments", "CourseId", "dbo.Courses");
            DropForeignKey("dbo.StudentEnrollments", "StudentId", "dbo.AspNetUsers");
            DropIndex("dbo.StudentEnrollments", new[] { "CourseId" });
            DropIndex("dbo.StudentEnrollments", new[] { "StudentId" });
            DropTable("dbo.StudentEnrollments");
            CreateIndex("dbo.AspNetUsers", "Course_CourseID");
            CreateIndex("dbo.Courses", "AppUser_Id1");
            CreateIndex("dbo.Courses", "AppUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Course_CourseID", "dbo.Courses", "CourseID");
            AddForeignKey("dbo.Courses", "AppUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Courses", "AppUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
