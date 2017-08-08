namespace Repository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class priceorder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Prices", "Order", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Prices", "Order", c => c.String());
        }
    }
}
