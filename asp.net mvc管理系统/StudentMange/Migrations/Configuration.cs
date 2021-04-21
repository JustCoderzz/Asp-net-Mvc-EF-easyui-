namespace StudentMange.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentMange.Models.User.UserDBContent>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "StudentMange.Models.User+UserDBContent";
        }

        protected override void Seed(StudentMange.Models.User.UserDBContent context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
