namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class categoryicon : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "IconUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Categories", "IconUrl");
        }
    }
}
