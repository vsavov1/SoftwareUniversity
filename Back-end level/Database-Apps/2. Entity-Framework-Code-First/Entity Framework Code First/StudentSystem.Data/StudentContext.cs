using StudentSystem.Data.Migrations;
using StudentSystem.Models;

namespace StudentSystem.Data
{
    using System.Data.Entity;

    public class StudentContext : DbContext
    {
      
        public StudentContext()
            : base("name=StudentContext")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<StudentContext, Configuration>());
        }

        public virtual IDbSet<Course> Courses { get; set; }
        public virtual IDbSet<Homework> Homeworks { get; set; }
        public virtual IDbSet<Student> Students { get; set; }
        public virtual IDbSet<Resource> Resources { get; set; }
    }
}