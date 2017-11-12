namespace FrunutStock.Web.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ItemWarehouses", "QtyBoxesOnhand", c => c.Int(nullable: false));
            AlterColumn("dbo.ItemWarehouses", "QtyTotalWeightReserved", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ItemWarehouses", "QtyTotalWeightReserved", c => c.Int(nullable: false));
            AlterColumn("dbo.ItemWarehouses", "QtyBoxesOnhand", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
