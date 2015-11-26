namespace MySens.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relationshipteachercourses2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Courses", name: "TeacherId", newName: "AppUserID");
            RenameIndex(table: "dbo.Courses", name: "IX_TeacherId", newName: "IX_AppUserID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Courses", name: "IX_AppUserID", newName: "IX_TeacherId");
            RenameColumn(table: "dbo.Courses", name: "AppUserID", newName: "TeacherId");
        }
    }
}
