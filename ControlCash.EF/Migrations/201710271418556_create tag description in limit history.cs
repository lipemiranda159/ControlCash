namespace ControlCash.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtagdescriptioninlimithistory : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.TbLimitHistory", "TagDescription", c => c.String(maxLength: 30));
        }
        
        public override void Down()
        {
            DropColumn("dbo.TbLimitHistory", "TagDescription");
        }
    }
}
