namespace StudentMange.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Myname = c.String(nullable: false, maxLength: 128),
                        Passsword = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Myname);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Users");
        }
    }
}
