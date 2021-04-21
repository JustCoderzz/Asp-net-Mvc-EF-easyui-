using ContosoUniversity.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ContosoUniversity.DAL
{
    public class SchoolContext : DbContext
    {

        public SchoolContext() : base("SchoolContext")
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();//OnModelCreating方法中
                  //的 modelBuilder.Conventions.Remove 语句阻止复数表名称。 如果未执行此操作，
                   //则数据库中生成的表将被命名为 Students、Courses和 Enrollments。 相反，表名称将为 Student、Course和 Enrollment。
                    //开发者对表名称是否应为复数意见不一。
                     //本教程使用单数形式，但重要的是，您可以选择您喜欢的任何形式，包括或省略此行代码。
        }
    }
}