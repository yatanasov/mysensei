namespace MySens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationships : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Courses", "AppUserID", "dbo.AspNetUsers");
            DropIndex("dbo.Courses", new[] { "AppUserID" });
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.TagID);
            
            CreateTable(
                "dbo.TagCourses",
                c => new
                    {
                        Tag_TagID = c.Int(nullable: false),
                        Course_CourseID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_TagID, t.Course_CourseID })
                .ForeignKey("dbo.Tags", t => t.Tag_TagID, cascadeDelete: true)
                .ForeignKey("dbo.Courses", t => t.Course_CourseID, cascadeDelete: true)
                .Index(t => t.Tag_TagID)
                .Index(t => t.Course_CourseID);
            
            AddColumn("dbo.Courses", "AppUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Courses", "AppUser_Id1", c => c.String(maxLength: 128));
            AddColumn("dbo.Courses", "Teacher_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Courses", "AppUserID", c => c.String());
            CreateIndex("dbo.Courses", "AppUser_Id");
            CreateIndex("dbo.Courses", "AppUser_Id1");
            CreateIndex("dbo.Courses", "Teacher_Id");
            AddForeignKey("dbo.Courses", "AppUser_Id1", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Courses", "Teacher_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Courses", "AppUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Courses", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "Teacher_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Courses", "AppUser_Id1", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagCourses", "Course_CourseID", "dbo.Courses");
            DropForeignKey("dbo.TagCourses", "Tag_TagID", "dbo.Tags");
            DropIndex("dbo.TagCourses", new[] { "Course_CourseID" });
            DropIndex("dbo.TagCourses", new[] { "Tag_TagID" });
            DropIndex("dbo.Courses", new[] { "Teacher_Id" });
            DropIndex("dbo.Courses", new[] { "AppUser_Id1" });
            DropIndex("dbo.Courses", new[] { "AppUser_Id" });
            AlterColumn("dbo.Courses", "AppUserID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Courses", "Teacher_Id");
            DropColumn("dbo.Courses", "AppUser_Id1");
            DropColumn("dbo.Courses", "AppUser_Id");
            DropTable("dbo.TagCourses");
            DropTable("dbo.Tags");
            CreateIndex("dbo.Courses", "AppUserID");
            AddForeignKey("dbo.Courses", "AppUserID", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
