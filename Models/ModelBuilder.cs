using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Emp.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
             modelBuilder.Entity<Employee>().HasData(
                new Employee
                {
                    Id = 1,
                    Name = "Mark",
                    Department = Dept.HR,
                    Email = "mark@gmail.com"
                },
                new Employee
                {
                    Id = 2,
                    Name = "James",
                    Department = Dept.IT,
                    Email = "james@gmail.com"
                },
                new Employee
                {
                    Id = 3,
                    Name = "Asseem",
                    Department = Dept.Payroll,
                    Email = "asseem @gmail.com"
                }
                ) ;
        }
    }
}
