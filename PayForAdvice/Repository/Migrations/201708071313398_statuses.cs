namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class statuses : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Answers", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Questions", "Status", c => c.Int(nullable: false));
            AlterColumn("dbo.Users", "Status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Status", c => c.String());
            AlterColumn("dbo.Questions", "Status", c => c.String());
            AlterColumn("dbo.Answers", "Status", c => c.String());
        }
    }
}
