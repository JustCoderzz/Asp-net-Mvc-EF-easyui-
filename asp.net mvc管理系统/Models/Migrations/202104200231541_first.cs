namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.EasyTrees", "EasyTree_id", "dbo.EasyTrees");
            DropPrimaryKey("dbo.EasyTrees");
            AlterColumn("dbo.EasyTrees", "id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.EasyTrees", "id");
            AddForeignKey("dbo.EasyTrees", "EasyTree_id", "dbo.EasyTrees", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EasyTrees", "EasyTree_id", "dbo.EasyTrees");
            DropPrimaryKey("dbo.EasyTrees");
            AlterColumn("dbo.EasyTrees", "id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.EasyTrees", "id");
            AddForeignKey("dbo.EasyTrees", "EasyTree_id", "dbo.EasyTrees", "id");
        }
    }
}
