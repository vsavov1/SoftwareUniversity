using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Problem_1_DbContextForTheSoftUni.EmployeeDAOClass;

namespace Problem_1_DbContextForTheSoftUni
{
    class SoftUniExamples
    {
        private static void Main(string[] args)
        {
            //Problem 1
            //Add UNCOMMENT
            //Employee employee = new Employee();
            //employee.FirstName = "Pesho";
            //employee.LastName = "Gosho";
            //employee.MiddleName = "dsada";
            //employee.JobTitle = "tarza";
            //employee.DepartmentID = 7;
            //employee.ManagerID = 210;
            //employee.HireDate = DateTime.Now;
            //employee.Salary = 123;
            //employee.AddressID = 259;
            //EmployeeDAOClass.Add(employee);

            //FindByKey UNCOMMENT
            //Employee findEmployee = EmployeeDAOClass.FindByKey(292); // replace with other id if get error
            //Console.WriteLine(findEmployee.FirstName);

            //Modify UNCOMMENT
            //findEmployee.FirstName = "No Name";
            //EmployeeDAOClass.Modify(findEmployee);
            //Console.WriteLine(findEmployee.FirstName);

            //Delete UNCOMMENT
            //EmployeeDAOClass.Delete(findEmployee);

            //Problem 2
            //Task1
            //var projects = EmployeeDAOClass.Context.Projects
            //    .Where(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)
            //    .Join(
            //        EmployeeDAOClass.Context.Employees,
            //        project => project.Employees,
            //        employee => employee.EmployeeID,
            //        (
            //            (project, employee) => new
            //            {
            //                ProjectName = project.Name,
            //                ProjectStartDate = project.StartDate,
            //                ProjectEndDate = project.EndDate,
            //                EmploeyeeName = employee.FirstName + ' ' + employee.LastName
            //            }));

            //UNCOMMENT
            //var projects = Context.Projects
            //          .Where(p => p.StartDate.Year >= 2001 && p.StartDate.Year <= 2003)
            //          .Select(p => new
            //          {
            //              ProjectName = p.Name,
            //              StartDate = p.StartDate,
            //              EndDate = p.EndDate
            //    // There is no project manager in the database
            //});

            //Task 2 UNCOMMENT
            //var addresses = Context.Addresses
            //           .OrderByDescending(a => a.Employees.Count)
            //           .Select(a => new
            //           {
            //               AddressText = a.AddressText,
            //               TownName = a.Town.Name,
            //               EmployeeCount = a.Employees.Count
            //           }).Take(10);

            //foreach (var address in addresses)
            //{
            //    Console.WriteLine(address.AddressText + ", " + address.TownName + " - " + address.EmployeeCount + "employees");
            //}

            //Task 2 UNCOMMENT
            //var employee = Context.Employees
            //    .Where(e => e.EmployeeID == 146)
            //    .Select(p => new
            //    {
            //        FirstName = p.FirstName,
            //        LastName = p.LastName,
            //        JobTitle = p.JobTitle,
            //        Projects = p.Projects.OrderBy(s => s.Name).Select(a => a.Name)
            //    });


            //foreach (var a in employee)
            //{
            //    Console.WriteLine(a.FirstName);
            //    Console.WriteLine(a.LastName);
            //    Console.WriteLine(a.JobTitle);
            //    Console.Write("Projects: ");
            //    Console.WriteLine(string.Join(", ", a.Projects));
            //}

            //Task 4 UNCOMMENT
            //var departments = EmployeeDAOClass.Context.Departments
            //    .Where(d => d.Employees.Count > 5)
            //    .OrderBy(d => d.Employees.Count)
            //    .Select(d => new
            //    {
            //        DepartmentName = d.Name,
            //        ManagerName = Context.Employees.Where(e => e.EmployeeID == d.ManagerID).Select(e => e.LastName).FirstOrDefault(),
            //        Employees = d.Employees
            //    });

            //Console.WriteLine(departments.Count());
            //foreach (var d in departments)
            //{
            //    Console.WriteLine("--{0} - Manager: {1}, Employees: {2}",
            //        d.DepartmentName,
            //        d.ManagerName,
            //        d.Employees.Count);
            //}

            //Problem 4
            //var sw = new Stopwatch();
            //sw.Start();
            //var linq = Context.Employees
            //    .Where(e => e.Projects.Any(p => p.StartDate.Year == 2002))
            //    .Select(e => e.FirstName);
            //foreach (var q in linq)
            //{
            //    Console.WriteLine(q);
            //}
            //Console.WriteLine("LINQ Query time: {0}", sw.Elapsed);

            //sw.Restart();

            //var NativeQuery = Context
            //    .Database
            //    .SqlQuery<string>(
            //    "SELECT e.FirstName" +
            //    "FROM Employees e" +
            //    "JOIN EmployeesProjects ep" +
            //    "ON e.EmployeeID = ep.EmployeeID" +
            //    "JOIN Projects p" +
            //    "ON ep.ProjectID = p.ProjectID" +
            //    "WHERE YEAR(p.StartDate) = 2002"
            //    );

            //foreach (var q in linq)
            //{
            //    Console.WriteLine(q);
            //}

            //Console.WriteLine("LINQ Query time: {0}", sw.Elapsed);
            //sw.Stop();


            //Problem5
            SoftUniEntities contextFirst = new SoftUniEntities();
            var employeFirst = contextFirst.Employees
                .Where(e => e.EmployeeID == 105).First();
            employeFirst.FirstName = "new nam1e";

            SoftUniEntities contextSecond = new SoftUniEntities();
            var employeeSecond = contextFirst.Employees
                .Where(e => e.EmployeeID == 105).First();
            employeFirst.FirstName = "new name22";

            contextFirst.SaveChanges();
            contextSecond.SaveChanges();

            //first case - last changes saved
            //last case - first changes saved

            //Problem 6
            var Projects = EmployeeDAOClass.GetProjectByEmployee("Ruth", "Ellerbrock");
            foreach (var Project in Projects)
            {
                Console.WriteLine("{0} , {1} , {2} ", Project.Name, Project.Description, Project.StartDate);
            }

        }
    }
}
