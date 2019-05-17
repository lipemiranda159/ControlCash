namespace ControlCash.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createlimithistorytable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TbLimitHistory",
                c => new
                    {
                        LimitHistoryId = c.Int(nullable: false, identity: true),
                        Month = c.Short(nullable: false),
                        Year = c.Short(nullable: false),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        LimitValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LimitHistoryId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.TbLimitHistory");
        }
    }
}
