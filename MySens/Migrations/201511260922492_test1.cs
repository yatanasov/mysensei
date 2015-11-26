namespace MySens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Course_CourseID", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Course_CourseID");
            AddForeignKey("dbo.AspNetUsers", "Course_CourseID", "dbo.Courses", "CourseID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Course_CourseID", "dbo.Courses");
            DropIndex("dbo.AspNetUsers", new[] { "Course_CourseID" });
            DropColumn("dbo.AspNetUsers", "Course_CourseID");
        }
    }
}
