namespace FrunutStock.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class relations : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Items", "ItemGroupID", "dbo.ItemGroups");
            AddColumn("dbo.ReceiveItems", "CompanyID", c => c.Long());
            CreateIndex("dbo.ReceiveItems", "CompanyID");
            AddForeignKey("dbo.ReceiveItems", "CompanyID", "dbo.Companies", "ID");
            AddForeignKey("dbo.Items", "ItemGroupID", "dbo.ItemGroups", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Items", "ItemGroupID", "dbo.ItemGroups");
            DropForeignKey("dbo.ReceiveItems", "CompanyID", "dbo.Companies");
            DropIndex("dbo.ReceiveItems", new[] { "CompanyID" });
            DropColumn("dbo.ReceiveItems", "CompanyID");
            AddForeignKey("dbo.Items", "ItemGroupID", "dbo.ItemGroups", "ID", cascadeDelete: true);
        }
    }
}
