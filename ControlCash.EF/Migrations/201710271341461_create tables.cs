namespace ControlCash.EF.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TbLimit",
                c => new
                    {
                        LimitId = c.Int(nullable: false, identity: true),
                        TagDescription = c.String(maxLength: 30),
                        LimitValue = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.LimitId);
            
            CreateTable(
                "dbo.TbMoviemntHistory",
                c => new
                    {
                        MovimentHistoryId = c.Int(nullable: false, identity: true),
                        Description = c.String(maxLength: 30),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Date = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.MovimentHistoryId)
                .ForeignKey("dbo.TbUser", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.TbUser",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        UserIdentifier = c.String(maxLength: 50),
                        WalletId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.TbWallet", t => t.WalletId, cascadeDelete: true)
                .Index(t => t.WalletId);
            
            CreateTable(
                "dbo.TbWallet",
                c => new
                    {
                        WalletId = c.Int(nullable: false, identity: true),
                        Balance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.WalletId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TbMoviemntHistory", "UserId", "dbo.TbUser");
            DropForeignKey("dbo.TbUser", "WalletId", "dbo.TbWallet");
            DropIndex("dbo.TbUser", new[] { "WalletId" });
            DropIndex("dbo.TbMoviemntHistory", new[] { "UserId" });
            DropTable("dbo.TbWallet");
            DropTable("dbo.TbUser");
            DropTable("dbo.TbMoviemntHistory");
            DropTable("dbo.TbLimit");
        }
    }
}
