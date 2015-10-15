using System.Collections.Generic;
using StudentSystem.Models;

namespace StudentSystem.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentSystem.Data.StudentContext>
    {
        public Configuration()
        {
            AutomaticMigrationDataLossAllowed = true;
            AutomaticMigrationsEnabled = true;
            ContextKey = "StudentSystem.Data.StudentContext";
        }

        protected override void Seed(StudentSystem.Data.StudentContext context)
        {
            DateTime start = new DateTime(1995, 1, 1);
            Random gen = new Random();
            Random rc = new Random();
            int range = (DateTime.Today - start).Days;
            HashSet<Student> students = new HashSet<Student>();
            int index = 0;

            for (int i = 0; i < 100; i++)
            {
                var resoures = new Resource();
                resoures.Name = "dsadasdada" + i;
                if (i > 50)
                {
                    resoures.Type = (i % 2 == 0) ? ResourceType.Presentation : ResourceType.Document;
                }
                else
                {
                    resoures.Type = (i % 2 == 0) ? ResourceType.Video : ResourceType.Other;
                }

                resoures.Url = "www.pornhub.com/video/" + i;
                context.Resources.AddOrUpdate(r => r.ResourceId, resoures);
            }
            context.SaveChanges();

            for (int i = 0; i < 100; i++)
            {
                var student = new Student
                {
                    Name = "Pesho" + i,
                    PhoneNumber = "0895" + i % 10 + "34" + (i * 2 / 10) % 10 + "3" + ((i - i / 2) % 10) + "1" + ((i * 1 * 2 / 5) % 123),
                    RegistrationDate = DateTime.Now.AddDays(gen.Next(i * 10 + i / 2)),
                    BirthDay = start.AddDays(gen.Next(range))
                };

                context.Students.AddOrUpdate(u => u.Name, student);
                students.Add(student);
            }
            context.SaveChanges();

            index = 0;
            for (int i = 0; i < 100; i++)
            {
                var homework = new Homework();
                homework.Content = "dsadasdadasda" + i;
                homework.ContentType = (i % 2 == 0) ? "rar" : "zip";
                homework.SubmissionDate = DateTime.Now.AddDays(gen.Next(i * 10 + i / 2));
                homework.Student = context.Students.ToList().ElementAt(index++);
                context.Homeworks.AddOrUpdate(h => h.Content, homework);
            }
            context.SaveChanges();

            index = 0;
            for (int i = 0; i < 100; i++)
            {
                var course = new Course();
                course.Name = "dsadadassd" + i;
                course.Description = i + "890s7chyesuiyrweuisjdfdhsff";
                course.Price = (decimal)(100 * i * 1.397);
                course.StartDate = DateTime.Now.AddDays(gen.Next(i * 10 + i / 2));
                course.EndDate = DateTime.Now.AddDays(gen.Next(i * 13 + i));
                course.Resources.Add(context.Resources.ToList().ElementAt(index));
                course.Homeworks.Add(context.Homeworks.ToList().ElementAt(index));
                context.Courses.AddOrUpdate(c => c.Name, course);
                index++;
            }
            context.SaveChanges();

            index = 0;

            foreach (var student in context.Students)
            {
                Course course = context.Courses.ToList().ElementAt(index);

                if (!student.Courses.Contains(course))
                {
                    student.Courses.Add(course);
                }
                index++;
            }
            context.SaveChanges();

            index = 0;
            foreach (var course in context.Courses)
            {
                Student student = context.Students.ToList().ElementAt(index++);
                if (!course.Students.Contains(student))
                {
                    course.Students.Add(student);
                }
            }

            for (int i = 0; i < 7; i++)
            {
                var resoures = new Resource();
                resoures.Name = "dsadasdada" + i * 100;
                if (i > 50)
                {
                    resoures.Type = (i % 2 == 0) ? ResourceType.Presentation : ResourceType.Document;
                }
                else
                {
                    resoures.Type = (i % 2 == 0) ? ResourceType.Video : ResourceType.Other;
                }

                resoures.Url = "www.pornhub.com/video/" + i * 100;
                if (context.Courses.ToList().ElementAt(0).Resources.Count() < 7)
                {
                    context.Courses.ToList()[0].Resources.Add(resoures);
                }
            }
            context.SaveChanges();
        }
    }
}
