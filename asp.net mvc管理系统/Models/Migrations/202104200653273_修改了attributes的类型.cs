namespace Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 修改了attributes的类型 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EasyTrees", "attributes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EasyTrees", "attributes");
        }
    }
}
