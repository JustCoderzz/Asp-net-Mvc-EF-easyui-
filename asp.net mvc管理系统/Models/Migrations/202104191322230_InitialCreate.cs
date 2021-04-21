namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EasyTrees",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        pid = c.Int(nullable: false),
                        text = c.String(),
                        state = c.String(),
                        ischecked = c.Boolean(nullable: false),
                        EasyTree_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.EasyTrees", t => t.EasyTree_id)
                .Index(t => t.EasyTree_id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EasyTrees", "EasyTree_id", "dbo.EasyTrees");
            DropIndex("dbo.EasyTrees", new[] { "EasyTree_id" });
            DropTable("dbo.EasyTrees");
        }
    }
}
