
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;

namespace Problem_1_DbContextForTheSoftUni
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class EmployeeDAOClass
    {
        public static SoftUniEntities Context = new SoftUniEntities();

        public static void Add(Employee employee)
        {
            Context.SaveChanges();
        }

        public static Employee FindByKey(object key)
        {
            return Context.Employees.Find(key);
        }

        public static void Modify(Employee employee)
        {
            Context.SaveChanges();
        }

        public static void Delete(Employee employee)
        {
            Context.Employees.Remove(employee);
            Context.SaveChanges();
        }

        public static DbRawSqlQuery<uspProjectByEmploye> GetProjectByEmployee(string f, string s)
        {
            return Context.Database
                .SqlQuery<uspProjectByEmploye>(
                    "GetProjectsByEmployee @FirstName, @LastName",
                    new SqlParameter("FirstName", f),
                    new SqlParameter("LastName", s)
                );
        }

    }
}
