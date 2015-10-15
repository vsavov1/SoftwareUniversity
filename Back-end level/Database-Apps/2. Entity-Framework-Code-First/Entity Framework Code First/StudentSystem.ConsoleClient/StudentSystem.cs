using System;
using System.Data.Entity.Core.Objects;
using System.Linq;
using StudentSystem.Data;
using StudentSystem.Models;

namespace StudentSystem.ConsoleClient
{
    class StudentSystem
    {
        static void Main(string[] args)
        {

            //-----------------   Uncomment where it needs to see results ------------------//
            //-----------------   Uncomment where it needs to see results ------------------//
            //-----------------   Uncomment where it needs to see results ------------------//

            var context = new StudentContext();

            //Problem 3 Task 1   -----------------------------------------------------------------------------------
            var studentHomeworks = context.Homeworks
                .Select(x => new
                {
                    Name = x.Student.Name,
                    Content = x.Content,
                    Type = x.ContentType
                });

            //foreach (var stdh in studentHomeworks)
            //{
            //    Console.WriteLine("Student: {0}, Content: {1}, Type: {2}", stdh.Name, stdh.Content, stdh.Type);
            //}


            //Problem 3 Task 2  -----------------------------------------------------------------------------------
            var courses = context.Courses
                .OrderBy(c => c.StartDate)
                .Select(c => new
                {
                    CourseName = c.Name,
                    Description = c.Description,
                    Resources = c.Resources
                });

            //foreach (var course in courses)
            //{
            //    Console.WriteLine("\nCourse name: {0}, Description {1}", course.CourseName, course.Description);
            //    Console.WriteLine("------------- Resources -------------");
            //    foreach (var resource  in course.Resources)
            //    {
            //        Console.WriteLine("Name: {0}, Type: {1}, URL: {2}", resource.Name, resource.Type, resource.Url);
            //    }
            //}

            //Problem 3 Task 3  -----------------------------------------------------------------------------------
            var coursesWithResources  = context.Courses
                .Where(c => c.Resources.Count() > 5)
                .OrderByDescending(c => c.Resources.Count)
                .ThenByDescending(c => c.StartDate)
                .Select(c => new
                {
                    CourseName = c.Name,
                    c.Description,
                    c.Resources
                });

            //foreach (var course in coursesWithResources)
            //{
            //    Console.WriteLine("\nCourse name: {0}, Description {1}", course.CourseName, course.Description);
            //    Console.WriteLine("------------- Resources -------------");
            //    foreach (var resource in course.Resources)
            //    {
            //        Console.WriteLine("Name: {0}, Type: {1}, URL: {2}", resource.Name, resource.Type, resource.Url);
            //    }
            //}

            //Problem 3 Task 4   -----------------------------------------------------------------------------------
            var currentDate = new DateTime(2015, 10, 29);
            var coursesByDate = context.Courses
                .Where(s => s.StartDate <= currentDate && s.EndDate <= currentDate)
                .OrderByDescending(c => c.Students.Count)
                .ThenByDescending(c => Math.Abs((int)EntityFunctions.DiffDays(c.StartDate, c.EndDate)))
                .Select(c => new
                {
                    c.Name,
                    c.StartDate,
                    c.EndDate,
                    Duration = Math.Abs((int) EntityFunctions.DiffDays(c.StartDate, c.EndDate)),
                    StudentsCount = c.Students.Count()
                });

            //foreach (var c in coursesByDate)
            //{
            //    Console.WriteLine("\n Course name: {0}, Start date: {1}, End Date: {2}, Duration in days: {3}, Students count: {4} ",
            //        c.Name,
            //        c.StartDate,
            //        c.EndDate,
            //        c.Duration,
            //        c.StudentsCount);
            //}

            //Problem 3 Task 5   -----------------------------------------------------------------------------------

            var studentCourses = context.Students
                .Select(c => new
                {
                    StudentName = c.Name,
                    CoursesCount = c.Courses.Count(),
                    TotalPrice = c.Courses.Sum(l => l.Price),
                    AvgPrice = c.Courses.Average(o => o.Price)
                });

            //avg and total price can be similiar because each student have one course

            //foreach (var student in studentCourses)
            //{
            //    Console.WriteLine("Student name: {0}, Course count: {1}, Total price: {2}, Avarege price: {3:F2}",
            //        student.StudentName,
            //        student.CoursesCount,
            //        student.TotalPrice,
            //        student.AvgPrice);
            //}


            //Problem 4 test adding licenses

            for (int i = 0; i < 5; i++)
            {
                var license = new Licenses();
                license.Name = "dsadad" + i;
                if (!context.Resources.ToList().ElementAt(i).Licenseses.Contains(license))
                {
                    context.Resources.ToList().ElementAt(i).Licenseses.Add(license);
                }
            }

            context.SaveChanges();
        }
    }
}
